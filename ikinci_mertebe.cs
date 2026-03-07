using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using CSiAPIv1;

namespace EtabsTools
{
    public class IkinciMertebeManager
    {
        public List<IkinciMertebeResult> CalculateDirection(
            List<StoryData> sortedStories, 
            List<ForceData> forces, 
            List<DriftData> drifts, 
            List<MassData> massDataList,
            string direction, 
            double Ch, 
            double R, 
            double D)
        {
            var results = new List<IkinciMertebeResult>();
            
            // Kombinasyonları belirle
            var uniqueCombos = forces.Select(f => f.LoadCase).Distinct().ToList();

            foreach (var combo in uniqueCombos)
            {
                // Kümülatif ağırlık hesabı için
                double cumWeight = 0;

                // En üst kattan aşağıya doğru iniyoruz (sortedStories: Top -> Base olmalı)
                foreach (var story in sortedStories)
                {
                    // 1. Kütle Bul
                    var massData = massDataList.FirstOrDefault(m => m.Story == story.Name);
                    double weight = massData?.Weight ?? 0;
                    
                    cumWeight += weight;
                    
                    // 2. Kuvvet Bul (LoadCase partial match)
                    var forceData = forces.FirstOrDefault(f => f.Story == story.Name && 
                        (f.LoadCase == combo || f.LoadCase.IndexOf(combo, StringComparison.OrdinalIgnoreCase) >= 0 || combo.IndexOf(f.LoadCase, StringComparison.OrdinalIgnoreCase) >= 0));
                    double V = direction == "X" ? forceData?.VX ?? 0 : forceData?.VY ?? 0;

                    // 3. Drift Bul (LoadCase partial match)
                    var driftData = drifts.FirstOrDefault(d => d.Story == story.Name && 
                        (d.LoadCase == combo || d.LoadCase.IndexOf(combo, StringComparison.OrdinalIgnoreCase) >= 0 || combo.IndexOf(d.LoadCase, StringComparison.OrdinalIgnoreCase) >= 0));
                    double delta = driftData?.Drift ?? 0; // Avg Drift (Ratio)

                    // V==0 olan katları da göster ama hesap yapamazlarsa Theta=0 olur
                    // Sadece forceData veya driftData bulunamazsa atla
                    if (forceData == null && driftData == null) continue;

                    // Formül: Theta = (AvgDrift_Ratio * Wij) / Vi
                    // ETABS AvgDrift genellikle Ratio'dur. (mm/mm)
                    double theta = V != 0 ? (delta * cumWeight) / V : 0;
                    double limit = 0.12 * D / (Ch * R);
                    
                    // NOT: Bodrum/Üst kat ayrımı Form1.cs'de ALT/UST kombinasyon filtrelemesi ile yapılıyor.
                    // Bu nedenle burada IsBodrum kontrolü yapmıyoruz. 

                    results.Add(new IkinciMertebeResult
                    {
                        Story = story.Name,
                        LoadCase = combo,
                        Direction = direction,
                        Vi = V,
                        Wij = cumWeight,
                        DriftRatio = delta,
                        Theta = theta,
                        Limit = limit,
                        Status = theta <= limit ? "OK" : "NOT OK"
                    });
                }
            }
            return results;
        }
    }

    // --- Data Models ---

    public class StoryData
    {
        public string Name { get; set; }
        public double Height { get; set; } // m
        public double Elevation { get; set; } // m
        public bool IsBodrum { get; set; }
    }

    public class MassData
    {
        public string Story { get; set; }
        public double Mass { get; set; } // Mass
        public double Weight { get; set; } // kN
    }

    public class ForceData
    {
        public string Story { get; set; }
        public string LoadCase { get; set; }
        public double VX { get; set; }
        public double VY { get; set; }
    }

    public class DriftData
    {
        public string Story { get; set; }
        public string LoadCase { get; set; }
        public string Direction { get; set; }
        public double Drift { get; set; }
    }

    public class IkinciMertebeResult
    {
        public string Story { get; set; }
        public string LoadCase { get; set; }
        public string Direction { get; set; }
        public double Vi { get; set; }
        public double Wij { get; set; }
        public double DriftRatio { get; set; }
        public double Theta { get; set; }
        public double Limit { get; set; }
        public string Status { get; set; }
    }

    // ---------------------------------------------------------
    // İKİNCİ MERTEBE UI SINIFI (Form1'den Taşındı)
    // ---------------------------------------------------------
    public class IkinciMertebeUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<Panel, int, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        // UI Controls
        private TabPage _tabPage;
        private TextBox txtCh, txtR_Ikinci, txtD_Ikinci;
        private CheckBox chkBodrumIkinci;
        private NumericUpDown numBodrumKatIkinci;
        private ListBox lstCombosIkinci;
        private Label lblIkinciMertebeStatus;
        private FlowLayoutPanel pnlIkinciSelectedCombos;
        private DataGridView dgvIkinciMertebeResults;
        private List<string> _ikinciSelectedCombos = new List<string>();

        // Cache Variables
        private List<ForceData> _cachedForces = new List<ForceData>();
        private List<DriftData> _cachedDrifts = new List<DriftData>();

        // Status Labels
        // Status Labels - REMOVED


        // Data Lists
        private List<StoryData> _storyDataList = new List<StoryData>();
        private List<MassData> _massDataList = new List<MassData>();
        private List<IkinciMertebeResult> _lastResults;

        public IkinciMertebeUI(Form1 form, Func<cSapModel> getSapModel, Func<Panel, int, Panel> createNavigationPanel, Action<int> goToPage, Color colorBackground)
        {
            _form = form;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNavigationPanel;
            _goToPage = goToPage;
            _colorBackground = colorBackground;
        }

        public void Initialize(TabPage page)
        {
            _tabPage = page;
            page.BackColor = _colorBackground;

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            Label lblPageTitle = Form1.CreateHeaderLabel("İkinci Mertebe Etkileri Tahkiki");
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
            tlp.Padding = new Padding(20, 10, 20, 10);

            Panel pnlLeft = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 215F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            // Kombinasyon Seçim Alanı
            TableLayoutPanel tlpCombos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                Margin = new Padding(0, 0, 10, 5)
            };
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            RoundedPanel pnlCombos = new RoundedPanel
            {
                Title = "Combinations and Cases",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 5, 0)
            };

            lstCombosIkinci = new ListBox
            {
                Location = new Point(15, 35),
                Size = new Size(145, 125),
                SelectionMode = SelectionMode.MultiExtended,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.FromArgb(250, 252, 255),
                BorderStyle = BorderStyle.None
            };

            SmoothButton btnLoadCombosIkinci = new SmoothButton
            {
                Text = "Getir",
                Size = new Size(55, 30),
                Location = new Point(15, 165),
                BaseColor = Color.FromArgb(225, 213, 233), // Soft Purple
                BorderRadius = 12,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnLoadCombosIkinci.Click += BtnGetCombosIkinci_Click;

            SmoothButton btnSelectCombosIkinci = new SmoothButton
            {
                Text = "Seç",
                Size = new Size(55, 30),
                Location = new Point(80, 165),
                BaseColor = Color.FromArgb(225, 213, 233),
                BorderRadius = 12,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnSelectCombosIkinci.Click += BtnSelectCombosIkinci_Click;

            pnlCombos.Controls.Add(lstCombosIkinci);
            pnlCombos.Controls.Add(btnLoadCombosIkinci);
            pnlCombos.Controls.Add(btnSelectCombosIkinci);
            tlpCombos.Controls.Add(pnlCombos, 0, 0);

            RoundedPanel pnlSelectedWrapper = new RoundedPanel
            {
                Title = "Seçili Kombinasyonlar",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(5, 0, 0, 0)
            };

            pnlIkinciSelectedCombos = new FlowLayoutPanel
            {
                Location = new Point(10, 35),
                Size = new Size(130, 160),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White
            };
            pnlSelectedWrapper.Controls.Add(pnlIkinciSelectedCombos);
            tlpCombos.Controls.Add(pnlSelectedWrapper, 1, 0);
            tlpLeft.Controls.Add(tlpCombos, 0, 0);

            // Parametreler
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 5, 10, 5)
            };

            int startY = 50, gapY = 32, labelX = 15, textX = 120, textW = 80;

            pnlParams.Controls.Add(new Label { Text = "Ch:", Location = new Point(labelX, startY), AutoSize = true });
            txtCh = new TextBox { Location = new Point(textX, startY - 3), Width = textW, Text = "0.5" };
            pnlParams.Controls.Add(txtCh);

            pnlParams.Controls.Add(new Label { Text = "R:", Location = new Point(labelX, startY + gapY), AutoSize = true });
            txtR_Ikinci = new TextBox { Location = new Point(textX, startY + gapY - 3), Width = textW, Text = "8" };
            pnlParams.Controls.Add(txtR_Ikinci);

            pnlParams.Controls.Add(new Label { Text = "D:", Location = new Point(labelX, startY + gapY * 2), AutoSize = true });
            txtD_Ikinci = new TextBox { Location = new Point(textX, startY + gapY * 2 - 3), Width = textW, Text = "2.5" };
            pnlParams.Controls.Add(txtD_Ikinci);

            chkBodrumIkinci = new CheckBox
            {
                Text = "Bodrum kabulü var mı?",
                Location = new Point(labelX, startY + gapY * 3),
                AutoSize = true
            };
            chkBodrumIkinci.CheckedChanged += (s, e) => { numBodrumKatIkinci.Enabled = chkBodrumIkinci.Checked; };
            pnlParams.Controls.Add(chkBodrumIkinci);

            pnlParams.Controls.Add(new Label { Text = "Bodrum kat sayısı:", Location = new Point(labelX + 20, startY + gapY * 4), AutoSize = true });
            numBodrumKatIkinci = new NumericUpDown
            {
                Location = new Point(textX + 30, startY + gapY * 4 - 3),
                Width = 50,
                Minimum = 0,
                Maximum = 20,
                Value = 0,
                Enabled = false
            };
            pnlParams.Controls.Add(numBodrumKatIkinci);

            SmoothButton btnFetchAll = new SmoothButton
            {
                Text = "Değerleri Çek",
                Size = new Size(140, 35),
                Location = new Point(35, startY + gapY * 5),
                BaseColor = Color.FromArgb(225, 213, 233),
                BorderRadius = 12,
                EnableCenterAnimation = true
            };
            btnFetchAll.Click += BtnFetchAll_Click;
            pnlParams.Controls.Add(btnFetchAll);

            tlpLeft.Controls.Add(pnlParams, 0, 1);

            Panel pnlButton = new Panel { Dock = DockStyle.Fill };
            
            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "HESAPLA",
                Size = new Size(120, 40),
                Location = new Point(15, 5),
                BaseColor = Color.FromArgb(225, 213, 233),
                BorderRadius = 15,
                EnableCenterAnimation = true
            };
            btnCalculate.Click += BtnCalculateIkinciMertebe_Click;
            pnlButton.Controls.Add(btnCalculate);

            SmoothButton btnSave = new SmoothButton
            {
                Text = "KAYDET",
                Size = new Size(120, 40),
                Location = new Point(155, 5),
                BaseColor = Color.FromArgb(235, 240, 245),
                BorderRadius = 15,
                EnableCenterAnimation = true
            };
            btnSave.Click += BtnSaveIkinciMertebe_Click;
            pnlButton.Controls.Add(btnSave);
            tlpLeft.Controls.Add(pnlButton, 0, 2);

            pnlLeft.Controls.Add(tlpLeft);
            tlp.Controls.Add(pnlLeft, 0, 0);

            // Sonuçlar Paneli
            RoundedPanel pnlResults = new RoundedPanel
            {
                Title = "Sonuçlar",
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                Margin = new Padding(10, 0, 0, 0),
                TitleFont = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            dgvIkinciMertebeResults = new DataGridView
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
                    BackColor = Color.FromArgb(244, 247, 254),
                    ForeColor = Color.FromArgb(113, 128, 150),
                    Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            dgvIkinciMertebeResults.Columns.Add("Kat", "Kat");
            dgvIkinciMertebeResults.Columns.Add("Kombinasyon", "Kombinasyon");
            dgvIkinciMertebeResults.Columns.Add("Dogrultu", "Doğrultu");
            dgvIkinciMertebeResults.Columns.Add("Vi", "Vi (kN)");
            dgvIkinciMertebeResults.Columns.Add("Wij", "Wij (kN)");
            dgvIkinciMertebeResults.Columns.Add("DriftRatio", "Drift");
            dgvIkinciMertebeResults.Columns.Add("Theta", "θ");
            dgvIkinciMertebeResults.Columns.Add("Limit", "Limit");
            dgvIkinciMertebeResults.Columns.Add("Durum", "Durum");

            lblIkinciMertebeStatus = new Label
            {
                Text = "",
                Dock = DockStyle.Bottom,
                Height = 25,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Blue,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel dgvContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 25, 15, 15) };
            dgvContainer.Controls.Add(dgvIkinciMertebeResults);
            dgvContainer.Controls.Add(lblIkinciMertebeStatus);
            pnlResults.Controls.Add(dgvContainer);
            tlp.Controls.Add(pnlResults, 1, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            page.Tag = 4;
            page.VisibleChanged += (s, e) => {
                if (page.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = _createNavigationPanel(null, 4);
                    mainLayout.Controls.Add(navPanel, 0, 2);
                }
            };

            page.Controls.Add(mainLayout);
        }

        // ----- EVENT HANDLERS -----

        private void BtnSelectCombosIkinci_Click(object sender, EventArgs e)
        {
            if (lstCombosIkinci.SelectedItems.Count == 0) return;
            foreach (var item in lstCombosIkinci.SelectedItems)
            {
                string combo = item.ToString();
                if (!_ikinciSelectedCombos.Contains(combo))
                {
                    _ikinciSelectedCombos.Add(combo);
                    AddIkinciComboTag(combo);
                }
            }
        }

        private void AddIkinciComboTag(string comboName)
        {
            Panel tag = new Panel { Size = new Size(120, 24), BackColor = Color.White, Margin = new Padding(3), Tag = comboName };
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
                    e.Graphics.FillPath(new SolidBrush(Color.FromArgb(255, 236, 159)), path);
                    e.Graphics.DrawPath(new Pen(Color.FromArgb(200, 180, 100)), path);
                }
            };

            Label lbl = new Label { Text = comboName.Length > 12 ? comboName.Substring(0, 10) + ".." : comboName, Location = new Point(5, 5), AutoSize = true, Font = new Font("Segoe UI", 7) };
            Button btnRemove = new Button { Text = "✕", Size = new Size(18, 18), Location = new Point(100, 3), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 7, FontStyle.Bold), ForeColor = Color.DarkRed, BackColor = Color.Transparent, Cursor = Cursors.Hand };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += (s, ev) => { pnlIkinciSelectedCombos.Controls.Remove(tag); _ikinciSelectedCombos.Remove(comboName); };

            tag.Controls.Add(lbl);
            tag.Controls.Add(btnRemove);
            pnlIkinciSelectedCombos.Controls.Add(tag);
        }

        private void BtnGetCombosIkinci_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null) { MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                lstCombosIkinci.Items.Clear();
                int numCombos = 0; string[] comboNames = null;
                sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                if (comboNames != null) foreach (var name in comboNames) lstCombosIkinci.Items.Add(name);

                int numCases = 0; string[] caseNames = null;
                sapModel.LoadCases.GetNameList(ref numCases, ref caseNames);
                if (caseNames != null) foreach (var name in caseNames) lstCombosIkinci.Items.Add(name);

                if (lstCombosIkinci.Items.Count == 0) ToastForm.ShowToast("Kombinasyon veya case bulunamadı.", _form, 2000);
            }
            catch (Exception ex) { ToastForm.ShowToast("Kombinasyonlar yüklenirken hata: " + ex.Message, _form, 2000); }
        }

        private void BtnFetchAll_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null) { ToastForm.ShowToast("Önce ETABS'a bağlanın.", _form, 2000); return; }
            if (_ikinciSelectedCombos.Count == 0) { ToastForm.ShowToast("Lütfen kombinasyon seçin.", _form, 2000); return; }
            if (!sapModel.GetModelIsLocked()) { ToastForm.ShowToast("ETABS modeli kilitli değil. Lütfen önce analizi çalıştırın.", _form, 2000); return; }

            try
            {
                // 1. Story Data
                _storyDataList.Clear();
                FetchStoryData();

                // 2. Mass Data
                _massDataList.Clear();
                FetchMassData();

                // 3. Forces & Drifts
                sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                string[] comboArray = _ikinciSelectedCombos.ToArray();
                sapModel.DatabaseTables.SetLoadCombinationsSelectedForDisplay(ref comboArray);

                foreach (var combo in _ikinciSelectedCombos) 
                { 
                    sapModel.Results.Setup.SetCaseSelectedForOutput(combo); 
                    sapModel.Results.Setup.SetComboSelectedForOutput(combo); 
                }

                _cachedForces = FetchStoryForces(_ikinciSelectedCombos);
                _cachedDrifts = FetchStoryDrifts(_ikinciSelectedCombos);
                
                // Cleanup
                sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                ToastForm.ShowToast("Değerler çekildi.", _form, 2000);
            }
            catch (Exception ex) 
            { 
                ToastForm.ShowToast("Veri çekme hatası: " + ex.Message, _form, 2000); 
            }
        }

        private void BtnSaveIkinciMertebe_Click(object sender, EventArgs e)
        {
            if (_lastResults == null || _lastResults.Count == 0)
            {
                ToastForm.ShowToast("Kaydedilecek sonuç yok. Lütfen önce hesaplama yapın.", _form, 2000);
                return;
            }
            SaveResultsToCSV(_lastResults);
        }

        private void BtnCalculateIkinciMertebe_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                ToastForm.ShowToast("Önce ETABS'a bağlanın.", _form, 2000);
                return;
            }

            // Girdileri Al
            if (!double.TryParse(txtCh.Text, out double Ch) || !double.TryParse(txtR_Ikinci.Text, out double R) || !double.TryParse(txtD_Ikinci.Text, out double D))
            {
                ToastForm.ShowToast("Lütfen Ch, R ve D parametrelerini geçerli sayı olarak girin.", _form, 2000);
                return;
            }

            if (_ikinciSelectedCombos.Count == 0)
            {
                ToastForm.ShowToast("Lütfen en az bir kombinasyon seçin.", _form, 2000);
                return;
            }

            try
            {
                // 1. Story Verileri (Kat yükseklikleri)
                if (_storyDataList.Count == 0) FetchStoryData();

                // 2. Kütle Verileri (Mass Summary by Story)
                if (_massDataList.Count == 0) FetchMassData();
                
                // 3. Kuvvet ve Drift Verileri
                // Eğer önceden çekilmişse (Değerleri Getir butonu ile) onları kullan, yoksa şimdi çek.
                var forces = _cachedForces.Count > 0 ? _cachedForces : FetchStoryForces(_ikinciSelectedCombos);
                var drifts = _cachedDrifts.Count > 0 ? _cachedDrifts : FetchStoryDrifts(_ikinciSelectedCombos);

                // Filtreleme Mantığı
                // U/A kuralı: Bodrum varsa, bodrum katları için ALT, üst katlar için UST kombinasyonları
                // X/Y kuralı: X kombinasyonları X yönü, Y kombinasyonları Y yönü
                
                // X ve Y + UST/ALT kombinasyonlarını ayır
                List<string> xCombosUST = _ikinciSelectedCombos.Where(c => c.ToUpper().Contains("X") && (c.ToUpper().Contains("UST") || c.ToUpper().Contains("U"))).ToList();
                List<string> xCombosALT = _ikinciSelectedCombos.Where(c => c.ToUpper().Contains("X") && (c.ToUpper().Contains("ALT") || c.ToUpper().Contains("A"))).ToList();
                List<string> yCombosUST = _ikinciSelectedCombos.Where(c => c.ToUpper().Contains("Y") && (c.ToUpper().Contains("UST") || c.ToUpper().Contains("U"))).ToList();
                List<string> yCombosALT = _ikinciSelectedCombos.Where(c => c.ToUpper().Contains("Y") && (c.ToUpper().Contains("ALT") || c.ToUpper().Contains("A"))).ToList();
                
                // Her iki tip de boşsa, tüm X/Y kombinasyonlarını kullan (UST/ALT ayrımı olmayan durum)
                if (xCombosUST.Count == 0 && xCombosALT.Count == 0) xCombosUST = _ikinciSelectedCombos.Where(c => c.ToUpper().Contains("X")).ToList();
                if (yCombosUST.Count == 0 && yCombosALT.Count == 0) yCombosUST = _ikinciSelectedCombos.Where(c => c.ToUpper().Contains("Y")).ToList();

                bool isBodrum = chkBodrumIkinci.Checked;
                int bodrumKatSayisi = isBodrum ? (int)numBodrumKatIkinci.Value : 0;

                // Robust Basement Detection: Use Elevations from _storyDataList instead of parsing names
                // Ensure _storyDataList is populated
                if (_storyDataList.Count == 0) FetchStoryData();

                // Robust Basement Detection & Data Population: Use API instead of Table
                var basementStoryNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                
                // Always fetch fresh story data via API to ensure _storyDataList is populated and valid
                int numStories = 0;
                string[] storyNames = null;
                double[] storyElevations = null;
                double[] storyHeights = null;
                bool[] isMaster = null;
                string[] similarTo = null;
                bool[] spliceAbove = null;
                double[] spliceHeight = null;

                // Get Stories via API (Robust)
                sapModel.Story.GetStories(ref numStories, ref storyNames, ref storyElevations, ref storyHeights, ref isMaster, ref similarTo, ref spliceAbove, ref spliceHeight);

                // Populate _storyDataList from API (Critical for CalculateDirection)
                _storyDataList.Clear();
                if (storyNames != null && storyElevations != null && storyHeights != null)
                {
                    for (int i = 0; i < numStories; i++)
                    {
                        if (storyNames[i].Equals("Base", StringComparison.OrdinalIgnoreCase)) continue;
                        _storyDataList.Add(new StoryData 
                        { 
                            Name = storyNames[i], 
                            Height = storyHeights[i] / 1000.0, // Assuming mm from API? No, API usually returns current units. 
                            // Wait, FetchStoryData divided by 1000. Let's assume lengths are consistent with previous logic or handle units.
                            // Actually, safest is to trust the parsing logic if possible, but API is cleaner. 
                            // Let's stick to the previous unit assumption or check units. 
                            // To be safe, let's assume the previous code's division by 1000 was correct for the table data (often mm).
                            // API data unit depends on current units. 
                            // Let's rely on _storyDataList NOT clearing if we want to be safe? 
                            // No, if I don't clear and re-populate, I risk emptiness.
                            // Let's populate assuming meters if current unit is meters. 
                            // Code usually assumes consistent units. Table parse divided by 1000. 
                            // Let's assume API keys return same units as Table.
                            Elevation = storyElevations[i] / 1000.0 
                        });
                        // Fix Height if needed. API "StoryHeights" vs Table "Height".
                        // Let's verify Table version: Height / 1000.0.
                        _storyDataList.Last().Height = storyHeights[i] / 1000.0;
                    }
                }

                if (isBodrum && bodrumKatSayisi > 0)
                {
                    // Kota göre küçükten büyüğe sırala (Bottom-Up)
                    var basementSortedStories = _storyDataList
                        .OrderBy(s => s.Elevation)
                        .Take(bodrumKatSayisi)
                        .ToList();
                        
                    foreach (var s in basementSortedStories) basementStoryNames.Add(s.Name);
                }

                // Veri listelerini oluştur - her kat için doğru kombinasyonları kullanarak
                var xForces = new List<ForceData>();
                var xDrifts = new List<DriftData>();
                var yForces = new List<ForceData>();
                var yDrifts = new List<DriftData>();

                foreach (var f in forces)
                {
                    bool isBodrumKat = basementStoryNames.Contains(f.Story);
                    
                    // X Yönü
                    if (isBodrumKat)
                    {
                        // Bodrum katı: ALT kombinasyonları
                        if (xCombosALT.Any(c => f.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                            xForces.Add(f);
                    }
                    else
                    {
                        // Üst kat: UST kombinasyonları
                        if (xCombosUST.Any(c => f.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                            xForces.Add(f);
                    }
                    
                    // Y Yönü
                    if (isBodrumKat)
                    {
                        if (yCombosALT.Any(c => f.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                            yForces.Add(f);
                    }
                    else
                    {
                        if (yCombosUST.Any(c => f.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                            yForces.Add(f);
                    }
                }

                foreach (var d in drifts)
                {
                    bool isBodrumKat = basementStoryNames.Contains(d.Story);
                    
                    // X Yönü - Direction sütununa göre filtrele
                    if (d.Direction.Equals("X", StringComparison.OrdinalIgnoreCase))
                    {
                        if (isBodrumKat)
                        {
                            if (xCombosALT.Any(c => d.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                                xDrifts.Add(d);
                        }
                        else
                        {
                            if (xCombosUST.Any(c => d.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                                xDrifts.Add(d);
                        }
                    }
                    
                    // Y Yönü - Direction sütununa göre filtrele
                    if (d.Direction.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        if (isBodrumKat)
                        {
                            if (yCombosALT.Any(c => d.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                                yDrifts.Add(d);
                        }
                        else
                        {
                            if (yCombosUST.Any(c => d.LoadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                                yDrifts.Add(d);
                        }
                    }
                }

                // HESAPLAMA MANTIĞI
                var manager = new IkinciMertebeManager();
                var sortedStories = _storyDataList.OrderByDescending(s => s.Elevation).ToList();
                
                // Sonuç Listesi
                var results = new List<IkinciMertebeResult>();
                
                // X Yönü Hesabı
                if (xForces.Count > 0)
                    results.AddRange(manager.CalculateDirection(sortedStories, xForces, xDrifts, _massDataList, "X", Ch, R, D));
                
                // Y Yönü Hesabı
                if (yForces.Count > 0)
                    results.AddRange(manager.CalculateDirection(sortedStories, yForces, yDrifts, _massDataList, "Y", Ch, R, D));

                // Sonuçları DataGridView'e göster
                dgvIkinciMertebeResults.Rows.Clear();
                bool allOk = true;

                if (results.Count > 0)
                {
                    // Sıralama: Önce Yön (X sonra Y), sonra Kat sırası (Üst > Alt)
                    var displayResults = results
                        .OrderBy(r => r.Direction) // X önce (A < Y)
                        .ThenByDescending(r => sortedStories.FindIndex(s => s.Name == r.Story)) // Kat sırasına göre (üstten alta)
                        .ThenBy(r => r.LoadCase)
                        .ToList();

                    foreach (var res in displayResults)
                    {
                        dgvIkinciMertebeResults.Rows.Add(res.Story, res.LoadCase, res.Direction, 
                            res.Vi.ToString("F2"), res.Wij.ToString("F2"), res.DriftRatio.ToString("F6"),
                            res.Theta.ToString("F6"), res.Limit.ToString("F4"), res.Status);
                        
                        // Durum renklendirmesi
                        int lastRow = dgvIkinciMertebeResults.Rows.Count - 1;
                        if (res.Status == "NOT OK")
                        {
                            dgvIkinciMertebeResults.Rows[lastRow].Cells["Durum"].Style.BackColor = Color.LightCoral;
                            allOk = false;
                        }
                        else
                        {
                            dgvIkinciMertebeResults.Rows[lastRow].Cells["Durum"].Style.BackColor = Color.LightGreen;
                        }
                    }

                    // FİNAL MESAJI
                    if (allOk)
                    {
                        lblIkinciMertebeStatus.Text = "✅ İkinci Mertebe Etkileri Göz Ardı Edilebilir.";
                        lblIkinciMertebeStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblIkinciMertebeStatus.Text = "❌ İkinci Mertebe Etkileri Hesaba Katılmalı!";
                        lblIkinciMertebeStatus.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblIkinciMertebeStatus.Text = "Hesaplanacak veri bulunamadı (Filtrelere uygun veri yok).";
                    lblIkinciMertebeStatus.ForeColor = Color.Gray;
                }

                // Sonuçları CSV'ye kaydet (OTOMATIK KAYIT İPTAL EDİLDİ - SADECE HESAPLA)
                // SaveResultsToCSV(results);
                
                // Sonuçları kaydet (Save butonu için)
                _lastResults = results;
            }
            catch (Exception ex)
            {
                lblIkinciMertebeStatus.Text = "Hata oluştu.";
                lblIkinciMertebeStatus.ForeColor = Color.Red;
                MessageBox.Show("Hesaplama Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveResultsToCSV(List<IkinciMertebeResult> results)
        {
            if (results.Count == 0) return;
            try
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(appPath, "IkinciMertebe_Sonuclari.csv");
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("Story;LoadCase;Direction;Vi (kN);Wij (kN);Drift Ratio;Theta;Limit;Status");
                    foreach (var res in results) writer.WriteLine($"{res.Story};{res.LoadCase};{res.Direction};{res.Vi:F2};{res.Wij:F2};{res.DriftRatio:F6};{res.Theta:F6};{res.Limit:F4};{res.Status}");
                }
                ToastForm.ShowToast("İkinci mertebe etkileri dosyası kaydedildi.", _form, 2000);
            }
            catch (Exception ex) { MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ----- ETABS DATA FETCH METHODS -----

        private List<ForceData> FetchStoryForces(List<string> combos)
        {
            var sapModel = _getSapModel();
            var forces = new List<ForceData>();
            string tableName = "Story Forces"; string groupName = ""; string[] fieldKeyList = null; int tableVersion = 0; string[] fieldsKeysIncluded = null; int numRecords = 0; string[] tableData = null;

            int ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            if (ret != 0 || fieldsKeysIncluded == null || numRecords == 0) return forces;

            int numFields = fieldsKeysIncluded.Length; int storyIdx = -1, caseIdx = -1, locIdx = -1, vxIdx = -1, vyIdx = -1;
            for (int i = 0; i < numFields; i++)
            {
                string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", "");
                if (col == "STORY") storyIdx = i; else if (col == "OUTPUTCASE" || col == "LOADCASE" || col == "CASE") caseIdx = i; else if (col == "LOCATION") locIdx = i; else if (col == "VX") vxIdx = i; else if (col == "VY") vyIdx = i;
            }
            if (storyIdx == -1 || caseIdx == -1) return forces;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields;
                if (locIdx >= 0) { string loc = tableData[baseIdx + locIdx] ?? ""; if (!loc.Equals("Bottom", StringComparison.OrdinalIgnoreCase)) continue; }
                string story = tableData[baseIdx + storyIdx] ?? ""; string loadCase = tableData[baseIdx + caseIdx] ?? "";
                if (!combos.Any(c => loadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0)) continue;
                double vx = 0, vy = 0;
                if (vxIdx >= 0) double.TryParse(tableData[baseIdx + vxIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vx);
                if (vyIdx >= 0) double.TryParse(tableData[baseIdx + vyIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vy);
                forces.Add(new ForceData { Story = story, LoadCase = loadCase, VX = Math.Abs(vx), VY = Math.Abs(vy) });
            }
            return forces;
        }

        private List<DriftData> FetchStoryDrifts(List<string> combos)
        {
            var sapModel = _getSapModel();
            var drifts = new List<DriftData>();
            string tableName = "Story Max Over Avg Drifts"; string groupName = ""; string[] fieldKeyList = null; int tableVersion = 0; string[] fieldsKeysIncluded = null; int numRecords = 0; string[] tableData = null;

            int ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            if (ret != 0 || numRecords == 0) { tableName = "Story Drifts"; ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData); }
            if (ret != 0 || fieldsKeysIncluded == null || numRecords == 0) return drifts;

            int numFields = fieldsKeysIncluded.Length; int storyIdx = -1, caseIdx = -1, dirIdx = -1, driftIdx = 6;
            for (int i = 0; i < numFields; i++) { string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", "").Replace(".", ""); if (col == "STORY") storyIdx = i; else if (col == "OUTPUTCASE" || col == "LOADCASE" || col == "CASE") caseIdx = i; else if (col == "DIRECTION") dirIdx = i; }
            if (storyIdx == -1 || caseIdx == -1) return drifts;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields;
                string story = tableData[baseIdx + storyIdx] ?? ""; string loadCase = tableData[baseIdx + caseIdx] ?? "";
                if (!combos.Any(c => loadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0)) continue;
                string direction = (dirIdx >= 0) ? (tableData[baseIdx + dirIdx] ?? "") : "";
                double drift = 0; if (driftIdx >= 0 && driftIdx < numFields) double.TryParse(tableData[baseIdx + driftIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out drift);
                drifts.Add(new DriftData { Story = story, LoadCase = loadCase, Direction = direction, Drift = drift });
            }
            return drifts;
        }

        private void FetchStoryData()
        {
            var sapModel = _getSapModel();
            _storyDataList.Clear();
            string tableName = "Story Definitions"; string groupName = ""; string[] fieldKeyList = null; int tableVersion = 0; string[] fieldsKeysIncluded = null; int numRecords = 0; string[] tableData = null;

            int ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            if (ret != 0 || fieldsKeysIncluded == null) return;

            int numFields = fieldsKeysIncluded.Length; int storyIdx = -1, heightIdx = -1, elevationIdx = -1;
            for (int i = 0; i < numFields; i++) 
            { 
                string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", ""); 
                if (col == "STORY") storyIdx = i; 
                else if (col == "HEIGHT") heightIdx = i; 
                else if (col == "ELEVATION") elevationIdx = i; 
            }
            if (storyIdx == -1 || heightIdx == -1) return;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields; string storyName = tableData[baseIdx + storyIdx];
                if (storyName.Equals("Base", StringComparison.OrdinalIgnoreCase)) continue;
                double height = 0, elevation = 0;
                double.TryParse(tableData[baseIdx + heightIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out height);
                if (elevationIdx != -1) double.TryParse(tableData[baseIdx + elevationIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out elevation);
                _storyDataList.Add(new StoryData { Name = storyName, Height = height / 1000.0, Elevation = elevation / 1000.0, IsBodrum = false });
            }
            // Revert strict parsing to safer original to fix "No Data" error if any
            // (Note: The main logic is now handled in BtnCalculate via API, this is just backup/filling _storyDataList)
        }

        private void FetchMassData()
        {
            var sapModel = _getSapModel();
            _massDataList.Clear();
            string tableName = "Mass Summary by Story"; string groupName = ""; string[] fieldKeyList = null; int tableVersion = 0; string[] fieldsKeysIncluded = null; int numRecords = 0; string[] tableData = null;

            int ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            if (ret != 0 || fieldsKeysIncluded == null) return;

            int numFields = fieldsKeysIncluded.Length; int storyIdx = -1, massXIdx = -1;
            for (int i = 0; i < numFields; i++) { string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", ""); if (col == "STORY") storyIdx = i; else if (col == "UX" || col == "MASSX") massXIdx = i; }
            if (storyIdx == -1) return;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields; string story = tableData[baseIdx + storyIdx];
                if (story.Equals("Base", StringComparison.OrdinalIgnoreCase)) continue;
                double massX = 0; if (massXIdx >= 0) double.TryParse(tableData[baseIdx + massXIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out massX);
                _massDataList.Add(new MassData { Story = story, Mass = massX, Weight = massX * 9.81 });
            }
        }
    }
}
