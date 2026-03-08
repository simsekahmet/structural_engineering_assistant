using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSiAPIv1;

namespace EtabsTools
{
    // Veri Modelleri
    public class ColumnRebarData
    {
        public string Name { get; set; }
        public string SectionName { get; set; } // ETABS kesit adı
        public bool RebarChanged { get; set; } // Değişiklik yapıldı mı?
        public string Story { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string RebarLabel { get; set; } // Örn: "20fi25"
        public Color DisplayColor { get; set; } // Çizim rengi
        public double Width { get; set; } // m cinsinden kesit genişliği
        public double Depth { get; set; } // m cinsinden kesit yüksekliği/çapı
        public double Angle { get; set; } // derece cinsinden dönme açısı
        public int Shape { get; set; } // 1: Rectangular, 2: Circular
        public string TypeLabel { get; set; } // T1, T2 gibi tip ismi
    }

    public class BeamData
    {
        public string Name { get; set; }
        public string Story { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }

    // Perde datası
    public class WallData
    {
        public string Name { get; set; }
        public string Story { get; set; }
        public List<PointF> Points { get; set; } = new List<PointF>();
        public double Thickness { get; set; }
    }

    public class KolonDonesiUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<Panel, int, string, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        // UI Elemanları
        private ComboBox cmbStory;
        private SmoothButton btnFetch;
        private RadioButton rbRebarView;
        private RadioButton rbObjectView;
        private CheckBox chkShowBeams;
        private CheckBox chkShowWalls;
        private SmoothButton btnDWG;
        private SmoothButton btnReset; // Eklendi
        private ComboBox cmbScope; // Tüm Katlar / Bir Kat
        private PlanViewPanel pnlPlanView;
        private Label lblLoading;

        // Veri
        private List<ColumnRebarData> _allColumns = new List<ColumnRebarData>();
        private List<ColumnRebarData> _originalColumns = new List<ColumnRebarData>(); // Eklendi
        private List<BeamData> _allBeams = new List<BeamData>();
        private List<WallData> _allWalls = new List<WallData>();
        private Dictionary<string, Color> _rebarColors = new Dictionary<string, Color>();
        private Random _rnd = new Random();

        public KolonDonesiUI(Form1 form, Func<cSapModel> getSapModel, 
                             Func<Panel, int, string, Panel> createNavigationPanel, 
                             Action<int> goToPage, Color colorBackground)
        {
            _form = form;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNavigationPanel;
            _goToPage = goToPage;
            _colorBackground = colorBackground;
        }

        public void Initialize(TabPage page)
        {
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            Label lblPageTitle = Form1.CreateHeaderLabel("Kolon Donesi");
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            TableLayoutPanel tlpContent = new TableLayoutPanel();
            tlpContent.Dock = DockStyle.Fill;
            tlpContent.ColumnCount = 2;
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));  
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));  
            tlpContent.Padding = new Padding(15, 5, 15, 5);

            // ================= SOL PANEL (Scrollable Wrapper) =================
            Panel pnlLeftScroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            RoundedPanel pnlLeft = new RoundedPanel
            {
                Title = "Ayarlar ve Veri Çekme",
                Dock = DockStyle.Top,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 10, 5),
                TitleFont = new Font("Segoe UI", 12, FontStyle.Bold),
                Height = 850 // İçerik yüksekliği
            };

            btnFetch = new SmoothButton
            {
                Text = "Model Bilgilerini Çek",
                Size = new Size(145, 40),
                Location = new Point(10, 35),
                BaseColor = Color.FromArgb(218, 232, 252), // Soft Blue
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnFetch.Click += BtnFetch_Click;

            btnReset = new SmoothButton
            {
                Text = "Orijinale Dön",
                Size = new Size(110, 40),
                Location = new Point(160, 35),
                BaseColor = Color.FromArgb(255, 235, 235), // Soft Red/Pink for reset
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Regular)
            };
            btnReset.Click += BtnReset_Click;

            // === Kat Seçimi ===
            Label lblStory = new Label { Text = "Kat Seçimi:", Location = new Point(15, 85), AutoSize = true, Font = new Font("Segoe UI", 9) };
            cmbStory = new ComboBox { Location = new Point(15, 105), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStory.SelectedIndexChanged += CmbStory_SelectedIndexChanged;

            // Filtreler
            chkShowBeams = new CheckBox { Text = "Kirişleri Göster", Location = new Point(15, 140), AutoSize = true, Checked = true };
            chkShowWalls = new CheckBox { Text = "Perdeleri Göster", Location = new Point(15, 160), AutoSize = true, Checked = true };

            chkShowBeams.CheckedChanged += (s, e) => { pnlPlanView.SetShowBeams(chkShowBeams.Checked); };
            chkShowWalls.CheckedChanged += (s, e) => { pnlPlanView.SetShowWalls(chkShowWalls.Checked); };

            Label lblView = new Label { Text = "Görünüm Seçeneği:", Location = new Point(15, 195), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            rbRebarView = new RadioButton { Text = "Çaplara Göre Görünüm", Location = new Point(15, 215), AutoSize = true, Checked = true };
            rbObjectView = new RadioButton { Text = "Tiplere Göre Görünüm", Location = new Point(15, 235), AutoSize = true };

            rbRebarView.CheckedChanged += (s, e) => { if (rbRebarView.Checked) UpdatePlanView(); };
            rbObjectView.CheckedChanged += (s, e) => { if (rbObjectView.Checked) UpdatePlanView(); };

            btnDWG = new SmoothButton
            {
                Text = "DWG Olarak Kaydet",
                Size = new Size(180, 40),
                Location = new Point(15, 270),
                BaseColor = Color.FromArgb(213, 232, 212), // Soft Green
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnDWG.Click += BtnDWG_Click;

            // Scope seçimi
            Label lblScope = new Label { Text = "Değişiklik Kapsamı:", Location = new Point(15, 320), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            cmbScope = new ComboBox { Location = new Point(15, 340), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbScope.Items.Add("Bir Kat");
            cmbScope.Items.Add("Tüm Katlar");
            cmbScope.SelectedIndex = 0; // Default: Bir Kat

            pnlLeft.Controls.Add(btnFetch);
            pnlLeft.Controls.Add(btnReset);
            pnlLeft.Controls.Add(lblStory);
            pnlLeft.Controls.Add(cmbStory);
            pnlLeft.Controls.Add(chkShowBeams);
            pnlLeft.Controls.Add(chkShowWalls);
            pnlLeft.Controls.Add(lblView);
            pnlLeft.Controls.Add(rbRebarView);
            pnlLeft.Controls.Add(rbObjectView);
            pnlLeft.Controls.Add(btnDWG);
            pnlLeft.Controls.Add(lblScope);
            pnlLeft.Controls.Add(cmbScope);

            tlpContent.Controls.Add(pnlLeft, 0, 0);

            // ================= SAĞ PANEL (PLAN VIEW) =================
            RoundedPanel pnlRight = new RoundedPanel
            {
                Title = "Kat Planı",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(5, 0, 0, 5),
                TitleFont = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            SmoothButton btnUp = new SmoothButton 
            { 
                Text = "▲", 
                Size = new Size(35, 30), 
                Location = new Point(pnlRight.Width - 90, 8), 
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BaseColor = Color.FromArgb(244, 247, 254),
                BorderRadius = 8,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnUp.Click += (s, e) => ChangeStory(1);
            
            SmoothButton btnDown = new SmoothButton 
            { 
                Text = "▼", 
                Size = new Size(35, 30), 
                Location = new Point(pnlRight.Width - 50, 8), 
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BaseColor = Color.FromArgb(244, 247, 254),
                BorderRadius = 8,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnDown.Click += (s, e) => ChangeStory(-1);

            pnlRight.Controls.Add(btnUp);
            pnlRight.Controls.Add(btnDown);

            pnlPlanView = new PlanViewPanel
            {
                Location = new Point(15, 45),
                Size = new Size(pnlRight.Width - 30, pnlRight.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblLoading = new Label
            {
                Text = "Veriler Çekiliyor...",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                BackColor = Color.White,
                AutoSize = true,
                Visible = false
            };
            
            pnlPlanView.Controls.Add(lblLoading);
            pnlPlanView.Resize += (s, e) => 
            {
                if(lblLoading.Visible)
                    lblLoading.Location = new Point((pnlPlanView.Width - lblLoading.Width) / 2, (pnlPlanView.Height - lblLoading.Height) / 2);
            };

            pnlPlanView.OnColumnClick += OnColumnClicked;
            pnlPlanView.OnColumnsSelected += OnMultipleColumnsSelected;
            pnlPlanView.OnRebarChangeRequested += (cols, newRebar) => ApplyRebarChange(cols, newRebar);
            
            // Paneli sağdaki kapsayıcıya ekle (Eksik olduğu için ekranda görünmüyordu)
            pnlRight.Controls.Add(pnlPlanView);

            pnlLeftScroll.Controls.Add(pnlLeft);
            tlpContent.Controls.Add(pnlLeftScroll, 0, 0);
            tlpContent.Controls.Add(pnlRight, 1, 0);

            mainLayout.Controls.Add(tlpContent, 0, 1);

            int tabIndex = (int)page.Tag;
            page.VisibleChanged += (s, e) => {
                if (page.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = _createNavigationPanel(null, tabIndex, "DONE");
                    mainLayout.Controls.Add(navPanel, 0, 2);
                }
            };

            page.Controls.Add(mainLayout);
        }

        private void ChangeStory(int direction)
        {
            if (cmbStory.Items.Count == 0 || cmbStory.SelectedIndex == -1) return;
            
            // Listede katlar aşağıdan yukarıya dizilmiş (0 = En alt kat).
            // Üst Kat (direction=1) => index artmalı (+1)
            // Alt Kat (direction=-1) => index azalmalı (-1)
            int newIdx = cmbStory.SelectedIndex + direction;
            
            // Sonsuz döngü: sınırları aşınca karşı uca atla
            if (newIdx < 0)
                newIdx = cmbStory.Items.Count - 1; // En alttayken aşağı bas => en üste git
            else if (newIdx >= cmbStory.Items.Count)
                newIdx = 0; // En üstteyken yukarı bas => en alta git
            
            cmbStory.SelectedIndex = newIdx;
        }

        private void CmbStory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlanView();
        }

        private async void BtnFetch_Click(object sender, EventArgs e)
        {
            var model = _getSapModel();
            if (model == null)
            {
                ToastForm.ShowToast("ETABS bağlanamadı!", _form, 2000);
                return;
            }

            try
            {
                btnFetch.Enabled = false;
                chkShowBeams.Enabled = false;
                chkShowWalls.Enabled = false;
                btnDWG.Enabled = false;
                
                lblLoading.Visible = true;
                lblLoading.Location = new Point((pnlPlanView.Width - lblLoading.Width) / 2, (pnlPlanView.Height - lblLoading.Height) / 2);
                pnlPlanView.ClearData(); // Ekrani temizle
                
                _allColumns.Clear();
                _allBeams.Clear();
                _allWalls.Clear();
                _rebarColors.Clear();
                cmbStory.Items.Clear();

                // ETABS'ten kat bilgilerini çek (modeldeki sırayı korumak için)
                int numStories = 0;
                string[] storyNames = null;
                
                model.Story.GetNameList(ref numStories, ref storyNames);

                await Task.Run(() => FetchDataFromEtabs(model));

                var modelStories = new List<string>();
                if (storyNames != null)
                {
                    modelStories.AddRange(storyNames);
                    // Kullanıcı talebi: Listenin en altında en alt kat olmalı.
                    // Liste yukarıya doğru büyümelidir (ST1, ST2, ST3... -> aşağıdan yukarıya).
                    // Bu nedenle STMax en başta (index 0) olmalıdır.
                    modelStories.Reverse();
                }

                var usedStories = _allColumns.Select(c => c.Story)
                            .Concat(_allBeams.Select(b => b.Story))
                            .Concat(_allWalls.Select(w => w.Story))
                            .Distinct()
                            .ToList();
                
                var orderedStories = new List<string>();
                // Önce ETABS modelindeki orijinal sıraya göre (yukarıdan aşağıya) ekle
                foreach (var ms in modelStories)
                {
                    if (usedStories.Contains(ms))
                    {
                        orderedStories.Add(ms);
                    }
                }
                
                // Modelde adı geçmeyen ("Bilinmiyor" vs.) varsa lisenin sonuna ekle
                foreach (var us in usedStories)
                {
                    if (!orderedStories.Contains(us))
                    {
                        orderedStories.Add(us);
                    }
                }

                foreach (var s in orderedStories) cmbStory.Items.Add(s);

                if (cmbStory.Items.Count > 0) cmbStory.SelectedIndex = 0;

                ToastForm.ShowToast("Model koordinatları başarıyla çekildi.", _form, 2000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                btnFetch.Enabled = true;
                chkShowBeams.Enabled = true;
                chkShowWalls.Enabled = true;
                btnDWG.Enabled = true;
                btnReset.Enabled = true;

                lblLoading.Visible = false;
                
                // Orijinal verileri yedekle
                _originalColumns.Clear();
                foreach (var col in _allColumns)
                {
                    _originalColumns.Add(new ColumnRebarData
                    {
                        Name = col.Name,
                        SectionName = col.SectionName,
                        RebarChanged = col.RebarChanged,
                        Story = col.Story,
                        X = col.X, Y = col.Y,
                        RebarLabel = col.RebarLabel,
                        TypeLabel = col.TypeLabel,
                        DisplayColor = col.DisplayColor,
                        Width = col.Width, Depth = col.Depth,
                        Angle = col.Angle, Shape = col.Shape
                    });
                }

                // Tiplendirmeyi yap
                GenerateColumnTypes();

                UpdatePlanView();
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (_originalColumns.Count == 0) return;

            _allColumns.Clear();
            foreach (var col in _originalColumns)
            {
                _allColumns.Add(new ColumnRebarData
                {
                    Name = col.Name,
                    SectionName = col.SectionName,
                    RebarChanged = col.RebarChanged,
                    Story = col.Story,
                    X = col.X, Y = col.Y,
                    RebarLabel = col.RebarLabel,
                    TypeLabel = col.TypeLabel, // Orijinal TipLabel (Genelde boş/varsayılan)
                    DisplayColor = col.DisplayColor,
                    Width = col.Width, Depth = col.Depth,
                    Angle = col.Angle, Shape = col.Shape
                });
            }

            GenerateColumnTypes(); // Yeniden grupla (orijinal ayarlarla)
            UpdatePlanView(); // Değişikliği anında yansıt
            ToastForm.ShowToast("Orijinal değerlere dönüldü.", _form, 1500);
        }

        public static double CalculateRebarRatio(ColumnRebarData col)
        {
            if (string.IsNullOrEmpty(col.RebarLabel)) return 0;

            // Örnek format: 12φ16, 8φ20
            string lbl = col.RebarLabel.Replace(" ", "");
            string[] parts = lbl.Split('φ'); // "\u03C6" (phi)
            if (parts.Length != 2) return 0;

            if (int.TryParse(parts[0], out int count) && double.TryParse(parts[1], out double diameterMm))
            {
                // Çap mm cinsinden. Alan cm2 olarak hesaplayıp m2'ye çevireceğiz veya doğrudan m2
                double diameterM = diameterMm / 1000.0;
                double areaRebarTotalM2 = count * (Math.PI * Math.Pow(diameterM, 2) / 4.0);

                // Kolon boyutu m cinsinden
                double areaColM2 = col.Width * col.Depth;
                if (areaColM2 == 0) return 0;

                return (areaRebarTotalM2 / areaColM2) * 100.0;
            }
            return 0;
        }

        private void OnColumnClicked(ColumnRebarData col)
        {
            bool isRebarView = rbRebarView.Checked;
            
            if (isRebarView)
            {
                string newRebar = Prompt.ShowDialog($"Güncel Donatı: {col.RebarLabel}", "Donatı Çapı ve Adetini Değiştir (Örn: 16φ20)", col.RebarLabel);
                if (!string.IsNullOrEmpty(newRebar) && newRebar != col.RebarLabel)
                {
                    ApplyRebarChange(new List<ColumnRebarData> { col }, newRebar);
                }
            }
            else
            {
                string newType = Prompt.ShowDialog($"Güncel Tip: {col.TypeLabel}\n\nNot: Manuel tip değişimi yeni bir donatı anlamına gelmez. Eğer kolon farklıysa donatı görünümünden donatısını güncelleyin. Yine de değiştirmek isterseniz:", "Tip Adını Değiştir (Örn: T5)", col.TypeLabel);
                if (!string.IsNullOrEmpty(newType) && newType != col.TypeLabel)
                {
                    col.TypeLabel = newType;
                    UpdatePlanView();
                }
            }
        }

        private void OnMultipleColumnsSelected(List<ColumnRebarData> cols)
        {
            // Çoklu seçim yapıldı, sağ tık ile menü açılacak
        }

        private void ApplyRebarChange(List<ColumnRebarData> columns, string newRebar)
        {
            string[] parts = newRebar.Split('φ');
            string sizeOnly = parts.Length == 2 ? parts[1] : newRebar;
            if (!_rebarColors.ContainsKey(newRebar))
            {
                _rebarColors[newRebar] = GetRebarColor(sizeOnly);
            }
            
            bool allStories = cmbScope != null && cmbScope.SelectedIndex == 1; // Tüm Katlar
            
            foreach (var col in columns)
            {
                col.RebarLabel = newRebar;
                col.DisplayColor = _rebarColors[newRebar];
                col.RebarChanged = true;
                
                // Tüm Katlar seçiliyse aynı koordinattaki diğer katlardakileri de güncelle
                if (allStories)
                {
                    var samePositionCols = _allColumns.Where(c => 
                        Math.Abs(c.X - col.X) < 0.01 && 
                        Math.Abs(c.Y - col.Y) < 0.01 && 
                        c != col).ToList();
                    foreach (var sc in samePositionCols)
                    {
                        sc.RebarLabel = newRebar;
                        sc.DisplayColor = _rebarColors[newRebar];
                        sc.RebarChanged = true;
                    }
                }
            }
            
            GenerateColumnTypes();
            UpdatePlanView();
        }

        private void BtnDWG_Click(object sender, EventArgs e)
        {
            if (_allColumns.Count == 0)
            {
                MessageBox.Show("Lütfen önce model bilgilerini çekin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new System.Windows.Forms.SaveFileDialog())
            {
                sfd.Filter = "DWG Dosyası|*.dxf";
                sfd.Title = "Kolon Donesi DWG Kaydet";
                sfd.FileName = "KolonDonesi.dxf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Geçerli katın plan bilgisini ve TÜM kolonları dışa aktar (daralma için)
                        string currentStory = cmbStory.SelectedItem?.ToString() ?? "";
                        var storyBeams = _allBeams.Where(b => b.Story == currentStory).ToList();
                        var storyWalls = _allWalls.Where(w => w.Story == currentStory).ToList();
                        
                        // Kat sırasını al (combobox sırasıyla)
                        var storyOrder = new List<string>();
                        for (int i = 0; i < cmbStory.Items.Count; i++)
                            storyOrder.Add(cmbStory.Items[i].ToString());
                        
                        KolonDwgExporter.ExportToDxf(
                            sfd.FileName,
                            _allColumns,
                            storyBeams,
                            storyWalls,
                            _rebarColors,
                            currentStory,
                            storyOrder);
                        
                        MessageBox.Show($"DXF dosyası başarıyla kaydedildi:\n{sfd.FileName}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"DXF kaydedilirken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private class StackInfo
        {
            public List<ColumnRebarData> Group = new List<ColumnRebarData>();
            public Dictionary<string, string> StorySections = new Dictionary<string, string>();
            public string TypeLabel;
        }

        private void GenerateColumnTypes()
        {
            // Plan lokasyonlarına (X,Y) göre kolonları grupla.
            // Tolerans 0.05m (5 cm) olabilir
            double clusterTolerance = 0.05;
            
            var groups = new List<List<ColumnRebarData>>();
            foreach (var col in _allColumns)
            {
                bool added = false;
                foreach (var grp in groups)
                {
                    var rep = grp[0];
                    if (Math.Abs(rep.X - col.X) < clusterTolerance && Math.Abs(rep.Y - col.Y) < clusterTolerance)
                    {
                        grp.Add(col);
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    groups.Add(new List<ColumnRebarData> { col });
                }
            }

            // Tip atama mantığı: Alt küme / Kapsama mantığı (Subset Matching)
            // Eğer daha kısa bir kolon, daha uzun bir kolonun kendi katlarındaki özellikleriyle birebir örtüşüyorsa
            // (yani uzun kolonun bir "alt kümesi/prefix"i ise), uzun kolonun tip numarasını alır.
            var stackInfos = new List<StackInfo>();
            foreach (var grp in groups)
            {
                var info = new StackInfo { Group = grp };
                foreach (var col in grp)
                {
                    double minDim = Math.Min(col.Width, col.Depth);
                    double maxDim = Math.Max(col.Width, col.Depth);
                    string sig = $"{Math.Round(minDim*100)}x{Math.Round(maxDim*100)}_{col.Shape}_{col.RebarLabel}";
                    info.StorySections[col.Story] = sig;
                }
                stackInfos.Add(info);
            }

            // En çok kata (en fazla veriye) sahip olanlar önce değerlendirilmeli
            stackInfos = stackInfos.OrderByDescending(s => s.StorySections.Count).ToList();

            int typeCounter = 1;
            var definedTypes = new List<StackInfo>();

            foreach (var info in stackInfos)
            {
                StackInfo matchedType = null;
                foreach (var definedType in definedTypes)
                {
                    bool isMatch = true;
                    foreach (var kvp in info.StorySections)
                    {
                        // Katı yoksa veya o kattaki özellikleri farklıysa eşleşme iptal
                        if (!definedType.StorySections.ContainsKey(kvp.Key) || definedType.StorySections[kvp.Key] != kvp.Value)
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        matchedType = definedType;
                        break;
                    }
                }

                if (matchedType != null)
                {
                    info.TypeLabel = matchedType.TypeLabel;
                }
                else
                {
                    info.TypeLabel = $"T{typeCounter++}";
                    definedTypes.Add(info); // Yeni bir kök tip tanımlandı
                }

                foreach (var col in info.Group)
                {
                    col.TypeLabel = info.TypeLabel;
                }
            }
        }

        private void FetchDataFromEtabs(cSapModel model)
        {
            // Çubukları dolaş
            int numNames = 0;
            string[] myName = null;
            model.FrameObj.GetNameList(ref numNames, ref myName);

            if (numNames > 0)
            {
                foreach (string tName in myName)
                {
                    eFrameDesignOrientation propType = eFrameDesignOrientation.Null;
                    model.FrameObj.GetDesignOrientation(tName, ref propType);

                    if (propType == eFrameDesignOrientation.Column || propType == eFrameDesignOrientation.Beam)
                    {
                        string cLabel = "", cStory = "";
                        model.FrameObj.GetLabelFromName(tName, ref cLabel, ref cStory);
                        if (string.IsNullOrEmpty(cStory)) cStory = "Bilinmiyor";

                        string pt1 = "", pt2 = "";
                        model.FrameObj.GetPoints(tName, ref pt1, ref pt2);

                        double x1 = 0, y1 = 0, z1 = 0;
                        model.PointObj.GetCoordCartesian(pt1, ref x1, ref y1, ref z1);

                        double x2 = 0, y2 = 0, z2 = 0;
                        model.PointObj.GetCoordCartesian(pt2, ref x2, ref y2, ref z2);

                        if (propType == eFrameDesignOrientation.Column)
                        {
                            string propName = "", sAuto = "";
                            model.FrameObj.GetSection(tName, ref propName, ref sAuto);

                            double angle = 0;
                            bool advanced = false;
                            model.FrameObj.GetLocalAxes(tName, ref angle, ref advanced);

                            // Boyutları çek
                            eFramePropType sType = default(eFramePropType);
                            model.PropFrame.GetTypeOAPI(propName, ref sType);

                            double t3 = 0.4, t2 = 0.4;
                            int shape = 1;
                            string fileName = "", matProp = "";
                            int color = 0;
                            string notes = "";
                            string guid = "";

                            if ((int)sType == 1) // Rectangular
                            {
                                model.PropFrame.GetRectangle(propName, ref fileName, ref matProp, ref t3, ref t2, ref color, ref notes, ref guid);
                                shape = 1;
                            }
                            else if ((int)sType == 2) // Circular
                            {
                                model.PropFrame.GetCircle(propName, ref fileName, ref matProp, ref t3, ref color, ref notes, ref guid);
                                t2 = t3;
                                shape = 2;
                            }
                            else 
                            {
                                // SD (Section Designer) veya diğer kesitler için Atalet ve Alan üzerinden eşdeğer dikdörtgen hesabı:
                                double Area = 0, As2 = 0, As3 = 0, Torsion = 0, I22 = 0, I33 = 0, S22 = 0, S33 = 0, Z22 = 0, Z33 = 0, R22 = 0, R33 = 0;
                                int ret = model.PropFrame.GetSectProps(propName, ref Area, ref As2, ref As3, ref Torsion, ref I22, ref I33, ref S22, ref S33, ref Z22, ref Z33, ref R22, ref R33);
                                if (ret == 0 && Area > 0)
                                {
                                    // I33 = 1/12 * t3 * t2^3   =>  t2 = sqrt(12 * I33 / Area)
                                    // I22 = 1/12 * t2 * t3^3   =>  t3 = sqrt(12 * I22 / Area)
                                    t2 = Math.Sqrt(12 * I33 / Area);
                                    t3 = Math.Sqrt(12 * I22 / Area);
                                }
                                shape = 1; // Eşdeğer dikdörtgen olarak çiz
                            }

                            string rebarLabel = GetRebarLabel(model, propName);
                            string[] parts = rebarLabel.Split(new string[] { "\u03C6" }, StringSplitOptions.None);
                            string rebarSizeOnly = parts.Length == 2 ? parts[1] : "";
                            
                            if (!string.IsNullOrEmpty(rebarLabel) && !_rebarColors.ContainsKey(rebarLabel))
                            {
                                _rebarColors[rebarLabel] = GetRebarColor(rebarSizeOnly);
                            }

                            _allColumns.Add(new ColumnRebarData
                            {
                                Name = tName,
                                SectionName = propName,
                                RebarChanged = false,
                                Story = cStory,
                                X = x1,
                                Y = y1, // Plan görünümü için X,Y I düğümü
                                RebarLabel = string.IsNullOrEmpty(rebarLabel) ? "Bilinmiyor" : rebarLabel,
                                DisplayColor = string.IsNullOrEmpty(rebarLabel) ? Color.Gray : _rebarColors[rebarLabel],
                                Width = t2, // 2 ekseni m cinsinden
                                Depth = t3, // 3 ekseni m cinsinden
                                Angle = angle,
                                Shape = shape
                            });
                        }
                        else if (propType == eFrameDesignOrientation.Beam)
                        {
                            _allBeams.Add(new BeamData
                            {
                                Name = tName,
                                Story = cStory,
                                X1 = x1,
                                Y1 = y1,
                                X2 = x2,
                                Y2 = y2
                            });
                        }
                    }
                }
            }

            // Perdeleri çek
            int numAreas = 0;
            string[] areaNames = null;
            model.AreaObj.GetNameList(ref numAreas, ref areaNames);
            
            if (numAreas > 0)
            {
                foreach (string aName in areaNames)
                {
                    eAreaDesignOrientation propType = eAreaDesignOrientation.Null;
                    model.AreaObj.GetDesignOrientation(aName, ref propType);

                    if (propType == eAreaDesignOrientation.Wall)
                    {
                        string cLabel = "", cStory = "";
                        model.AreaObj.GetLabelFromName(aName, ref cLabel, ref cStory);
                        if (string.IsNullOrEmpty(cStory)) cStory = "Bilinmiyor";

                        // Kalınlık Bulma
                        string propName = "";
                        model.AreaObj.GetProperty(aName, ref propName);
                        double thickness = 0.2; // Varsayılan

                        if (!string.IsNullOrEmpty(propName))
                        {
                            eWallPropType wType = default(eWallPropType);
                            eShellType sType = default(eShellType);
                            string matProp = "", notes = "", guid = "";
                            int color = 0;
                            model.PropArea.GetWall(propName, ref wType, ref sType, ref matProp, ref thickness, ref color, ref notes, ref guid);
                        }

                        int numPts = 0;
                        string[] pts = null;
                        model.AreaObj.GetPoints(aName, ref numPts, ref pts);

                        if (numPts > 0 && pts != null)
                        {
                            List<PointF> wPoints = new List<PointF>();
                            for (int i = 0; i < numPts; i++)
                            {
                                double ax = 0, ay = 0, az = 0;
                                model.PointObj.GetCoordCartesian(pts[i], ref ax, ref ay, ref az);
                                wPoints.Add(new PointF((float)ax, (float)ay));
                            }
                            
                            // Plan görünümü için sadece farklı olan (X,Y) noktalarını (çizgi çizebilmek için) al
                            var distinctXY = wPoints.GroupBy(p => new { X = Math.Round(p.X, 3), Y = Math.Round(p.Y, 3) })
                                                    .Select(g => g.First()).ToList();

                            _allWalls.Add(new WallData
                            {
                                Name = aName,
                                Story = cStory,
                                Points = distinctXY,
                                Thickness = thickness == 0 ? 0.2 : thickness
                            });
                        }
                    }
                }
            }
        }

        private string GetRebarLabel(cSapModel model, string propName)
        {
            int pattern = 0;
            string rebarSize = "";
            double cover = 0;
            bool toBeDesigned = false;

            string matPropLong = "", matPropConfine = "";
            int confineType = 0;
            double tieSpacingLongit = 0;
            int number2DirTieBars = 0, number3DirTieBars = 0;
            int numberCBars = 0, numberR3Bars = 0, numberR2Bars = 0;
            string tieSize = "";

            int ret = model.PropFrame.GetRebarColumn(propName, ref matPropLong, ref matPropConfine, ref pattern, ref confineType, ref cover, ref numberCBars, ref numberR3Bars, ref numberR2Bars, ref rebarSize, ref tieSize, ref tieSpacingLongit, ref number2DirTieBars, ref number3DirTieBars, ref toBeDesigned);
            
            if (ret == 0 && pattern == 1) // 1 = Rectangular
            {
                int totalBars = (numberR3Bars * 2) + (numberR2Bars * 2) - 4;
                if (totalBars < 4) totalBars = 4; // minimum

                string sizeNum = GetRebarNumber(rebarSize);
                return $"{totalBars}\u03C6{sizeNum}";
            }
            if (ret == 0 && pattern == 2) // 2 = Circular
            {
                string sizeNum = GetRebarNumber(rebarSize);
                return $"{numberCBars}\u03C6{sizeNum}";
            }

            return "";
        }

        private string GetRebarNumber(string rebarStr)
        {
            string numOnly = new String(rebarStr.Where(Char.IsDigit).ToArray());
            return numOnly != "" ? numOnly : rebarStr;
        }

        private Color GetRebarColor(string rebarNum)
        {
            switch (rebarNum)
            {
                case "16": return Color.Yellow;
                case "18": return Color.FromArgb(204, 51, 0); // ~Color 23 (AutoCAD)
                case "20": return Color.FromArgb(255, 127, 0); // ~Color 30 (AutoCAD)
                case "22": return Color.Magenta;
                case "25": return Color.FromArgb(178, 51, 102); // ~Color 192 (AutoCAD yaklasimi)
                default: return GetRandomBrightColor();
            }
        }

        private Color GetRandomBrightColor()
        {
            return Color.FromArgb(
                _rnd.Next(50, 200),
                _rnd.Next(50, 200),
                _rnd.Next(50, 200)
            );
        }

        private void UpdatePlanView()
        {
            if (cmbStory.SelectedIndex == -1) return;
            string selectedStory = cmbStory.SelectedItem.ToString();
            bool isRebarView = rbRebarView.Checked;

            var colsToShow = _allColumns.Where(c => c.Story == selectedStory).ToList();
            var beamsToShow = _allBeams.Where(c => c.Story == selectedStory).ToList();
            var wallsToShow = _allWalls.Where(c => c.Story == selectedStory).ToList();
            
            pnlPlanView.SetData(colsToShow, beamsToShow, wallsToShow, isRebarView);
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };
            Label textLabel = new Label() { Left = 15, Top = 20, Width = 300, Text = text };
            TextBox textBox = new TextBox() { Left = 15, Top = 50, Width = 300, Text = defaultValue };
            Button confirmation = new Button() { Text = "Tamam", Left = 135, Width = 80, Top = 90, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "İptal", Left = 235, Width = 80, Top = 90, DialogResult = DialogResult.Cancel };
            
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

    // ================= CUSTOM PLAN VIEW GÖRSELLEŞTİRME PANELİ =================
    public class PlanViewPanel : Panel
    {
        private List<ColumnRebarData> _columns = new List<ColumnRebarData>();
        private List<BeamData> _beams = new List<BeamData>();
        private List<WallData> _walls = new List<WallData>();
        
        // Etkileşim için form referansını veya rebar listesini ekleyelim
        public Action<ColumnRebarData> OnColumnClick { get; set; }
        
        private bool _isRebarView = true;
        private bool _showBeams = true;
        private bool _showWalls = true;

        // Viewport Properties
        private float _zoom = 1.0f;
        private PointF _offset = new PointF(0, 0);
        private Point _lastMousePos;
        private bool _isPanning = false;

        // Rectangle Selection (ETABS tarzı)
        private bool _isSelecting = false;
        private Point _selStart;
        private Point _selCurrent;
        private List<ColumnRebarData> _selectedColumns = new List<ColumnRebarData>();
        public Action<List<ColumnRebarData>> OnColumnsSelected { get; set; }
        public Action<List<ColumnRebarData>, string> OnRebarChangeRequested { get; set; }

        // Bounding Box (Auto Fit için)
        private double minX, maxX, minY, maxY;

        // Sağ tık menü
        private ContextMenuStrip _contextMenu;

        public PlanViewPanel()
        {
            this.DoubleBuffered = true;
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseUp += OnMouseUp;
            this.MouseWheel += OnMouseWheel;
            this.Resize += (s, e) => AutoFit();
            this.KeyDown += OnKeyDown;
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            // Sağ tık context menüsü
            _contextMenu = new ContextMenuStrip();
            var miChangeRebar = new ToolStripMenuItem("Seçili Elemanların Donatısını Değiştir");
            miChangeRebar.Click += (s, e) =>
            {
                if (_selectedColumns.Count == 0) return;
                string currentLabel = _selectedColumns[0].RebarLabel;
                string newRebar = Prompt.ShowDialog(
                    $"Seçili {_selectedColumns.Count} kolon için yeni donatı girin:",
                    "Çoklu Donatı Değiştir (Örn: 16φ20)",
                    currentLabel);
                if (!string.IsNullOrEmpty(newRebar))
                {
                    var colsCopy = new List<ColumnRebarData>(_selectedColumns);
                    _selectedColumns.Clear();
                    OnRebarChangeRequested?.Invoke(colsCopy, newRebar);
                    this.Invalidate();
                }
            };
            _contextMenu.Items.Add(miChangeRebar);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _selectedColumns.Clear();
                this.Invalidate();
            }
        }

        public List<ColumnRebarData> SelectedColumns => _selectedColumns;

        public void SetShowBeams(bool show) { _showBeams = show; this.Invalidate(); }
        public void SetShowWalls(bool show) { _showWalls = show; this.Invalidate(); }

        public void ClearData()
        {
            _columns.Clear();
            _beams.Clear();
            _walls.Clear();
            this.Invalidate();
        }

        public void SetData(List<ColumnRebarData> columns, List<BeamData> beams, List<WallData> walls, bool isRebarView)
        {
            _columns = columns;
            _beams = beams;
            _walls = walls;
            _isRebarView = isRebarView;

            CalculateBoundingBox();
            AutoFit();
        }

        private void CalculateBoundingBox()
        {
            var allPointsX = new List<double>();
            var allPointsY = new List<double>();

            foreach(var c in _columns) { allPointsX.Add(c.X); allPointsY.Add(c.Y); }
            foreach(var b in _beams) { allPointsX.Add(b.X1); allPointsX.Add(b.X2); allPointsY.Add(b.Y1); allPointsY.Add(b.Y2); }
            foreach(var w in _walls) { foreach(var p in w.Points) { allPointsX.Add(p.X); allPointsY.Add(p.Y); } }

            if (allPointsX.Count > 0)
            {
                minX = allPointsX.Min();
                maxX = allPointsX.Max();
                minY = allPointsY.Min();
                maxY = allPointsY.Max();
            }
            else
            {
                minX = -10; maxX = 10; minY = -10; maxY = 10;
            }
        }

        private void AutoFit()
        {
            if ((_columns.Count == 0 && _beams.Count == 0 && _walls.Count == 0) || this.Width == 0 || this.Height == 0) return;

            float padding = 50f; 
            float viewWidth = this.Width - padding * 2;
            float viewHeight = this.Height - padding * 2;

            double dataWidth = maxX - minX;
            double dataHeight = maxY - minY;

            if (dataWidth == 0) dataWidth = 10;
            if (dataHeight == 0) dataHeight = 10;

            float scaleX = viewWidth / (float)dataWidth;
            float scaleY = viewHeight / (float)dataHeight;

            _zoom = Math.Min(scaleX, scaleY);
            
            float targetCX = (float)(minX + dataWidth / 2f);
            float targetCY = (float)(minY + dataHeight / 2f);

            _offset.X = this.Width / 2f - targetCX * _zoom;
            // ETABS Y'si yukarı, ekran Y'si aşağı (dönüşüm Y = EkranY/2 + TargetCY * Zoom oluyor)
            _offset.Y = this.Height / 2f + targetCY * _zoom;

            this.Invalidate();
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (_columns.Count == 0 && _beams.Count == 0 && _walls.Count == 0) return;

            float zoomFactor = e.Delta > 0 ? 1.2f : 0.8f;
            
            float mouseX = e.X;
            float mouseY = e.Y;

            float dataX = (mouseX - _offset.X) / _zoom;
            float dataY = (mouseY - _offset.Y) / _zoom;

            _zoom *= zoomFactor;

            _offset.X = mouseX - dataX * _zoom;
            _offset.Y = mouseY - dataY * _zoom;

            this.Invalidate();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) // Pan (Gezinme) sadece tekerlek tuşu ile
            {
                _isPanning = true;
                _lastMousePos = e.Location;
                this.Cursor = Cursors.SizeAll;
            }
            if (e.Button == MouseButtons.Left) // Seçim
            {
                _isSelecting = true;
                _selStart = e.Location;
                _selCurrent = e.Location;
                this.Focus(); // ESC tuşunu alabilmek için
            }
            if (e.Button == MouseButtons.Right) // Sağ tık menü
            {
                if (_selectedColumns.Count > 0)
                {
                    _contextMenu.Show(this, e.Location);
                }
            }
        }

        private void HitTestColumn(Point mouseLocation)
        {
            foreach (var col in _columns)
            {
                var pt = EtabsToScreen(col.X, col.Y);
                float screenW = (float)(col.Width * _zoom);
                float screenH = (float)(col.Depth * _zoom);
                
                float minDim = Math.Min(screenW, screenH);
                if (minDim > 0 && minDim < 4) 
                {
                    float ratio = 4f / minDim;
                    screenW *= ratio;
                    screenH *= ratio;
                }
                
                float rad = (float)(col.Angle * Math.PI / 180.0);
                float cosA = Math.Abs((float)Math.Cos(rad));
                float sinA = Math.Abs((float)Math.Sin(rad));
                
                float bboxW = screenW * cosA + screenH * sinA + 12f; // padding=12
                float bboxH = screenW * sinA + screenH * cosA + 12f;

                RectangleF bounds = new RectangleF(pt.X - bboxW / 2, pt.Y - bboxH / 2, bboxW, bboxH);
                if (bounds.Contains(mouseLocation))
                {
                    // Toggle seçim: seçiliyse çıkar, değilse ekle
                    if (_selectedColumns.Contains(col))
                        _selectedColumns.Remove(col);
                    else
                        _selectedColumns.Add(col);
                    
                    this.Invalidate();
                    break;
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isPanning)
            {
                float dx = e.X - _lastMousePos.X;
                float dy = e.Y - _lastMousePos.Y;
                
                _offset.X += dx;
                _offset.Y += dy;
                
                _lastMousePos = e.Location;
                this.Invalidate();
            }
            if (_isSelecting)
            {
                _selCurrent = e.Location;
                this.Invalidate();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (_isSelecting && e.Button == MouseButtons.Left)
            {
                _isSelecting = false;
                
                int dx = Math.Abs(_selCurrent.X - _selStart.X);
                int dy = Math.Abs(_selCurrent.Y - _selStart.Y);
                
                if (dx < 5 && dy < 5)
                {
                    // Tek tıklama => Kolon tespiti yap
                    HitTestColumn(e.Location);
                }
                else
                {
                    // Dikdörtgen seçim - mevcut seçime ekle (temizleme)
                    bool leftToRight = _selCurrent.X >= _selStart.X;
                    RectangleF selRect = GetSelectionRect();
                    
                    foreach (var col in _columns)
                    {
                        if (_selectedColumns.Contains(col)) continue; // Zaten seçili, atla
                        
                        RectangleF colBounds = GetColumnScreenBounds(col);
                        
                        if (leftToRight)
                        {
                            // Soldan sağa: tamamen içinde kalanlar
                            if (selRect.Contains(colBounds))
                            {
                                _selectedColumns.Add(col);
                            }
                        }
                        else
                        {
                            // Sağdan sola: dikdörtgene değen/kesişenler
                            if (selRect.IntersectsWith(colBounds))
                            {
                                _selectedColumns.Add(col);
                            }
                        }
                    }
                    
                    if (_selectedColumns.Count > 0)
                    {
                        OnColumnsSelected?.Invoke(_selectedColumns);
                    }
                }
                
                this.Invalidate();
            }
            
            _isPanning = false;
            this.Cursor = Cursors.Default;
        }

        private RectangleF GetSelectionRect()
        {
            float x = Math.Min(_selStart.X, _selCurrent.X);
            float y = Math.Min(_selStart.Y, _selCurrent.Y);
            float w = Math.Abs(_selCurrent.X - _selStart.X);
            float h = Math.Abs(_selCurrent.Y - _selStart.Y);
            return new RectangleF(x, y, w, h);
        }

        private RectangleF GetColumnScreenBounds(ColumnRebarData col)
        {
            var pt = EtabsToScreen(col.X, col.Y);
            float screenW = (float)(col.Width * _zoom);
            float screenH = (float)(col.Depth * _zoom);
            
            float minDim = Math.Min(screenW, screenH);
            if (minDim > 0 && minDim < 4)
            {
                float ratio = 4f / minDim;
                screenW *= ratio;
                screenH *= ratio;
            }
            
            float rad = (float)(col.Angle * Math.PI / 180.0);
            float cosA = Math.Abs((float)Math.Cos(rad));
            float sinA = Math.Abs((float)Math.Sin(rad));
            
            float bboxW = screenW * cosA + screenH * sinA;
            float bboxH = screenW * sinA + screenH * cosA;
            
            return new RectangleF(pt.X - bboxW / 2, pt.Y - bboxH / 2, bboxW, bboxH);
        }

        // ETABS uzayından => Ekran Piksellerine Dönüşüm
        private PointF EtabsToScreen(double x, double y)
        {
            return new PointF(
                (float)(x * _zoom) + _offset.X,
                (float)(-y * _zoom) + _offset.Y // Y ekseni ters çevrilir
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_columns.Count == 0 && _beams.Count == 0 && _walls.Count == 0)
            {
                bool isLoading = this.Controls.OfType<Label>().Any(lbl => lbl.Visible);
                if(!isLoading)
                {
                    string msg = "Görüntülenecek veri yok.\nLütfen sol panelden bilgileri çekin.";
                    SizeF size = g.MeasureString(msg, this.Font);
                    g.DrawString(msg, this.Font, Brushes.Gray, (this.Width - size.Width) / 2, (this.Height - size.Height) / 2);
                }
                return;
            }

            // --- 1. Çizim Katmanı: PERDELER ---
            if (_showWalls)
            {
                foreach (var wall in _walls)
                {
                    if (wall.Points.Count < 2) continue;
                    PointF[] screenPts = wall.Points.Select(p => EtabsToScreen(p.X, p.Y)).ToArray();

                    // Perdenin merkezinden geçen çizgiyi kalınlığı ile çiz (Line Join ile)
                    using (Pen wallPen = new Pen(Color.FromArgb(120, 180, 180, 180), (float)(wall.Thickness * _zoom)))
                    {
                        wallPen.StartCap = LineCap.Square;
                        wallPen.EndCap = LineCap.Square;
                        
                        if (screenPts.Length == 2)
                        {
                            g.DrawLine(wallPen, screenPts[0], screenPts[1]);
                        }
                        else
                        {
                            g.DrawLines(wallPen, screenPts);
                        }
                    }
                }
            }

            // --- 2. Çizim Katmanı: KİRİŞLER ---
            if (_showBeams)
            {
                using (Pen beamPen = new Pen(Color.FromArgb(180, 100, 100, 100), 2f)) // Hafif soluk gri
                {
                    foreach (var beam in _beams)
                    {
                        var pt1 = EtabsToScreen(beam.X1, beam.Y1);
                        var pt2 = EtabsToScreen(beam.X2, beam.Y2);
                        g.DrawLine(beamPen, pt1, pt2);
                    }
                }
            }

            // --- 3. Çizim Katmanı: KOLONLAR ---
            Font labelFont = new Font("Segoe UI", 8, FontStyle.Bold);
            
            // Eğer tip görünümüne geçildiyse objelere tiplere özel atanmış renk üretelim:
            var typeColors = new Dictionary<string, Color>();
            if (!_isRebarView)
            {
                var rng = new Random(12345);
                var allTypes = _columns.Select(c => c.TypeLabel).Distinct().ToList();

                Color[] predefinedColors = {
                    Color.Cyan,
                    Color.Magenta,
                    Color.Yellow,
                    Color.FromArgb(255, 127, 0), // Color 30 (Turuncu)
                    Color.Black, // Beyaz istendi ama arka plan beyaz olduğu için Siyah (AutoCAD Color 7)
                    Color.Red,
                    Color.Green,
                    Color.FromArgb(204, 51, 0), // Color 23
                    Color.FromArgb(178, 51, 102), // Color 192
                    Color.Blue
                };

                foreach(var t in allTypes)
                {
                    int typeIndex = 0;
                    if (t.StartsWith("T") && int.TryParse(t.Substring(1), out typeIndex) && typeIndex > 0)
                    {
                        if (typeIndex <= 10)
                        {
                            typeColors[t] = predefinedColors[typeIndex - 1];
                        }
                        else
                        {
                            // 10'dan büyükse rastgele farklı bir renk üret
                            Color randomColor;
                            do
                            {
                                randomColor = Color.FromArgb(rng.Next(50, 220), rng.Next(50, 220), rng.Next(50, 220));
                            } while (predefinedColors.Any(pc => Math.Abs(pc.R - randomColor.R) < 20 && Math.Abs(pc.G - randomColor.G) < 20 && Math.Abs(pc.B - randomColor.B) < 20));
                            
                            typeColors[t] = randomColor;
                        }
                    }
                    else
                    {
                        typeColors[t] = Color.Gray;
                    }
                }
            }

            foreach (var col in _columns)
            {
                var pt = EtabsToScreen(col.X, col.Y);
                Color cColor = _isRebarView ? col.DisplayColor : (typeColors.ContainsKey(col.TypeLabel) ? typeColors[col.TypeLabel] : Color.Gray);
                string labelText = _isRebarView ? col.RebarLabel : col.TypeLabel;

                // Gerçek boyut ve rotasyon dönüşümleri
                GraphicsState state = g.Save();

                // Çizim merkezini kolon merkezine al
                g.TranslateTransform(pt.X, pt.Y);
                
                // Açıya göre çevir
                g.RotateTransform(-(float)col.Angle);

                float screenW = (float)(col.Width * _zoom);
                float screenH = (float)(col.Depth * _zoom);
                
                float minDim = Math.Min(screenW, screenH);
                if (minDim > 0 && minDim < 4) 
                {
                    float ratio = 4f / minDim;
                    screenW *= ratio;
                    screenH *= ratio;
                }

                using (SolidBrush b = new SolidBrush(cColor))
                {
                    if (col.Shape == 2) 
                    {
                        g.FillEllipse(b, -screenW / 2, -screenH / 2, screenW, screenH);
                        g.DrawEllipse(Pens.Black, -screenW / 2, -screenH / 2, screenW, screenH);
                    }
                    else 
                    {
                        g.FillRectangle(b, -screenW / 2, -screenH / 2, screenW, screenH);
                        g.DrawRectangle(Pens.Black, -screenW / 2, -screenH / 2, screenW, screenH);
                    }
                }

                g.Restore(state);

                if (!_isRebarView)
                {
                    // Kare/dikdörtgen çiz
                    float rad = (float)(col.Angle * Math.PI / 180.0);
                    float cosA = Math.Abs((float)Math.Cos(rad));
                    float sinA = Math.Abs((float)Math.Sin(rad));
                    
                    float bboxW = screenW * cosA + screenH * sinA + 12f; // padding=12
                    float bboxH = screenW * sinA + screenH * cosA + 12f;

                    using (Pen boxPen = new Pen(cColor, 1.5f))
                    {
                        g.DrawRectangle(boxPen, pt.X - bboxW / 2, pt.Y - bboxH / 2, bboxW, bboxH);
                    }

                    using (SolidBrush textBrush = new SolidBrush(cColor))
                    {
                        var textSize = g.MeasureString(labelText, labelFont);
                        // Üst orta noktaya hizala
                        float textX = pt.X - textSize.Width / 2;
                        float textY = pt.Y - bboxH / 2 - textSize.Height - 2f;
                        g.DrawString(labelText, labelFont, textBrush, textX, textY);
                        
                        // Donatı oranını altına yaz
                        string ratioText = $"%{KolonDonesiUI.CalculateRebarRatio(col):F2}";
                        var ratioSize = g.MeasureString(ratioText, labelFont);
                        float ratioX = pt.X - ratioSize.Width / 2;
                        float ratioY = pt.Y + bboxH / 2 + 2f; // Alt orta kısım
                        g.DrawString(ratioText, labelFont, textBrush, ratioX, ratioY);
                    }
                }
                else
                {
                    using (SolidBrush textBrush = new SolidBrush(cColor))
                    {
                        g.DrawString(labelText, labelFont, textBrush, pt.X + 8, pt.Y - 14);
                        
                        // Donatı oranını altına eklenecek
                        string ratioText = $"%{KolonDonesiUI.CalculateRebarRatio(col):F2}";
                        g.DrawString(ratioText, labelFont, textBrush, pt.X + 8, pt.Y);
                    }
                }
            }

            // Legend / Lejant
            if (_isRebarView)
            {
                var uniqueRebars = _columns.GroupBy(x => x.RebarLabel).Select(grp => grp.First()).ToList();
                int lx = 10, ly = 10;
                
                g.DrawString("Lejant (Donatı):", new Font("Segoe UI", 9, FontStyle.Bold), Brushes.Black, lx, ly);
                ly += 20;

                foreach (var ur in uniqueRebars)
                {
                    g.FillRectangle(new SolidBrush(ur.DisplayColor), lx, ly, 12, 12);
                    g.DrawRectangle(Pens.Black, lx, ly, 12, 12);
                    g.DrawString(ur.RebarLabel, labelFont, Brushes.Black, lx + 16, ly - 2);
                    ly += 18;
                }
            }
            else
            {
                var uniqueTypes = _columns.GroupBy(x => x.TypeLabel).Select(grp => grp.First()).OrderBy(x => x.TypeLabel).ToList();
                int lx = 10, ly = 10;

                g.DrawString("Lejant (Tiplere Göre Görünüm):", new Font("Segoe UI", 9, FontStyle.Bold), Brushes.Black, lx, ly);
                ly += 20;

                foreach(var ut in uniqueTypes)
                {
                    var tColor = typeColors.ContainsKey(ut.TypeLabel) ? typeColors[ut.TypeLabel] : Color.Gray;
                    g.FillRectangle(new SolidBrush(tColor), lx, ly, 12, 12);
                    g.DrawRectangle(Pens.Black, lx, ly, 12, 12);
                    g.DrawString(ut.TypeLabel, labelFont, Brushes.Black, lx + 16, ly - 2);
                    ly += 18;
                }
            }

            // Seçili kolonları vurgula
            if (_selectedColumns.Count > 0)
            {
                foreach (var col in _selectedColumns)
                {
                    if (!_columns.Contains(col)) continue;
                    var pt = EtabsToScreen(col.X, col.Y);
                    RectangleF bounds = GetColumnScreenBounds(col);
                    using (Pen hlPen = new Pen(Color.Cyan, 2))
                    {
                        hlPen.DashStyle = DashStyle.Dash;
                        g.DrawRectangle(hlPen, bounds.X, bounds.Y, bounds.Width, bounds.Height);
                    }
                }
            }

            // Seçim dikdörtgenini çiz (ETABS tarzı)
            if (_isSelecting)
            {
                RectangleF selRect = GetSelectionRect();
                bool leftToRight = _selCurrent.X >= _selStart.X;
                
                Color fillColor = leftToRight 
                    ? Color.FromArgb(30, 0, 120, 215)   // Mavi (Window selection)
                    : Color.FromArgb(30, 0, 180, 0);     // Yeşil (Crossing selection)
                Color borderColor = leftToRight 
                    ? Color.FromArgb(180, 0, 120, 215)
                    : Color.FromArgb(180, 0, 180, 0);

                using (SolidBrush fillBrush = new SolidBrush(fillColor))
                {
                    g.FillRectangle(fillBrush, selRect);
                }
                using (Pen borderPen = new Pen(borderColor, 1.5f))
                {
                    if (!leftToRight) borderPen.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(borderPen, selRect.X, selRect.Y, selRect.Width, selRect.Height);
                }
            }
        }
    }

    // ================= CUSTOM LOADING SPINNER PANELİ =================
    public class LoadingSpinner : Control
    {
        private Timer _timer;
        private int _angle = 0;

        public LoadingSpinner()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(80, 80);

            _timer = new Timer();
            _timer.Interval = 30; // ~33 fps
            _timer.Tick += (s, e) =>
            {
                _angle = (_angle + 10) % 360;
                this.Invalidate();
            };
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible) _timer.Start();
            else _timer.Stop();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.BackColor);

            int spinnerSize = 50;
            Rectangle rect = new Rectangle((this.Width - spinnerSize) / 2, (this.Height - spinnerSize) / 2, spinnerSize, spinnerSize);
            
            using (Pen pen = new Pen(Color.LightGray, 5))
            {
                e.Graphics.DrawArc(pen, rect, 0, 360);
            }
            using (Pen pen = new Pen(Color.DarkOrange, 5))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                e.Graphics.DrawArc(pen, rect, _angle, 90);
            }
        }
    }
}
