using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using CSiAPIv1;

namespace EtabsTools
{
    // ===== Perde Kesme Veri Yapıları =====

    public class PierStoryGeometry
    {
        public double Bw { get; set; } // cm
        public double Lw { get; set; } // cm
    }

    public class PierGeometryData
    {
        public double Hw { get; set; } // m
        public double BottomLw { get; set; } // cm
        public double Ratio { get; set; }
        public bool IsShort { get; set; }
        public Dictionary<string, PierStoryGeometry> Stories { get; set; } = new Dictionary<string, PierStoryGeometry>();
    }

    public class PerdeKesmeRowResult
    {
        public string Story { get; set; }
        public string Pier { get; set; }
        public double Bw { get; set; }
        public double Lw { get; set; }
        public double Vmax { get; set; }
        public double Vc { get; set; }
        public double Vd { get; set; }
        public double Vr { get; set; }
        public int N { get; set; }
        public int Phi { get; set; }
        public int S { get; set; }
        public double FckUsed { get; set; }
        public string Status { get; set; }
        public double KapVal { get; set; }
        public double PurVal { get; set; }
    }

    // ===== Perde Kesme Hesap Mantığı =====

    public class PerdeKesmeManager
    {
        private cSapModel SapModel;
        public Dictionary<string, Dictionary<string, object>> StoryData { get; set; } = new Dictionary<string, Dictionary<string, object>>();
        public List<string> OrderedStoryNames { get; set; } = new List<string>();
        public List<string> Combos { get; set; } = new List<string>();
        public List<string> Patterns { get; set; } = new List<string>();

        // Pier bazlı kuvvet verisi: (story, pier) -> Vd
        public Dictionary<string, double> PierData { get; set; } = new Dictionary<string, double>();

        public PerdeKesmeManager(cSapModel sapModel)
        {
            SapModel = sapModel;
        }

        public void LoadInitialData()
        {
            // Kombinasyonları ve load pattern'ları yükle
            try
            {
                int numCombos = 0;
                string[] comboNames = null;
                SapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                Combos = numCombos > 0 ? comboNames.ToList() : new List<string>();
            }
            catch { Combos = new List<string>(); }

            try
            {
                int numPatterns = 0;
                string[] patternNames = null;
                SapModel.LoadPatterns.GetNameList(ref numPatterns, ref patternNames);
                Patterns = numPatterns > 0 ? patternNames.ToList() : new List<string>();
            }
            catch { Patterns = new List<string>(); }

            // Kat bilgilerini yükle
            try
            {
                double baseElevation = 0;
                int numStories = 0;
                string[] storyNames = null;
                double[] storyElevations = null;
                double[] storyHeights = null;
                bool[] isMasterStory = null;
                string[] similarToStory = null;
                bool[] spliceAbove = null;
                double[] spliceHeight = null;
                int[] color_ = null;

                SapModel.Story.GetStories_2(ref baseElevation, ref numStories, ref storyNames, ref storyElevations, ref storyHeights, ref isMasterStory, ref similarToStory, ref spliceAbove, ref spliceHeight, ref color_);

                if (numStories > 0)
                {
                    OrderedStoryNames = storyNames.Select(s => s.Trim()).ToList();
                    for (int i = 0; i < OrderedStoryNames.Count; i++)
                    {
                        string name = OrderedStoryNames[i];
                        double topZ = storyElevations[i];
                        double height = storyHeights[i];
                        double bottomZ = topZ - height;
                        StoryData[name] = new Dictionary<string, object>
                        {
                            { "elevation", topZ },
                            { "top_z", topZ },
                            { "bottom_z", bottomZ },
                            { "height", height },
                            { "order", i }
                        };
                    }
                }
            }
            catch { }
        }

        public Dictionary<string, PierGeometryData> GetPierGeometryData()
        {
            var result = new Dictionary<string, PierGeometryData>();
            try
            {
                int numPiers = 0;
                string[] pierNames = null;
                SapModel.PierLabel.GetNameList(ref numPiers, ref pierNames);

                if (numPiers <= 0) return result;

                foreach (string pier in pierNames)
                {
                    int numStories = 0;
                    string[] apiStoryNames = null;
                    double[] axisAngle = null;
                    int[] numAreaObjs = null;
                    int[] numLineObjs = null;
                    double[] widthBot = null;
                    double[] thickBot = null;
                    double[] widthTop = null;
                    double[] thickTop = null;
                    string[] matProp = null;
                    double[] cgBotX = null, cgBotY = null, cgBotZ = null;
                    double[] cgTopX = null, cgTopY = null, cgTopZ = null;

                    SapModel.PierLabel.GetSectionProperties(pier, ref numStories, ref apiStoryNames,
                        ref axisAngle, ref numAreaObjs, ref numLineObjs,
                        ref widthBot, ref thickBot, ref widthTop, ref thickTop,
                        ref matProp, ref cgBotX, ref cgBotY, ref cgBotZ,
                        ref cgTopX, ref cgTopY, ref cgTopZ);

                    var geoData = new PierGeometryData();

                    if (numStories > 0 && apiStoryNames != null)
                    {
                        for (int i = 0; i < numStories; i++)
                        {
                            string sName = apiStoryNames[i].Trim();
                            double sWidth = widthBot[i] * 100; // m -> cm
                            double sThick = thickBot[i] * 100;
                            geoData.Stories[sName] = new PierStoryGeometry { Bw = sThick, Lw = sWidth };
                        }
                    }

                    // Hw hesabı
                    double minElev = double.MaxValue;
                    double maxElev = double.MinValue;
                    bool foundStories = false;

                    foreach (var sName in geoData.Stories.Keys)
                    {
                        if (StoryData.ContainsKey(sName))
                        {
                            foundStories = true;
                            double botZ = (double)StoryData[sName]["bottom_z"];
                            double topZ = (double)StoryData[sName]["top_z"];
                            if (botZ < minElev) minElev = botZ;
                            if (topZ > maxElev) maxElev = topZ;
                        }
                    }
                    geoData.Hw = foundStories ? (maxElev - minElev) : 0;

                    // Bottom Lw
                    double lowestZ = double.MaxValue;
                    foreach (var sName in geoData.Stories.Keys)
                    {
                        if (StoryData.ContainsKey(sName))
                        {
                            double z = (double)StoryData[sName]["bottom_z"];
                            if (z < lowestZ)
                            {
                                lowestZ = z;
                                geoData.BottomLw = geoData.Stories[sName].Lw;
                            }
                        }
                    }

                    double hwCm = geoData.Hw * 100;
                    geoData.Ratio = geoData.BottomLw > 0 ? hwCm / geoData.BottomLw : 0;
                    geoData.IsShort = geoData.Ratio > 0 && geoData.Ratio <= 2.0;

                    result[pier] = geoData;
                }
            }
            catch { }
            return result;
        }

        public int FetchForces(List<string> combos)
        {
            PierData.Clear();
            var tempData = new Dictionary<string, List<double>>();
            int processedCount = 0;

            foreach (string combo in combos)
            {
                try
                {
                    SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                    if (Patterns.Contains(combo))
                        SapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    else
                        SapModel.Results.Setup.SetComboSelectedForOutput(combo);

                    int numResults = 0;
                    string[] storyNames = null, pierNames = null, loadCases = null, locations = null;
                    double[] p = null, v2 = null, v3 = null, t = null, m2 = null, m3 = null;

                    SapModel.Results.PierForce(ref numResults, ref storyNames, ref pierNames,
                        ref loadCases, ref locations, ref p, ref v2, ref v3, ref t, ref m2, ref m3);

                    if (numResults > 0)
                    {
                        for (int i = 0; i < numResults; i++)
                        {
                            string key = storyNames[i].Trim() + "::" + pierNames[i].Trim();
                            if (!tempData.ContainsKey(key))
                                tempData[key] = new List<double>();
                            tempData[key].Add(v2[i]);
                            processedCount++;
                        }
                    }
                }
                catch { continue; }
            }

            // Her pier-kat için max |V2| hesapla
            foreach (var kvp in tempData)
            {
                double vd = kvp.Value.Max(v => Math.Abs(v));
                PierData[kvp.Key] = vd;
            }

            return processedCount;
        }

        public List<PerdeKesmeRowResult> PerformCalculation(double fck, double fyd,
            List<int> nOpts, List<int> fOpts, List<int> sOpts)
        {
            double fctd = 0.35 * Math.Sqrt(fck) / 1.5;
            var pierDims = GetPierGeometryData();

            // Çap kombinasyonlarını oluştur
            var combinations = new List<Dictionary<string, object>>();
            foreach (int n in nOpts)
            foreach (int f in fOpts)
            foreach (int s in sOpts)
            {
                double C = n * (Math.PI * Math.Pow(f * 0.1, 2) / 4) / s;
                combinations.Add(new Dictionary<string, object> {
                    { "C", C }, { "n", n }, { "f", f }, { "s", s }
                });
            }
            var sortedCombinations = combinations.OrderBy(x => (int)x["n"]).ThenBy(x => (double)x["C"]).ToList();

            var results = new List<PerdeKesmeRowResult>();
            var uniquePiers = PierData.Keys.Select(k => k.Split(new[] { "::" }, StringSplitOptions.None)[1])
                .Distinct().OrderBy(x => x).ToList();

            foreach (string pierName in uniquePiers)
            {
                var pierInfo = pierDims.ContainsKey(pierName) ? pierDims[pierName] : null;
                double hwCm = pierInfo?.Hw * 100 ?? 0;
                bool isShort = pierInfo?.IsShort ?? false;

                // Bu perde için tüm katları bul
                var pierStories = PierData.Keys
                    .Where(k => k.Split(new[] { "::" }, StringSplitOptions.None)[1] == pierName)
                    .Select(k => k.Split(new[] { "::" }, StringSplitOptions.None)[0])
                    .OrderByDescending(s => StoryData.ContainsKey(s) ? (int)StoryData[s]["order"] : -1)
                    .ToList();

                double maxCReq = 0;

                // İlk geçiş: C_req hesapla
                var storySnapshots = new List<PerdeKesmeRowResult>();
                foreach (string story in pierStories)
                {
                    string key = story + "::" + pierName;
                    if (!PierData.ContainsKey(key)) continue;

                    double bwCm = 0, lwCm = 0;
                    if (pierInfo?.Stories.ContainsKey(story) == true)
                    {
                        bwCm = pierInfo.Stories[story].Bw;
                        lwCm = pierInfo.Stories[story].Lw;
                    }

                    if (bwCm <= 0 || lwCm <= 0)
                    {
                        storySnapshots.Add(new PerdeKesmeRowResult
                        {
                            Story = story, Pier = pierName, Bw = bwCm, Lw = lwCm,
                            Status = "GEO ERR", FckUsed = fck
                        });
                        continue;
                    }

                    double vdOriginal = PierData[key];
                    double vdFinal = vdOriginal;
                    double coeff = 1.0;

                    if (isShort)
                    {
                        double localHwLw = lwCm > 0 ? hwCm / lwCm : 0;
                        if (localHwLw > 0)
                        {
                            double calcCoeff = 3.0 / (1.0 + localHwLw);
                            coeff = Math.Max(1.0, Math.Min(calcCoeff, 2.0));
                            vdFinal = vdOriginal * coeff;
                        }
                    }

                    double vmax = 0.085 * bwCm * lwCm * Math.Sqrt(fck);
                    double vc = 0.065 * bwCm * lwCm * fctd;
                    double vwReq = Math.Max(vdFinal - vc, 0);
                    double cReq = vwReq > 0 ? vwReq / (lwCm * fyd * 0.1) : 0;
                    if (cReq > maxCReq) maxCReq = cReq;

                    storySnapshots.Add(new PerdeKesmeRowResult
                    {
                        Story = story, Pier = pierName, Bw = bwCm, Lw = lwCm,
                        Vmax = vmax, Vc = vc, Vd = vdFinal, FckUsed = fck
                    });
                }

                // Optimal donatı kombinasyonunu bul
                Dictionary<string, object> finalCombo = null;
                foreach (var combo in sortedCombinations)
                {
                    if ((double)combo["C"] >= maxCReq)
                    {
                        finalCombo = combo;
                        break;
                    }
                }
                if (finalCombo == null && sortedCombinations.Count > 0)
                    finalCombo = sortedCombinations.Last();

                if (finalCombo == null) continue;

                int selN = (int)finalCombo["n"];
                int selF = (int)finalCombo["f"];
                int selS = (int)finalCombo["s"];
                double selC = (double)finalCombo["C"];

                foreach (var snap in storySnapshots)
                {
                    if (snap.Status == "GEO ERR")
                    {
                        results.Add(snap);
                        continue;
                    }

                    double vw = selC * snap.Lw * fyd * 0.1;
                    double vr = snap.Vc + vw;

                    string status = "O.K.";
                    if (snap.Vd > snap.Vmax)
                        status = "NOT O.K. (Vd > Vmax)";
                    else if (snap.Vd > vr)
                        status = "NOT O.K. (Vd > Vr)";
                    else if (vr > snap.Vmax)
                        status = "NOT O.K. (Vr > Vmax)";
                    else
                    {
                        double ashProv = selN * (Math.PI * Math.Pow(selF / 10.0, 2) / 4) * (100.0 / selS);
                        double ashLimit = 0.0025 * 100 * snap.Bw;
                        if (ashProv < ashLimit)
                            status = "NOT OK Min. Donatı";
                    }

                    snap.N = selN;
                    snap.Phi = selF;
                    snap.S = selS;
                    snap.Vr = vr;
                    snap.Status = status;
                    snap.KapVal = snap.Vmax > 0 ? snap.Vd / snap.Vmax : 0;
                    snap.PurVal = vr > 0 ? snap.Vd / vr : 0;

                    results.Add(snap);
                }
            }

            return results;
        }
    }

    // ===== Perde Kesme UI Sınıfı =====

    public class PerdeKesmeUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<int, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        // UI Kontrolleri
        private TextBox txtFck, txtFyd;
        private ListBox lstCombinations;
        private FlowLayoutPanel pnlSelectedCombos;
        private DataGridView dgvResults;
        private Label lblStatus;
        private PerdeKesmeManager _manager;
        private List<PerdeKesmeRowResult> _lastResults;

        public PerdeKesmeUI(Form1 form, Func<cSapModel> getSapModel,
            Func<int, Panel> createNavigationPanel, Action<int> goToPage, Color colorBackground)
        {
            _form = form;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNavigationPanel;
            _goToPage = goToPage;
            _colorBackground = colorBackground;
        }

        public void Initialize(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = Form1.CreateHeaderLabel("Perde Kesme Güvenliği Kontrolü");
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp.Padding = new Padding(20, 10, 20, 10);

            // =============== SOL PANEL ===============
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 225F)); // Kombinasyonlar
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Parametreler
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Butonlar

            // --- KOMBİNASYON SEÇİM ALANI ---
            TableLayoutPanel tlpCombos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                Margin = new Padding(0, 0, 15, 10)
            };
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Sol: Kombinasyonlar paneli
            RoundedPanel pnlCombos = new RoundedPanel
            {
                Title = "Yük Kombinasyonları",
                Dock = DockStyle.Fill,
                BorderRadius = 15,
                Margin = new Padding(0, 0, 8, 0)
            };

            lstCombinations = new ListBox
            {
                Location = new Point(20, 45),
                Size = new Size(160, 115),
                SelectionMode = SelectionMode.MultiExtended,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.FromArgb(250, 252, 255),
                BorderStyle = BorderStyle.None
            };
            lstCombinations.DoubleClick += LstCombinations_DoubleClick;

            SmoothButton btnLoadCombos = new SmoothButton
            {
                Text = "Getir",
                Size = new Size(65, 30),
                Location = new Point(20, 170),
                BaseColor = Color.FromArgb(204, 229, 255), // Soft Mavi (Ferah Açık Mavi)
                BorderRadius = 12,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnLoadCombos.Click += BtnLoadCombos_Click;

            SmoothButton btnSelectCombos = new SmoothButton
            {
                Text = "Seç",
                Size = new Size(65, 30),
                Location = new Point(95, 170),
                BaseColor = Color.FromArgb(204, 229, 255),
                BorderRadius = 12,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnSelectCombos.Click += BtnSelectCombos_Click;

            pnlCombos.Controls.Add(lstCombinations);
            pnlCombos.Controls.Add(btnLoadCombos);
            pnlCombos.Controls.Add(btnSelectCombos);
            tlpCombos.Controls.Add(pnlCombos, 0, 0);

            // Sağ: Seçili Kombinasyonlar paneli
            RoundedPanel pnlSelectedWrapper = new RoundedPanel
            {
                Title = "Seçilenler",
                Dock = DockStyle.Fill,
                BorderRadius = 15,
                Margin = new Padding(8, 0, 0, 0)
            };

            pnlSelectedCombos = new FlowLayoutPanel
            {
                Location = new Point(15, 45),
                Size = new Size(160, 150),
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
                Title = "Hesap Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 15,
                Margin = new Padding(0, 5, 15, 10)
            };

            int startY = 45;
            int gapY = 35;
            int labelX = 20;
            int textX = 140;
            int textW = 80;

            Font lblFont = new Font("Segoe UI", 9.5f);
            Color lblColor = Color.FromArgb(113, 128, 150);

            pnlParams.Controls.Add(new Label { Text = "fck (MPa):", Location = new Point(labelX, startY), AutoSize = true, Font = lblFont, ForeColor = lblColor });
            txtFck = new TextBox { Location = new Point(textX, startY - 2), Width = textW, Text = "30", Font = lblFont };
            pnlParams.Controls.Add(txtFck);

            pnlParams.Controls.Add(new Label { Text = "fyd (MPa):", Location = new Point(labelX, startY + gapY), AutoSize = true, Font = lblFont, ForeColor = lblColor });
            txtFyd = new TextBox { Location = new Point(textX, startY + gapY - 2), Width = textW, Text = "420", Font = lblFont };
            pnlParams.Controls.Add(txtFyd);

            tlpLeft.Controls.Add(pnlParams, 0, 1);

            // --- HESAPLA VE KAYDET BUTONLARI ---
            Panel pnlButton = new Panel { Dock = DockStyle.Fill };

            SmoothButton btnFetch = new SmoothButton
            {
                Text = "VERİ ÇEK",
                Size = new Size(110, 45),
                Location = new Point(10, 5),
                BaseColor = Color.FromArgb(204, 229, 255),
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnFetch.Click += BtnFetch_Click;
            pnlButton.Controls.Add(btnFetch);

            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "HESAPLA",
                Size = new Size(110, 45),
                Location = new Point(130, 5),
                BaseColor = Color.FromArgb(204, 229, 255),
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnCalculate.Click += BtnCalculate_Click;
            pnlButton.Controls.Add(btnCalculate);

            SmoothButton btnExport = new SmoothButton
            {
                Text = "KAYDET",
                Size = new Size(90, 45),
                Location = new Point(250, 5),
                BaseColor = Color.FromArgb(235, 240, 245),
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnExport.Click += BtnExport_Click;
            pnlButton.Controls.Add(btnExport);

            tlpLeft.Controls.Add(pnlButton, 0, 2);

            pnlLeft.Controls.Add(tlpLeft);
            tlp.Controls.Add(pnlLeft, 0, 0);

            // =============== SAĞ PANEL - SONUÇLAR ===============
            RoundedPanel pnlResults = new RoundedPanel
            {
                Title = "Hesap Sonuçları",
                Dock = DockStyle.Fill,
                BorderRadius = 15,
                Margin = new Padding(15, 0, 0, 5),
                TitleFont = new Font("Segoe UI Semibold", 14f, FontStyle.Regular)
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
                Font = new Font("Segoe UI", 9f),
                ScrollBars = ScrollBars.Both,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(244, 247, 254),
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 40,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(244, 247, 254),
                    ForeColor = Color.FromArgb(113, 128, 150),
                    Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    SelectionBackColor = Color.FromArgb(244, 247, 254)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    SelectionBackColor = Color.FromArgb(235, 240, 245),
                    SelectionForeColor = Color.FromArgb(43, 54, 116),
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(45, 55, 72)
                }
            };
            dgvResults.Columns.Add("Kat", "Kat");
            dgvResults.Columns.Add("Perde", "Perde");
            dgvResults.Columns.Add("fck", "fck");
            dgvResults.Columns.Add("bw", "bw (cm)");
            dgvResults.Columns.Add("lw", "lw (cm)");
            dgvResults.Columns.Add("n", "n");
            dgvResults.Columns.Add("phi", "φ");
            dgvResults.Columns.Add("s", "s (cm)");
            dgvResults.Columns.Add("Vmax", "Vmax (kN)");
            dgvResults.Columns.Add("Vr", "Vr (kN)");
            dgvResults.Columns.Add("Vd", "Vd (kN)");
            dgvResults.Columns.Add("Durum", "Durum");
            dgvResults.Columns.Add("Kapasite", "Kapasite");
            dgvResults.Columns.Add("Purlama", "Pürüzlülük");

            dgvResults.CellFormatting += DgvResults_CellFormatting;

            lblStatus = new Label
            {
                Text = "Sonuç Bekleniyor...",
                Dock = DockStyle.Bottom,
                Height = 35,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Italic),
                ForeColor = Color.FromArgb(163, 174, 208),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel dgvContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 45, 20, 15)
            };
            dgvContainer.Controls.Add(dgvResults);
            dgvContainer.Controls.Add(lblStatus);

            pnlResults.Controls.Add(dgvContainer);
            tlp.Controls.Add(pnlResults, 1, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            page.Tag = 6;
            page.VisibleChanged += (s, e) =>
            {
                if (page.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = _createNavigationPanel(6);
                    mainLayout.Controls.Add(navPanel, 0, 2);
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
            Panel tag = new Panel
            {
                Size = new Size(120, 24),
                BackColor = Color.White,
                Margin = new Padding(3),
                Tag = comboName
            };
            tag.Paint += (s, e) =>
            {
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
                ToastForm.ShowToast("Önce ETABS'a bağlanın!", _form, 2000);
                return;
            }

            lstCombinations.Items.Clear();
            try
            {
                int numCombos = 0;
                string[] comboNames = null;
                sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                if (numCombos > 0)
                    foreach (var name in comboNames) lstCombinations.Items.Add(name);

                int numPatterns = 0;
                string[] patternNames = null;
                sapModel.LoadPatterns.GetNameList(ref numPatterns, ref patternNames);
                if (numPatterns > 0)
                    foreach (var name in patternNames) lstCombinations.Items.Add(name);

                ToastForm.ShowToast($"{lstCombinations.Items.Count} kombinasyon yüklendi.", _form, 2000);
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Hata: " + ex.Message, _form, 2000);
            }
        }

        // Veri çek
        private void BtnFetch_Click(object sender, EventArgs e)
        {
            var sapModel = _getSapModel();
            if (sapModel == null)
            {
                ToastForm.ShowToast("Önce ETABS'a bağlanın!", _form, 2000);
                return;
            }

            // Seçili kombinasyonları topla
            var selectedCombos = new List<string>();
            foreach (Control c in pnlSelectedCombos.Controls)
                if (c.Tag != null) selectedCombos.Add(c.Tag.ToString());

            if (selectedCombos.Count == 0)
            {
                ToastForm.ShowToast("Lütfen kombinasyon seçin!", _form, 2000);
                return;
            }

            try
            {
                _manager = new PerdeKesmeManager(sapModel);
                _manager.LoadInitialData();
                int count = _manager.FetchForces(selectedCombos);

                lblStatus.Text = $"{_manager.PierData.Count} adet perde-kat verisi çekildi ({count} satır).";
                lblStatus.ForeColor = Color.FromArgb(72, 187, 120);
                ToastForm.ShowToast($"Veriler ETABS'tan başarıyla çekildi.", _form, 2000);
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Hata: " + ex.Message, _form, 2000);
            }
        }

        // Hesapla
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (_manager == null || _manager.PierData.Count == 0)
            {
                ToastForm.ShowToast("Önce veri çekin!", _form, 2000);
                return;
            }

            try
            {
                double fck = double.Parse(txtFck.Text);
                double fyd = double.Parse(txtFyd.Text);

                // Standart donatı seçenekleri
                var nOpts = new List<int> { 2, 3, 4 };
                var fOpts = new List<int> { 10, 12, 14, 16, 20, 22, 24, 26 };
                var sOpts = new List<int> { 10, 15, 20, 25 };

                _lastResults = _manager.PerformCalculation(fck, fyd, nOpts, fOpts, sOpts);

                // Sonuçları tabloya yaz
                dgvResults.Rows.Clear();
                foreach (var r in _lastResults)
                {
                    dgvResults.Rows.Add(
                        r.Story, r.Pier, r.FckUsed, r.Bw.ToString("F0"), r.Lw.ToString("F0"),
                        r.N, r.Phi, r.S,
                        r.Vmax.ToString("F0"), r.Vr.ToString("F0"), r.Vd.ToString("F0"),
                        r.Status,
                        r.KapVal.ToString("F2"), r.PurVal.ToString("F2")
                    );
                }

                int failCount = _lastResults.Count(r => r.Status != "O.K.");
                int okCount = _lastResults.Count(r => r.Status == "O.K.");

                if (failCount == 0)
                {
                    lblStatus.Text = $"Tüm perdeler güvenli. ({okCount} perde-kat kontrol edildi)";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblStatus.Text = $"{failCount} adet perde-kat yetersiz! ({okCount} güvenli, {failCount} yetersiz)";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Hesap hatası: " + ex.Message, _form, 2000);
            }
        }

        // DataGridView hücre boyama
        private void DgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvResults.Columns["Durum"].Index && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "O.K.")
                {
                    e.CellStyle.BackColor = Color.FromArgb(198, 239, 206);
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
                else if (val.Contains("NOT"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 199, 206);
                    e.CellStyle.ForeColor = Color.DarkRed;
                }
            }
        }

        // Excel'e kaydet
        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_lastResults == null || _lastResults.Count == 0)
            {
                ToastForm.ShowToast("Önce hesaplama yapın!", _form, 2000);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Dosyası|*.csv";
                sfd.FileName = "PerdeKesmeRaporu.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine("Kat;Perde;fck (MPa);bw (cm);lw (cm);n;φ;s (cm);Vmax (kN);Vr (kN);Vd (kN);Durum;Kapasite;Pürüzlülük");
                            foreach (var r in _lastResults)
                            {
                                sw.WriteLine($"{r.Story};{r.Pier};{r.FckUsed:F0};{r.Bw:F0};{r.Lw:F0};{r.N};{r.Phi};{r.S};{r.Vmax:F0};{r.Vr:F0};{r.Vd:F0};{r.Status};{r.KapVal:F2};{r.PurVal:F2}");
                            }
                        }
                        ToastForm.ShowToast("Rapor başarıyla kaydedildi.", _form, 2000);
                    }
                    catch (Exception ex)
                    {
                        ToastForm.ShowToast("Kaydetme hatası: " + ex.Message, _form, 2000);
                    }
                }
            }
        }

        public void Reset()
        {
            txtFck.Text = "30";
            txtFyd.Text = "420";
            pnlSelectedCombos.Controls.Clear();
            dgvResults.Rows.Clear();
            lblStatus.Text = "Sonuç Bekleniyor...";
            lblStatus.ForeColor = Color.FromArgb(163, 174, 208);
            _manager = null;
            _lastResults = null;
        }
    }
}
