using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Forms;
using CSiAPIv1;

namespace EtabsTools
{
    // --- Veri Yapıları ---
    public class ColumnForceData
    {
        public string Story { get; set; }
        public string Column { get; set; }
        public string UniqueName { get; set; } // Eşleştirme Anahtarı
        public string LoadCase { get; set; }
        public string Location { get; set; }
        public double P { get; set; }
    }

    public class FrameAssignmentData
    {
        public string UniqueName { get; set; } // Eşleştirme Anahtarı
        public string Story { get; set; }
        public string Label { get; set; }
        public string SectionName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Area { get; set; }
    }

    public class KolonEksenelYukResult
    {
        public string Story { get; set; }
        public string Column { get; set; }
        public string UniqueName { get; set; } // Excel'deki gibi Unique Name sütunu
        public string LoadCase { get; set; }
        public string Location { get; set; }
        public double Nd { get; set; }         // P (kN)
        public string Section { get; set; }
        public double B { get; set; }
        public double D { get; set; }
        public double Ac { get; set; }
        public double AcFck { get; set; }
        public double Limit { get; set; }
        public double Fck { get; set; }
        public double NdRatio { get; set; }
        public bool IsOK { get; set; }
        public string Status { get; set; }
    }

    // --- MANAGER SINIFI ---
    public class KolonEksenelYukManager
    {
        public double Fck { get; set; }
        public double Limit { get; set; }
        public bool IsBodrum { get; set; }
        public int BodrumKatCount { get; set; }

        public KolonEksenelYukManager(double fck, double limit, bool isBodrum, int bodrumCount)
        {
            Fck = fck;
            Limit = limit;
            IsBodrum = isBodrum;
            BodrumKatCount = bodrumCount;
        }

        public List<KolonEksenelYukResult> Calculate(
        List<ColumnForceData> allForces,
        List<FrameAssignmentData> assignments,
        List<StoryData> stories)
        {
            var results = new List<KolonEksenelYukResult>();

            // 0. Bodrum Katlarını Belirle (Elevation Sıralaması)
            // Eğer stories null ise boş liste kabul et
            if (stories == null) stories = new List<StoryData>();

            var sortedStories = stories.OrderBy(s => s.Elevation).ToList();
            var bodrumStories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (IsBodrum && BodrumKatCount > 0)
            {
                for (int i = 0; i < sortedStories.Count; i++)
                {
                    if (i < BodrumKatCount)
                    {
                        bodrumStories.Add(sortedStories[i].Name);
                    }
                }
            }

            // 1. Frame Assignment Verilerini Story + Label'a göre sözlüğe al
            // Unique Name boş veya bozuk olduğu için Story + Label en güvenilir anahtardır.
            var assignmentsDict = assignments
                .GroupBy(a => new { a.Story, a.Label }) // Story ve Label'a göre grupla
                .ToDictionary(g => g.Key, g => g.First()); // İlkini al

            // 2. ANA DÖNGÜ: Filtrelenmiş Kuvvet Listesi (Element Forces)
            foreach (var force in allForces)
            {
                // A. Anahtar oluştur (Force tablosundaki Story ve Column)
                var key = new { Story = force.Story, Label = force.Column };

                // Bu kuvvete ait kolon özelliklerini bul
                if (!assignmentsDict.TryGetValue(key, out FrameAssignmentData frame))
                {
                    // Eşleşme yoksa atla
                    continue; 
                }

                // B. Kesit Boyutlarını Parse Et (C40X90 -> 40, 90)
                double b_cm = 0;
                double d_cm = 0;

                // Öncelik Frame Assignment'dan alınan kesit isminde
                string sectionName = frame.SectionName ?? ""; 
                
                var match = Regex.Match(sectionName, @"(\d+(?:[.,]\d+)?)\s*[xX*]\s*(\d+(?:[.,]\d+)?)");
                if (match.Success)
                {
                    string val1 = match.Groups[1].Value.Replace(",", ".");
                    string val2 = match.Groups[2].Value.Replace(",", ".");
                    b_cm = double.Parse(val1, CultureInfo.InvariantCulture);
                    d_cm = double.Parse(val2, CultureInfo.InvariantCulture);
                }
                else
                {
                    // Regex bulamazsa Frame tablosundaki nümerik width/height değerlerini kullan (mm -> cm)
                    b_cm = frame.Width / 10.0;
                    d_cm = frame.Height / 10.0;
                }

                double Ac_cm2 = b_cm * d_cm;
                double Ac_mm2 = Ac_cm2 * 100; // mm²

                // C. AH / UH Kombinasyon Filtresi
                bool isCurrentStoryBodrum = bodrumStories.Contains(force.Story);
                string loadCase = force.LoadCase.ToUpper();
                bool hideRow = false;

                // KURAL: 
                // 1. Bodrum Kabulü VARSA (IsBodrum = true):
                //    - Bodrum Katları: "U" içerenleri gizle (A ve Nötr görünsün).
                //    - Üst Katlar: "A" içerenleri gizle (U ve Nötr görünsün).
                // 2. Bodrum Kabulü YOKSA (IsBodrum = false):
                //    - HİÇBİR ŞEY GİZLEME (Hangi kombinasyon seçilirse seçilsin hepsi görünsün).

                bool hasAH = loadCase.Contains("A");
                bool hasUH = loadCase.Contains("U");

                if (IsBodrum)
                {
                    if (isCurrentStoryBodrum)
                    {
                        // Bodrumdayız -> UH İSTEMİYORUZ
                        if (hasUH) hideRow = true;
                    }
                    else
                    {
                        // Üst kattayız -> AH İSTEMİYORUZ
                        if (hasAH) hideRow = true;
                    }
                }
                else
                {
                    // Bodrum yok -> A (Alt) içerenleri GİZLE, diğerlerini (U/Nötr) göster.
                    if (hasAH) hideRow = true;
                }

                if (hideRow) continue;

                // D. Hesaplama
                // P değeri Element Forces tablosundan gelir (force.P)
                double Nd = force.P; // Orijinal işaretli değer (Excel uyumu için)
                double AbsNd = Math.Abs(Nd); // Hesap için mutlak değer

                // Ac * fck (N -> kN)
                // Ac_mm2 * Fck(N/mm2) = N
                // / 1000 => kN
                double AcFck_kN = (Ac_mm2 * Fck) / 1000.0;

                double ratio = 0;
                if (AcFck_kN > 0) ratio = AbsNd / AcFck_kN;

                // E. Sonuç Satırı
                string status = (ratio > Limit) ? "Limiti Aşıyor" : "Uygun";

                // Sonuç listesine ekle
                results.Add(new KolonEksenelYukResult
                {
                    Story = force.Story,       // Force tablosundaki Story
                    Column = force.Column,     // Force tablosundaki Column Adı
                    UniqueName = force.UniqueName,
                    LoadCase = force.LoadCase, // Force tablosundaki Kombinasyon
                    Location = force.Location, // Force tablosundaki Station
                    Nd = AbsNd,                // İSTEK: Mutlak değer yazdır
                    Section = sectionName,     // Frame Assignment'dan gelen kesit
                    B = b_cm,
                    D = d_cm,
                    Ac = Ac_cm2,
                    AcFck = AcFck_kN,
                    Limit = Limit,
                    Fck = Fck,
                    NdRatio = ratio,
                    IsOK = ratio <= Limit,
                    Status = status
                });
            }

            // Listeyi Sırala (Kat, Kolon Adı ve LoadCase'e göre)
            return results
                .OrderByDescending(r => r.Story)
                .ThenBy(r => r.Column)
                .ThenBy(r => r.LoadCase)
                .ToList();
        }
    }

    // --- KOLON EKSENEL YÜK KONTROLÜ UI ---
    public class KolonEksenelYukUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Action _fetchStoryData;
        private Func<List<StoryData>> _getStoryDataList;
        private Func<Panel, int, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        // UI Controls
        private TextBox txtFck;
        private TextBox txtLimit;
        private DataGridView dgvKolonResults;
        private Label lblKolonStatus;
        private Label lblKolonFrameStatus;
        private CheckBox chkKolonBodrum;
        private NumericUpDown numKolonBodrumKat;
        private ListBox lstKolonCombinations;
        private FlowLayoutPanel pnlKolonSelectedCombos;
        private RichTextBox rtbFailedColumns;

        // Data Storage
        private List<ColumnForceData> _kolonColumnForces = new List<ColumnForceData>();
        private List<FrameAssignmentData> _kolonFrameAssignments = new List<FrameAssignmentData>();
        private List<string> _kolonSelectedCombos = new List<string>();

        public KolonEksenelYukUI(Form1 form, Func<cSapModel> getSapModel, Action fetchStoryData, Func<List<StoryData>> getStoryDataList,
                                  Func<Panel, int, Panel> createNavigationPanel, Action<int> goToPage, Color colorBackground)
        {
            _form = form;
            _getSapModel = getSapModel;
            _fetchStoryData = fetchStoryData;
            _getStoryDataList = getStoryDataList;
            _createNavigationPanel = createNavigationPanel;
            _goToPage = goToPage;
            _colorBackground = colorBackground;
        }

        public void Initialize(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = Form1.CreateHeaderLabel("Kolon Eksenel Yük Tahkiki");
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));  // Sol panel
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));  // Sağ panel
            tlp.Padding = new Padding(15, 5, 15, 5);

            // =============== SOL PANEL ===============
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 320F));  // Parametreler
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));   // Frame/Element
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));   // Butonlar
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));   // Boş alan

            // ========== HESAP PARAMETRELERİ ==========
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 10, 5)
            };

            pnlParams.Controls.Add(new Label { Text = "fck (N/mm²):", Location = new Point(15, 38), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtFck = new TextBox { Location = new Point(100, 35), Width = 50, Text = "30" };
            pnlParams.Controls.Add(txtFck);

            pnlParams.Controls.Add(new Label { Text = "Limit:", Location = new Point(160, 38), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtLimit = new TextBox { Location = new Point(200, 35), Width = 50, Text = "0.40" };
            pnlParams.Controls.Add(txtLimit);

            chkKolonBodrum = new CheckBox { Text = "Bodrum kabulü var mı?", Location = new Point(15, 75), AutoSize = true, Font = new Font("Segoe UI", 8) };
            chkKolonBodrum.CheckedChanged += (s, ev) => { numKolonBodrumKat.Enabled = chkKolonBodrum.Checked; };
            pnlParams.Controls.Add(chkKolonBodrum);

            pnlParams.Controls.Add(new Label { Text = "Kat:", Location = new Point(165, 76), AutoSize = true, Font = new Font("Segoe UI", 8) });
            numKolonBodrumKat = new NumericUpDown { Location = new Point(190, 73), Width = 45, Minimum = 0, Maximum = 20, Value = 0, Enabled = false };
            pnlParams.Controls.Add(numKolonBodrumKat);

            // Kombinasyonlar bölümü
            TableLayoutPanel tlpCombosInParams = new TableLayoutPanel
            {
                Location = new Point(10, 100),
                Size = new Size(400, 210),
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            tlpCombosInParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCombosInParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Sol: Combinations list
            Panel pnlCombosLeft = new Panel { Dock = DockStyle.Fill };
            pnlCombosLeft.Controls.Add(new Label { Text = "Combinations and Cases", Location = new Point(5, 5), AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) });
            
            lstKolonCombinations = new ListBox
            {
                Location = new Point(5, 28),
                Size = new Size(180, 140),
                SelectionMode = SelectionMode.MultiExtended,
                Font = new Font("Segoe UI", 8)
            };
            lstKolonCombinations.DoubleClick += LstKolonCombinations_DoubleClick;
            pnlCombosLeft.Controls.Add(lstKolonCombinations);

            Button btnKolonLoadCombos = new Button { Text = "Getir", Size = new Size(55, 28), Location = new Point(5, 175), BackColor = Color.FromArgb(220, 220, 220), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnKolonLoadCombos.FlatAppearance.BorderSize = 1;
            btnKolonLoadCombos.Click += BtnKolonLoadCombos_Click;
            pnlCombosLeft.Controls.Add(btnKolonLoadCombos);

            Button btnKolonSelectCombos = new Button { Text = "Seç", Size = new Size(55, 28), Location = new Point(70, 175), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnKolonSelectCombos.FlatAppearance.BorderSize = 1;
            btnKolonSelectCombos.Click += BtnKolonSelectCombos_Click;
            pnlCombosLeft.Controls.Add(btnKolonSelectCombos);

            tlpCombosInParams.Controls.Add(pnlCombosLeft, 0, 0);

            // Sağ: Seçili Kombinasyonlar
            Panel pnlCombosRight = new Panel { Dock = DockStyle.Fill };
            pnlCombosRight.Controls.Add(new Label { Text = "Seçili Kombinasyonlar", Location = new Point(5, 5), AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) });
            
            pnlKolonSelectedCombos = new FlowLayoutPanel
            {
                Location = new Point(5, 28),
                Size = new Size(150, 175),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlCombosRight.Controls.Add(pnlKolonSelectedCombos);
            tlpCombosInParams.Controls.Add(pnlCombosRight, 1, 0);

            pnlParams.Controls.Add(tlpCombosInParams);
            tlpLeft.Controls.Add(pnlParams, 0, 0);

            // ========== TABLOLAR ==========
            TableLayoutPanel tlpDataTables = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 10, 5)
            };
            tlpDataTables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDataTables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            RoundedPanel pnlFrameData = new RoundedPanel { Title = "Frame Assignment", Dock = DockStyle.Fill, BorderRadius = 20, Margin = new Padding(0, 0, 5, 0) };
            Button btnGetFrame = new Button { Text = "Getir", Size = new Size(60, 28), Location = new Point(15, 35), BackColor = Color.FromArgb(220, 220, 220), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetFrame.FlatAppearance.BorderSize = 1;
            btnGetFrame.Click += BtnGetFrameAssignment_Click;
            pnlFrameData.Controls.Add(btnGetFrame);

            lblKolonFrameStatus = new Label { Text = "", Location = new Point(85, 40), Size = new Size(100, 20), Font = new Font("Segoe UI", 8), ForeColor = Color.Green };
            pnlFrameData.Controls.Add(lblKolonFrameStatus);
            tlpDataTables.Controls.Add(pnlFrameData, 0, 0);

            RoundedPanel pnlElemData = new RoundedPanel { Title = "Element Forces", Dock = DockStyle.Fill, BorderRadius = 20, Margin = new Padding(5, 0, 0, 0) };
            Button btnGetElem = new Button { Text = "Getir", Size = new Size(60, 28), Location = new Point(15, 35), BackColor = Color.FromArgb(220, 220, 220), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetElem.FlatAppearance.BorderSize = 1;
            btnGetElem.Click += BtnKolonGetValues_Click;
            pnlElemData.Controls.Add(btnGetElem);

            Label lblElemStatus = new Label { Name = "lblKolonElemStatus", Text = "", Location = new Point(85, 40), Size = new Size(100, 20), Font = new Font("Segoe UI", 8), ForeColor = Color.Green };
            pnlElemData.Controls.Add(lblElemStatus);
            tlpDataTables.Controls.Add(pnlElemData, 1, 0);

            tlpLeft.Controls.Add(tlpDataTables, 0, 1);

            // ========== BUTONLAR ==========
            Panel pnlCalcBtn = new Panel { Dock = DockStyle.Fill };
            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "Hesapla ve Kaydet",
                Size = new Size(200, 40),
                Location = new Point(5, 15),
                BaseColor = Color.FromArgb(159, 219, 255),
                BorderRadius = 20,
                GrowAmount = 2
            };
            btnCalculate.Click += BtnCalculateKolonEksenel_Click;
            pnlCalcBtn.Controls.Add(btnCalculate);
            tlpLeft.Controls.Add(pnlCalcBtn, 0, 2);

            // ========== SINIRI AŞANLALAR ==========
            RoundedPanel pnlFailed = new RoundedPanel
            {
                Title = "Sınırı Aşan Kolonlar",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 5, 10, 5)
            };
            
            rtbFailedColumns = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ReadOnly = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Padding = new Padding(5)
            };
            pnlFailed.Controls.Add(rtbFailedColumns);
            tlpLeft.Controls.Add(pnlFailed, 0, 3);
            
            tlp.Controls.Add(tlpLeft, 0, 0);

            // =============== SAĞ PANEL - SONUÇLAR ===============
            RoundedPanel pnlResults = new RoundedPanel
            {
                Title = "Sonuçlar",
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                Margin = new Padding(5, 0, 0, 0),
                TitleFont = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            dgvKolonResults = new DataGridView
            {
                Location = new Point(10, 40),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 8),
                ScrollBars = ScrollBars.Both,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(255, 203, 159),
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvKolonResults.Columns.Add("Story", "Story");
            dgvKolonResults.Columns.Add("Column", "Column");
            dgvKolonResults.Columns.Add("LoadCase", "Load Case/Combo");
            dgvKolonResults.Columns.Add("Location", "Location");
            dgvKolonResults.Columns.Add("P", "P (kN)");
            dgvKolonResults.Columns.Add("Section", "Analysis Section");
            dgvKolonResults.Columns.Add("B", "b (cm)");
            dgvKolonResults.Columns.Add("D", "d (cm)");
            dgvKolonResults.Columns.Add("Ac", "Ac (cm²)");
            dgvKolonResults.Columns.Add("AcFck", "Ac*fck (kN)");
            dgvKolonResults.Columns.Add("Ratio", "Ratio");
            dgvKolonResults.Columns.Add("Limit", "Limit");

            lblKolonStatus = new Label
            {
                Text = "",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            pnlResults.Controls.Add(dgvKolonResults);
            pnlResults.Controls.Add(lblKolonStatus);
            pnlResults.Resize += (s, e) => {
                dgvKolonResults.Size = new Size(pnlResults.Width - 25, pnlResults.Height - 80);
                lblKolonStatus.Location = new Point(10, pnlResults.Height - 35);
                lblKolonStatus.Size = new Size(pnlResults.Width - 25, 25);
            };

            tlp.Controls.Add(pnlResults, 1, 0);
            mainLayout.Controls.Add(tlp, 0, 1);

            // Navigasyon
            page.Tag = 5; 
            if (_createNavigationPanel != null)
            {
                page.VisibleChanged += (s, e) => {
                    if (page.Visible && mainLayout.Controls.Count < 3)
                    {
                         Panel navPanel = _createNavigationPanel(null, 5);
                         mainLayout.Controls.Add(navPanel, 0, 2);
                    }
                };
            }

            page.Controls.Add(mainLayout);
        }

        // --- EVENT HANDLERS ---

        private void BtnKolonLoadCombos_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lstKolonCombinations.Items.Clear();

                int numCombos = 0;
                string[] comboNames = null;
                sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                if (comboNames != null)
                    foreach (var name in comboNames)
                        lstKolonCombinations.Items.Add(name);

                int numCases = 0;
                string[] caseNames = null;
                sapModel.LoadCases.GetNameList(ref numCases, ref caseNames);
                if (caseNames != null)
                    foreach (var name in caseNames)
                        lstKolonCombinations.Items.Add(name);

                if (lstKolonCombinations.Items.Count == 0)
                    MessageBox.Show("Kombinasyon veya case bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kombinasyonlar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LstKolonCombinations_DoubleClick(object sender, EventArgs e)
        {
            foreach (var item in lstKolonCombinations.SelectedItems)
            {
                string comboName = item.ToString();
                if (!_kolonSelectedCombos.Contains(comboName))
                {
                    _kolonSelectedCombos.Add(comboName);
                    AddKolonSelectedComboTag(comboName);
                }
            }
        }

        private void BtnKolonSelectCombos_Click(object sender, EventArgs e)
        {
             foreach (var item in lstKolonCombinations.SelectedItems)
            {
                string comboName = item.ToString();
                if (!_kolonSelectedCombos.Contains(comboName))
                {
                    _kolonSelectedCombos.Add(comboName);
                    AddKolonSelectedComboTag(comboName);
                }
            }
        }

        private void AddKolonSelectedComboTag(string comboName)
        {
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
            btnRemove.Click += (s, ev) => { 
                pnlKolonSelectedCombos.Controls.Remove(tag); 
                _kolonSelectedCombos.Remove(comboName);
            };

            tag.Controls.Add(lbl);
            tag.Controls.Add(btnRemove);
            pnlKolonSelectedCombos.Controls.Add(tag);
        }

        private void BtnGetFrameAssignment_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string tableName = "Frame Assignments - Summary";
                string groupName = "";
                string[] fieldKeyList = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;
                int numRecords = 0;
                string[] tableData = null;

                int ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);

                if (ret != 0 || fieldsKeysIncluded == null)
                {
                    MessageBox.Show("Frame Assignment tablosu alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (lblKolonFrameStatus != null)
                    {
                        lblKolonFrameStatus.Text = "Veri Yok";
                        lblKolonFrameStatus.ForeColor = Color.Red;
                    }
                    return;
                }

                int numFields = fieldsKeysIncluded.Length;
                int uniqueIdx = -1, labelIdx = -1, storyIdx = -1, designTypeIdx = -1;
                
                // Bölüm indeksleri
                int analysisSectionIdx = -1, designSectionIdx = -1, sectionPropIdx = -1;

                for (int i = 0; i < numFields; i++)
                {
                    string col = fieldsKeysIncluded[i];
                    if (col == "Unique Name") uniqueIdx = i;
                    else if (col == "Label") labelIdx = i;
                    else if (col == "Story") storyIdx = i;
                    else if (col == "Design Type") designTypeIdx = i;
                    
                    else if (col.IndexOf("Analysis", StringComparison.OrdinalIgnoreCase) >= 0 && col.IndexOf("Sect", StringComparison.OrdinalIgnoreCase) >= 0) 
                        analysisSectionIdx = i;
                    else if (col.IndexOf("Design", StringComparison.OrdinalIgnoreCase) >= 0 && col.IndexOf("Sect", StringComparison.OrdinalIgnoreCase) >= 0) 
                        designSectionIdx = i;
                    else if (col.IndexOf("Section", StringComparison.OrdinalIgnoreCase) >= 0) 
                        sectionPropIdx = i;
                }

                _kolonFrameAssignments.Clear();
                int numRows = numRecords;

                for (int row = 0; row < numRows; row++)
                {
                    int baseIndex = row * numFields;

                    string designType = designTypeIdx >= 0 ? tableData[baseIndex + designTypeIdx] : "";
                    
                    if (designTypeIdx >= 0 && designType.IndexOf("Column", StringComparison.OrdinalIgnoreCase) < 0) continue;

                    string story = storyIdx >= 0 ? tableData[baseIndex + storyIdx] : "";
                    string label = labelIdx >= 0 ? tableData[baseIndex + labelIdx] : "";
                    string unique = uniqueIdx >= 0 ? tableData[baseIndex + uniqueIdx] : "";
                    
                    if (string.IsNullOrEmpty(unique)) unique = label;
                    
                    string finalSection = "";
                    if (analysisSectionIdx >= 0) finalSection = tableData[baseIndex + analysisSectionIdx];
                    if (string.IsNullOrEmpty(finalSection) && designSectionIdx >= 0) finalSection = tableData[baseIndex + designSectionIdx];
                    if (string.IsNullOrEmpty(finalSection) && sectionPropIdx >= 0) finalSection = tableData[baseIndex + sectionPropIdx];

                    double width = 0, height = 0;
                    if (!string.IsNullOrEmpty(finalSection))
                    {
                        var match = Regex.Match(finalSection, @"(\d+(?:[.,]\d+)?)\s*[xX*]\s*(\d+(?:[.,]\d+)?)");
                        if (match.Success)
                        {
                            width = double.Parse(match.Groups[1].Value) * 10;
                            height = double.Parse(match.Groups[2].Value) * 10;
                        }
                    }
                    double area = width * height;

                    _kolonFrameAssignments.Add(new FrameAssignmentData
                    {
                        UniqueName = unique,
                        Label = label,
                        Story = story,
                        SectionName = finalSection,
                        Width = width,
                        Height = height,
                        Area = area
                    });
                }

                if (_kolonFrameAssignments.Count > 0)
                {
                    if (lblKolonFrameStatus != null)
                    {
                        lblKolonFrameStatus.Text = "Tablo alındı.";
                        lblKolonFrameStatus.ForeColor = Color.Green;
                    }
                }
                else
                {
                    if (lblKolonFrameStatus != null)
                    {
                        lblKolonFrameStatus.Text = "Kolon verisi yok";
                        lblKolonFrameStatus.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                if (lblKolonFrameStatus != null)
                {
                    lblKolonFrameStatus.Text = "Hata";
                    lblKolonFrameStatus.ForeColor = Color.Red;
                }
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKolonGetValues_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!sapModel.GetModelIsLocked())
            {
                MessageBox.Show("Model kilitli değil. Lütfen analiz yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_kolonSelectedCombos.Count == 0)
            {
                MessageBox.Show("Önce kombinasyon seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Durum Label'ını güncelle
            var button = sender as Button;
            Label lbl = null;
            if (button?.Parent != null)
            {
                foreach (Control ctrl in button.Parent.Controls)
                {
                    if (ctrl is Label l && l.Name == "lblKolonElemStatus")
                    {
                        lbl = l; 
                        break;
                    }
                }
            }

            // Verileri çek 
            FetchKolonColumnForcesForSelectedCombos(lbl);
        }

        private void FetchKolonColumnForcesForSelectedCombos(Label lblStatus = null)
        {
            var sapModel = _getSapModel();
            try
            {
                // Önce temizle
                sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                // Seçili kombinasyonları ayarla
                foreach (string combo in _kolonSelectedCombos)
                {
                    sapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    sapModel.Results.Setup.SetComboSelectedForOutput(combo);
                }

                string tableName = "Element Forces - Columns";
                string groupName = "";
                string[] fieldKeyList = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;
                int numRecords = 0;
                string[] tableData = null;

                int ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);

                // Alternatif tablo arama
                if (ret != 0 || numRecords == 0)
                {
                     int numTables = 0;
                    string[] availableTables = null; string[] desc = null; int[] import = null;
                    sapModel.DatabaseTables.GetAvailableTables(ref numTables, ref availableTables, ref desc, ref import);
                    if (availableTables != null)
                    {
                        foreach (string t in availableTables)
                        {
                            if (t.ToUpper().Contains("ELEMENT") && t.ToUpper().Contains("FORCES") && t.ToUpper().Contains("COLUMN"))
                            {
                                tableName = t;
                                break;
                            }
                        }
                    }
                    ret = sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
                }

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    MessageBox.Show($"Element Forces tablosu okunamadı veya boş!\nTablo: {tableName}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int numFields = fieldsKeysIncluded.Length;
                int storyIdx = -1, columnIdx = -1, caseIdx = -1, pIdx = -1, uniqueIdx = -1, stepTypeIdx = -1, stationIdx = -1;
                for (int i = 0; i < numFields; i++)
                {
                    string originalCol = fieldsKeysIncluded[i].Trim();
                    string col = originalCol.ToUpper().Replace(" ", "").Replace("_", "");
                    
                    if (col == "STORY") storyIdx = i;
                    else if (col == "COLUMN" || col == "LABEL") columnIdx = i;
                    else if (col == "UNIQUENAME" || col == "UNIQUE") uniqueIdx = i;
                    else if (col == "OUTPUTCASE" || col == "LOADCASE" || col == "CASE") caseIdx = i;
                    else if (col == "STEPTYPE") stepTypeIdx = i;
                    else if (col == "PKN" || col == "P") pIdx = i;
                    
                    if (stationIdx < 0)
                    {
                        if (col == "STATION" || col == "STATIONM" || col == "LOCATION" || 
                            col.Contains("STATION") || 
                            originalCol.Equals("Station", StringComparison.OrdinalIgnoreCase))
                        {
                            stationIdx = i;
                        }
                    }
                }

                _kolonColumnForces.Clear();
                int numRows = numRecords;

                for (int row = 0; row < numRows; row++)
                {
                    int baseIndex = row * numFields;
                    string loadCase = caseIdx >= 0 ? tableData[baseIndex + caseIdx] : "";

                    // Kombinasyon Filtresi (zaten ETABS'a set ettik ama garanti olsun)
                    bool matchedCombo = _kolonSelectedCombos.Any(c => string.Equals(loadCase, c, StringComparison.OrdinalIgnoreCase));
                    if (!matchedCombo) continue;

                    // Station = 0 Filtresi
                    string stationStr = stationIdx >= 0 ? tableData[baseIndex + stationIdx] : "";
                    stationStr = stationStr.Replace(",", ".");
                    
                    double stationValue = -999;
                    if (double.TryParse(stationStr, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out stationValue))
                    {
                        if (Math.Abs(stationValue) > 0.0001) continue;
                    }

                    // StepType = Min Filtresi
                    string stepType = stepTypeIdx >= 0 ? tableData[baseIndex + stepTypeIdx] : "";
                    if (!string.IsNullOrEmpty(stepType) && !string.Equals(stepType, "Min", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string unique = uniqueIdx >= 0 ? tableData[baseIndex + uniqueIdx] : "";
                    string column = columnIdx >= 0 ? tableData[baseIndex + columnIdx] : "";
                    string story = storyIdx >= 0 ? tableData[baseIndex + storyIdx] : "";
                    string location = (stationIdx >= 0) ? tableData[baseIndex + stationIdx] : "0";

                    double p = 0;
                    if (pIdx >= 0)
                        double.TryParse(tableData[baseIndex + pIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out p);

                    _kolonColumnForces.Add(new ColumnForceData
                    {
                        UniqueName = unique,
                        Column = column,
                        Story = story,
                        LoadCase = loadCase,
                        Location = location,
                        P = p
                    });
                }

                if (lblStatus != null)
                {
                    if (_kolonColumnForces.Count > 0)
                    {
                        lblStatus.Text = $"{_kolonColumnForces.Count} satır veri hazır.";
                        lblStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblStatus.Text = "Veri Yok / Filtre dışı";
                        lblStatus.ForeColor = Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Element Forces işleme hatası:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (lblStatus != null)
                {
                    lblStatus.Text = "Hata";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            finally
            {
                if (sapModel != null)
                     sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }
        }

        private void BtnCalculateKolonEksenel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtFck.Text, out double fck) || fck <= 0)
                {
                    MessageBox.Show("fck değeri geçerli ve sıfırdan büyük olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtLimit.Text, out double limit) || limit <= 0)
                {
                    MessageBox.Show("Limit değeri geçerli olmalıdır (Örn: 0.40)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool hasAH = _kolonSelectedCombos.Any(c => c.ToUpper().Contains("A"));
                if (chkKolonBodrum.Checked && !hasAH)
                {
                    MessageBox.Show("Bodrum kabulü varsa lütfen alt yüklemeli (A içeren) kombinasyonları seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (_kolonColumnForces.Count == 0 || _kolonFrameAssignments.Count == 0)
                {
                    MessageBox.Show("Önce Frame Assignment ve Element Forces verilerini 'Getir' butonları ile çekiniz.", "Veri Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var stories = _getStoryDataList();
                if (stories == null || stories.Count == 0) 
                {
                    _fetchStoryData();
                    stories = _getStoryDataList();
                }

                bool isBodrum = chkKolonBodrum.Checked;
                int bodrumCount = (int)numKolonBodrumKat.Value;

                var manager = new KolonEksenelYukManager(fck, limit, isBodrum, bodrumCount);
                var results = manager.Calculate(_kolonColumnForces, _kolonFrameAssignments, stories);

                // Tabloyu Doldur
                dgvKolonResults.Rows.Clear();
                dgvKolonResults.Columns.Clear();
                
                dgvKolonResults.Columns.Add("Story", "Story");
                dgvKolonResults.Columns.Add("Column", "Column");
                dgvKolonResults.Columns.Add("LoadCase", "Load Case/Combo");
                dgvKolonResults.Columns.Add("Location", "Location");
                dgvKolonResults.Columns.Add("P", "P (kN)");
                dgvKolonResults.Columns.Add("Section", "Analysis Section");
                dgvKolonResults.Columns.Add("B", "b (cm)");
                dgvKolonResults.Columns.Add("D", "d (cm)");
                dgvKolonResults.Columns.Add("Ac", "Ac (cm²)");
                dgvKolonResults.Columns.Add("AcFck", "Ac*fck (kN)");
                dgvKolonResults.Columns.Add("Ratio", "Ratio");
                dgvKolonResults.Columns.Add("Limit", $"Limit ({limit})");

                foreach (var res in results)
                {
                    int rowIndex = dgvKolonResults.Rows.Add(
                        res.Story,
                        res.Column,
                        res.LoadCase,
                        res.Location,
                        res.Nd.ToString("0.00"),
                        res.Section,
                        res.B.ToString("0"),
                        res.D.ToString("0"),
                        res.Ac.ToString("0.00"),
                        res.AcFck.ToString("0.00"),
                        res.NdRatio.ToString("0.000"),
                        res.Limit.ToString("0.00")
                    );

                    if (!res.IsOK)
                    {
                        dgvKolonResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200); 
                        dgvKolonResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                }

                int failCount = results.Count(r => !r.IsOK);
                if (failCount > 0)
                {
                    lblKolonStatus.Text = $"❌ {failCount} kolon limiti aşıyor!";
                    lblKolonStatus.ForeColor = Color.Red;
                }
                else
                {
                    lblKolonStatus.Text = "✅ Tüm kolonlar limiti sağlıyor.";
                    lblKolonStatus.ForeColor = Color.Green;
                }

                if (failCount > 0)
                {
                    var failedLabels = results
                        .Where(r => !r.IsOK)
                        .Select(r => $"{r.Column} ({r.Story})")
                        .Distinct()
                        .OrderBy(c => c)
                        .ToList();
                    
                    rtbFailedColumns.Text = string.Join(", ", failedLabels);
                    rtbFailedColumns.ForeColor = Color.Red;
                }
                else
                {
                    rtbFailedColumns.Text = "Sınırı aşan kolon yok.";
                    rtbFailedColumns.ForeColor = Color.Green;
                }

                SaveKolonEksenelResults(results, fck);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesaplama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveKolonEksenelResults(List<KolonEksenelYukResult> results, double fck)
        {
            try
            {
                string fileName = $"KolonEksenelYuk_Sonuc_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string filePath = System.IO.Path.Combine(Application.StartupPath, fileName);

                using (var sw = new System.IO.StreamWriter(filePath, false, new System.Text.UTF8Encoding(true)))
                {
                    sw.WriteLine($"Kolon Eksenel Yük Kontrolü Sonuçları - fck = {fck} N/mm²");
                    sw.WriteLine($"Tarih: {DateTime.Now:dd.MM.yyyy HH:mm}");
                    sw.WriteLine();
                    sw.WriteLine("Story;Column;Load Case/Combo;Location;P (kN);Analysis Section;b (cm);d (cm);Ac (cm²);Ac*fck (kN);Limit");

                    foreach (var result in results)
                    {
                        sw.WriteLine($"{result.Story};{result.Column};{result.LoadCase};{result.Location};{result.Nd:0.00};{result.Section};{result.B:0.00};{result.D:0.00};{result.Ac:0.00};{result.AcFck:0.00};{result.Limit:0.00}");
                    }
                }

                MessageBox.Show($"Sonuçlar kaydedildi:\n{filePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}