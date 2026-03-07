using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using CSiAPIv1;

namespace EtabsTools
{
    // Story Drift veri yapısı
    public class StoryDriftData
    {
        public string Story { get; set; }
        public string OutputCase { get; set; }
        public string Direction { get; set; }
        public double Drift { get; set; }
        public double LambdaDrift { get; set; }
        public double Limit { get; set; }
        public bool IsOK { get; set; }
    }

    // Hesap sonucu yapısı
    public class GoreliKatResult
    {
        public List<StoryDriftData> Items { get; set; } = new List<StoryDriftData>();
        public double Lambda { get; set; }
        public double Limit { get; set; }
        public bool AllPassed => Items.TrueForAll(x => x.IsOK);
    }

    // Göreli Kat Ötelemesi Hesap Manager
    public class GoreliKatOtelemesiManager
    {
        // DD-2 Parametreleri
        public double SDS_DD2 { get; set; }
        public double SD1_DD2 { get; set; }

        // DD-3 Parametreleri
        public double SDS_DD3 { get; set; }
        public double SD1_DD3 { get; set; }

        // Diğer Parametreler
        public double K { get; set; } = 1.0;
        public double Tp { get; set; } = 0.5;
        public bool EsnekDerz { get; set; } = false;
        public bool BodrumKabulu { get; set; } = false;
        public int BodrumKatSayisi { get; set; } = 0;

        public GoreliKatOtelemesiManager() { }

        public GoreliKatOtelemesiManager(double sds_dd2, double sds_dd3, double sd1_dd2, double sd1_dd3,
                                          double k, double tp, bool esnekDerz, bool bodrumKabulu, int bodrumKatSayisi)
        {
            SDS_DD2 = sds_dd2; SDS_DD3 = sds_dd3;
            SD1_DD2 = sd1_dd2; SD1_DD3 = sd1_dd3;
            K = k; Tp = tp;
            EsnekDerz = esnekDerz;
            BodrumKabulu = bodrumKabulu;
            BodrumKatSayisi = bodrumKatSayisi;
        }

        // Kat numarası çıkarma (Story1 -> 1, Story10 -> 10)
        public static int? ExtractStoryNumber(string storyName)
        {
            if (string.IsNullOrEmpty(storyName)) return null;
            var match = Regex.Match(storyName, @"\d+");
            return match.Success ? int.Parse(match.Value) : (int?)null;
        }

        // Lambda hesabı (TBDY 2018)
        public double CalculateLambda()
        {
            if (SDS_DD2 == 0) return 0;
            double TA = SD1_DD2 / SDS_DD2;
            return Tp < TA ? SDS_DD3 / SDS_DD2 : SD1_DD3 / SD1_DD2;
        }

        // Drift limit hesabı
        public double CalculateLimit()
        {
            return EsnekDerz ? 0.016 * K : 0.008 * K;
        }

        // Ana hesaplama
        public GoreliKatResult Calculate(List<StoryDriftData> driftData)
        {
            double lambda = CalculateLambda();
            double limit = CalculateLimit();

            var result = new GoreliKatResult
            {
                Lambda = lambda,
                Limit = limit
            };

            foreach (var item in driftData)
            {
                item.LambdaDrift = lambda * item.Drift;
                item.Limit = limit;
                item.IsOK = item.LambdaDrift < limit;
                result.Items.Add(item);
            }

            return result;
        }

        // Eksik deprem kombinasyonu belirleme (bodrum durumuna göre)
        public string DetermineLoadCase(string direction, int? storyNumber)
        {
            string dir = direction.ToUpper();
            if (BodrumKabulu && storyNumber.HasValue && storyNumber.Value <= BodrumKatSayisi)
                return $"RS{dir}ALT";
            return $"RS{dir}UST";
        }
    }

    // ---------------------------------------------------------
    // GÖRELİ KAT ÖTELEMESİ UI SINIFI
    // Form1.cs'den ayrıştırılmış UI ve Event Handler kodları
    // ---------------------------------------------------------
    public class GoreliKatOtelemesiUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<Panel, int, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        // UI Kontrolleri
        private TextBox txtSDS_DD2, txtSDS_DD3, txtSD1_DD2, txtSD1_DD3, txtK, txtTp;
        private CheckBox chkEsnekDerz, chkBodrum;
        private NumericUpDown numBodrumKat;
        private ListBox lstCombinations;
        private FlowLayoutPanel pnlSelectedCombos;
        private DataGridView dgvResults;
        private Label lblGoreliStatus;

        // Spektrum değerleri referansı (Tasarım Spektrumu sekmesinden)
        private TextBox _txtSDS_Ref, _txtSD1_Ref;

        public GoreliKatOtelemesiUI(Form1 form, Func<cSapModel> getSapModel, 
                                     Func<Panel, int, Panel> createNavigationPanel, 
                                     Action<int> goToPage, Color colorBackground)
        {
            _form = form;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNavigationPanel;
            _goToPage = goToPage;
            _colorBackground = colorBackground;
        }

        public void Initialize(TabPage page, TextBox txtSDS_Ref = null, TextBox txtSD1_Ref = null)
        {
            _txtSDS_Ref = txtSDS_Ref;
            _txtSD1_Ref = txtSD1_Ref;

            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Başlık
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // İçerik
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Navigasyon

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = Form1.CreateHeaderLabel("Göreli Kat Ötelemesi Tahkiki");
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Sol panel
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // Sağ panel
            tlp.Padding = new Padding(20, 10, 20, 10);

            // =============== SOL PANEL ===============
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill };
            
            // Ana sol panel için TableLayout
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 185F)); // Kombinasyonlar + Seçili (yan yana)
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Parametreler
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  // Hesapla butonu

            // --- KOMBİNASYON SEÇİM ALANI (Yan yana düzen) ---
            TableLayoutPanel tlpCombos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                Margin = new Padding(0, 0, 10, 5)
            };
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Sol: Kombinasyonlar paneli
            RoundedPanel pnlCombos = new RoundedPanel
            {
                Title = "Combinations and Cases",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 5, 0)
            };

            lstCombinations = new ListBox
            {
                Location = new Point(15, 35),
                Size = new Size(145, 95),
                SelectionMode = SelectionMode.MultiExtended,
                Font = new Font("Segoe UI", 8)
            };
            lstCombinations.DoubleClick += LstCombinations_DoubleClick;

            Button btnLoadCombos = new Button
            {
                Text = "Getir",
                Size = new Size(55, 28),
                Location = new Point(15, 135),
                BackColor = Color.FromArgb(220, 220, 220),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLoadCombos.FlatAppearance.BorderSize = 1;
            btnLoadCombos.Click += BtnLoadCombos_Click;

            Button btnSelectCombos = new Button
            {
                Text = "Seç",
                Size = new Size(55, 28),
                Location = new Point(80, 135),
                BackColor = Color.FromArgb(159, 219, 255),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSelectCombos.FlatAppearance.BorderSize = 1;
            btnSelectCombos.Click += BtnSelectCombos_Click;

            pnlCombos.Controls.Add(lstCombinations);
            pnlCombos.Controls.Add(btnLoadCombos);
            pnlCombos.Controls.Add(btnSelectCombos);
            tlpCombos.Controls.Add(pnlCombos, 0, 0);

            // Sağ: Seçili Kombinasyonlar paneli
            RoundedPanel pnlSelectedWrapper = new RoundedPanel
            {
                Title = "Seçili Kombinasyonlar",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(5, 0, 0, 0)
            };

            pnlSelectedCombos = new FlowLayoutPanel
            {
                Location = new Point(10, 35),
                Size = new Size(145, 130),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White
            };
            pnlSelectedWrapper.Controls.Add(pnlSelectedCombos);
            tlpCombos.Controls.Add(pnlSelectedWrapper, 1, 0);

            tlpLeft.Controls.Add(tlpCombos, 0, 0);

            // --- PARAMETRELER ---
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "TBDY 2018 Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 5, 10, 5)
            };

            int startY = 40;
            int gapY = 32;
            int labelX = 15;
            int textX = 120;
            int textW = 80;

            pnlParams.Controls.Add(new Label { Text = "SDS (DD-2):", Location = new Point(labelX, startY), AutoSize = true });
            txtSDS_DD2 = new TextBox { Location = new Point(textX, startY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtSDS_DD2);

            pnlParams.Controls.Add(new Label { Text = "SDS (DD-3):", Location = new Point(labelX, startY + gapY), AutoSize = true });
            txtSDS_DD3 = new TextBox { Location = new Point(textX, startY + gapY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtSDS_DD3);

            pnlParams.Controls.Add(new Label { Text = "SD1 (DD-2):", Location = new Point(labelX, startY + gapY * 2), AutoSize = true });
            txtSD1_DD2 = new TextBox { Location = new Point(textX, startY + gapY * 2 - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtSD1_DD2);

            pnlParams.Controls.Add(new Label { Text = "SD1 (DD-3):", Location = new Point(labelX, startY + gapY * 3), AutoSize = true });
            txtSD1_DD3 = new TextBox { Location = new Point(textX, startY + gapY * 3 - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtSD1_DD3);

            pnlParams.Controls.Add(new Label { Text = "k:", Location = new Point(labelX, startY + gapY * 4), AutoSize = true });
            txtK = new TextBox { Location = new Point(textX, startY + gapY * 4 - 3), Width = textW, Text = "1" };
            pnlParams.Controls.Add(txtK);

            pnlParams.Controls.Add(new Label { Text = "Tp:", Location = new Point(labelX, startY + gapY * 5), AutoSize = true });
            txtTp = new TextBox { Location = new Point(textX, startY + gapY * 5 - 3), Width = textW, Text = "0.5" };
            pnlParams.Controls.Add(txtTp);

            chkEsnekDerz = new CheckBox
            {
                Text = "Esnek derz var mı? (Var: 0.016, Yok: 0.008)",
                Location = new Point(labelX, startY + gapY * 6),
                AutoSize = true
            };
            pnlParams.Controls.Add(chkEsnekDerz);

            chkBodrum = new CheckBox
            {
                Text = "Bodrum kabulü var mı?",
                Location = new Point(labelX, startY + gapY * 7),
                AutoSize = true
            };
            chkBodrum.CheckedChanged += (s, e) => { numBodrumKat.Enabled = chkBodrum.Checked; };
            pnlParams.Controls.Add(chkBodrum);

            pnlParams.Controls.Add(new Label { Text = "Bodrum kat sayısı:", Location = new Point(labelX + 20, startY + gapY * 8), AutoSize = true });
            numBodrumKat = new NumericUpDown
            {
                Location = new Point(textX + 30, startY + gapY * 8 - 3),
                Width = 50,
                Minimum = 0,
                Maximum = 20,
                Value = 0,
                Enabled = false
            };
            pnlParams.Controls.Add(numBodrumKat);

            tlpLeft.Controls.Add(pnlParams, 0, 1);

            // --- HESAPLA BUTONU ---
            Panel pnlButton = new Panel { Dock = DockStyle.Fill };
            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "Hesapla ve Kaydet",
                Size = new Size(200, 40),
                Location = new Point(50, 5),
                BaseColor = Color.FromArgb(159, 219, 255),
                BorderRadius = 20,
                GrowAmount = 2
            };
            btnCalculate.Click += BtnCalculateGoreliKat_Click;
            pnlButton.Controls.Add(btnCalculate);
            tlpLeft.Controls.Add(pnlButton, 0, 2);

            pnlLeft.Controls.Add(tlpLeft);
            tlp.Controls.Add(pnlLeft, 0, 0);

            // =============== SAĞ PANEL - SONUÇLAR ===============
            RoundedPanel pnlResults = new RoundedPanel
            {
                Title = "Sonuçlar",
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                Margin = new Padding(10, 0, 0, 0),
                TitleFont = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            dgvResults = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                ScrollBars = ScrollBars.Both,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(159, 219, 255),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvResults.Columns.Add("Kat", "Kat");
            dgvResults.Columns.Add("Kombinasyon", "Kombinasyon");
            dgvResults.Columns.Add("Dogrultu", "Doğrultu");
            dgvResults.Columns.Add("Drift", "Drift");
            dgvResults.Columns.Add("LambdaDrift", "λδi/hi");
            dgvResults.Columns.Add("Limit", "Limit");
            dgvResults.Columns.Add("Durum", "Durum");

            lblGoreliStatus = new Label
            {
                Text = "",
                Dock = DockStyle.Bottom,
                Height = 25,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Blue,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel dgvContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 40, 15, 15)
            };
            dgvContainer.Controls.Add(dgvResults);
            dgvContainer.Controls.Add(lblGoreliStatus);

            pnlResults.Controls.Add(dgvContainer);
            tlp.Controls.Add(pnlResults, 1, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            page.Tag = 3; // Göreli Kat Ötelemesi tab index
            page.VisibleChanged += (s, e) => {
                if (page.Visible)
                {
                    // Navigasyon panelini ekle
                    if (mainLayout.Controls.Count < 3)
                    {
                        Panel navPanel = _createNavigationPanel(null, 3);
                        mainLayout.Controls.Add(navPanel, 0, 2);
                    }

                    // Tasarım Spektrumu değerlerini aktar (eğer girilmişse)
                    if (_txtSDS_Ref != null && !string.IsNullOrEmpty(_txtSDS_Ref.Text) && _txtSDS_Ref.Text != "0")
                        txtSDS_DD2.Text = _txtSDS_Ref.Text;
                    if (_txtSD1_Ref != null && !string.IsNullOrEmpty(_txtSD1_Ref.Text) && _txtSD1_Ref.Text != "0")
                        txtSD1_DD2.Text = _txtSD1_Ref.Text;
                }
            };

            page.Controls.Add(mainLayout);
        }

        // Kombinasyon listesine çift tıklayınca seçili listeye ekle
        private void LstCombinations_DoubleClick(object sender, EventArgs e)
        {
            foreach (var item in lstCombinations.SelectedItems)
            {
                string comboName = item.ToString();
                if (!IsComboAlreadySelected(comboName))
                    AddSelectedComboTag(comboName);
            }
        }

        private bool IsComboAlreadySelected(string name)
        {
            foreach (Control c in pnlSelectedCombos.Controls)
                if (c.Tag?.ToString() == name) return true;
            return false;
        }

        private void AddSelectedComboTag(string comboName)
        {
            // Oval köşeli tag panel
            Panel tag = new Panel
            {
                Size = new Size(120, 24),
                BackColor = Color.White,
                Margin = new Padding(3),
                Tag = comboName
            };
            tag.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    int radius = 12;
                    var rect = new Rectangle(0, 0, tag.Width - 1, tag.Height - 1);
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    tag.Region = new Region(path);
                    using (var pen = new Pen(Color.LightGray, 1.5f))
                        e.Graphics.DrawPath(pen, path);
                }
            };

            Label lbl = new Label
            {
                Text = comboName,
                AutoSize = false,
                Size = new Size(95, 20),
                Location = new Point(5, 2),
                Font = new Font("Segoe UI", 7.5f),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnRemove = new Button
            {
                Text = "✕",
                Size = new Size(18, 18),
                Location = new Point(100, 3),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 7, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += (s, ev) => { pnlSelectedCombos.Controls.Remove(tag); };

            tag.Controls.Add(lbl);
            tag.Controls.Add(btnRemove);
            pnlSelectedCombos.Controls.Add(tag);
        }

        // Seçili kombinasyonları ekle
        private void BtnSelectCombos_Click(object sender, EventArgs e)
        {
            foreach (var item in lstCombinations.SelectedItems)
            {
                string comboName = item.ToString();
                if (!IsComboAlreadySelected(comboName))
                    AddSelectedComboTag(comboName);
            }
        }

        // ETABS'tan kombinasyonları yükle
        private void BtnLoadCombos_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lstCombinations.Items.Clear();

                // Response Combinations
                int numCombos = 0;
                string[] comboNames = null;
                sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                if (comboNames != null)
                    foreach (var name in comboNames)
                        lstCombinations.Items.Add(name);

                // Load Cases
                int numCases = 0;
                string[] caseNames = null;
                sapModel.LoadCases.GetNameList(ref numCases, ref caseNames);
                if (caseNames != null)
                    foreach (var name in caseNames)
                        lstCombinations.Items.Add(name);

                if (lstCombinations.Items.Count == 0)
                    MessageBox.Show("Kombinasyon veya case bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kombinasyonlar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Göreli kat ötelemesi hesaplama
        private void BtnCalculateGoreliKat_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kilit kontrolü
            bool isLocked = sapModel.GetModelIsLocked();
            if (!isLocked)
            {
                MessageBox.Show("Model kilitli değil! Lütfen önce modeli çalıştırın (Run Analysis).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili kombinasyonları al
            List<string> selectedCombos = new List<string>();
            foreach (Control c in pnlSelectedCombos.Controls)
                if (c.Tag != null) selectedCombos.Add(c.Tag.ToString());

            if (selectedCombos.Count == 0)
            {
                MessageBox.Show("En az bir kombinasyon seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                double sds_dd2 = double.Parse(txtSDS_DD2.Text);
                double sds_dd3 = double.Parse(txtSDS_DD3.Text);
                double sd1_dd2 = double.Parse(txtSD1_DD2.Text);
                double sd1_dd3 = double.Parse(txtSD1_DD3.Text);
                double k = double.Parse(txtK.Text);
                double tp = double.Parse(txtTp.Text);

                var manager = new GoreliKatOtelemesiManager(
                    sds_dd2, sds_dd3, sd1_dd2, sd1_dd3,
                    k, tp, chkEsnekDerz.Checked, chkBodrum.Checked, (int)numBodrumKat.Value);

                // X ve Y yönü kombinasyonlarını ayır ve ALT/UST kombinasyonlarını da ayır
                List<string> xCombosUST = selectedCombos.Where(c => c.ToUpper().Contains("X") && c.ToUpper().Contains("UST")).ToList();
                List<string> xCombosALT = selectedCombos.Where(c => c.ToUpper().Contains("X") && c.ToUpper().Contains("ALT")).ToList();
                List<string> yCombosUST = selectedCombos.Where(c => c.ToUpper().Contains("Y") && c.ToUpper().Contains("UST")).ToList();
                List<string> yCombosALT = selectedCombos.Where(c => c.ToUpper().Contains("Y") && c.ToUpper().Contains("ALT")).ToList();

                // Bodrum kaç kat?
                int bodrumKatSayisi = chkBodrum.Checked ? (int)numBodrumKat.Value : 0;

                // ETABS'tan Story Drift verilerini çek ve yöne göre filtrele
                List<StoryDriftData> allDriftData = new List<StoryDriftData>();

                // X yönü hesapları - ÜST kombinasyonlar
                if (xCombosUST.Count > 0)
                {
                    var xData = GetStoryDriftFromETABS(sapModel, xCombosUST);
                    foreach (var item in xData)
                    {
                        if (item.Direction.ToUpper() == "X")
                        {
                            int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(item.Story);
                            if (!chkBodrum.Checked || storyNum == null || storyNum > bodrumKatSayisi)
                                allDriftData.Add(item);
                        }
                    }
                }

                // X yönü hesapları - ALT kombinasyonlar (bodrum katları için)
                if (chkBodrum.Checked && xCombosALT.Count > 0)
                {
                    var xData = GetStoryDriftFromETABS(sapModel, xCombosALT);
                    foreach (var item in xData)
                    {
                        if (item.Direction.ToUpper() == "X")
                        {
                            int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(item.Story);
                            if (storyNum != null && storyNum <= bodrumKatSayisi)
                                allDriftData.Add(item);
                        }
                    }
                }

                // Y yönü hesapları - ÜST kombinasyonlar
                if (yCombosUST.Count > 0)
                {
                    var yData = GetStoryDriftFromETABS(sapModel, yCombosUST);
                    foreach (var item in yData)
                    {
                        if (item.Direction.ToUpper() == "Y")
                        {
                            int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(item.Story);
                            if (!chkBodrum.Checked || storyNum == null || storyNum > bodrumKatSayisi)
                                allDriftData.Add(item);
                        }
                    }
                }

                // Y yönü hesapları - ALT kombinasyonlar (bodrum katları için)
                if (chkBodrum.Checked && yCombosALT.Count > 0)
                {
                    var yData = GetStoryDriftFromETABS(sapModel, yCombosALT);
                    foreach (var item in yData)
                    {
                        if (item.Direction.ToUpper() == "Y")
                        {
                            int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(item.Story);
                            if (storyNum != null && storyNum <= bodrumKatSayisi)
                                allDriftData.Add(item);
                        }
                    }
                }

                if (allDriftData.Count == 0)
                {
                    MessageBox.Show("Story Drift verisi bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hesapla
                var result = manager.Calculate(allDriftData);

                // Sonuçları DataGridView'e yaz (önce X, sonra Y)
                dgvResults.Rows.Clear();
                var sortedItems = result.Items.OrderBy(x => x.Direction == "X" ? 0 : 1).ThenBy(x => x.Story).ToList();
                foreach (var item in sortedItems)
                {
                    int rowIndex = dgvResults.Rows.Add(
                        item.Story,
                        item.OutputCase,
                        item.Direction,
                        item.Drift.ToString("0.00000"),
                        item.LambdaDrift.ToString("0.00000"),
                        item.Limit.ToString("0.00000"),
                        item.IsOK ? "✅" : "❌"
                    );

                    // Başarısız satırları kırmızı yap
                    if (!item.IsOK)
                        dgvResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                }

                // Durum mesajı
                lblGoreliStatus.Text = result.AllPassed ? "✅ Göreli Kat Ötelemesi Tahkiki Sağlanmıştır" : "❌ Göreli Kat Ötelemesi Tahkiki Sağlanmamıştır";
                lblGoreliStatus.ForeColor = result.AllPassed ? Color.Green : Color.Red;

                // Excel'e kaydet
                SaveGoreliKatResults(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesaplama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ETABS'tan Story Drift verilerini çek
        private List<StoryDriftData> GetStoryDriftFromETABS(cSapModel sapModel, List<string> combinations)
        {
            var results = new List<StoryDriftData>();

            try
            {
                // Önce tüm case seçimlerini temizle
                sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                // Seçili kombinasyonları aktifleştir
                foreach (var combo in combinations)
                {
                    sapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    sapModel.Results.Setup.SetComboSelectedForOutput(combo);
                }

                // Story Drift sonuçlarını al
                int numResults = 0;
                string[] story = null, loadCase = null, stepType = null, direction = null, label = null;
                double[] stepNum = null, drift = null, x = null, y = null, z = null;

                sapModel.Results.StoryDrifts(ref numResults, ref story, ref loadCase, ref stepType,
                    ref stepNum, ref direction, ref drift, ref label, ref x, ref y, ref z);

                if (story != null)
                {
                    for (int i = 0; i < numResults; i++)
                    {
                        results.Add(new StoryDriftData
                        {
                            Story = story[i],
                            OutputCase = loadCase[i],
                            Direction = direction[i],
                            Drift = drift[i]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ETABS veri çekme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sapModel != null) sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }

            return results;
        }

        // Sonuçları CSV dosyasına kaydet
        private void SaveGoreliKatResults(GoreliKatResult result)
        {
            try
            {
                string fileName = $"GoreliKat_Sonuc_{DateTime.Now:yyyyMMdd}.csv";
                string filePath = Path.Combine(Application.StartupPath, fileName);

                // CSV formatında kaydet - UTF-8 BOM (semboller için)
                using (var sw = new StreamWriter(filePath, false, new System.Text.UTF8Encoding(true)))
                {
                    // Başlık satırı
                    sw.WriteLine("Kat;Kombinasyon;Doğrultu;Drift;λδi/hi;Limit;Durum");

                    // Verileri sırala ve yaz (önce X, sonra Y)
                    var sortedItems = result.Items.OrderBy(x => x.Direction == "X" ? 0 : 1).ThenBy(x => x.Story).ToList();
                    foreach (var item in sortedItems)
                    {
                        sw.WriteLine($"{item.Story};{item.OutputCase};{item.Direction};{item.Drift:0.00000};{item.LambdaDrift:0.00000};{item.Limit:0.00000};{(item.IsOK ? "✅" : "❌")}");
                    }
                }

                MessageBox.Show($"Sonuçlar kaydedildi:\n{filePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
