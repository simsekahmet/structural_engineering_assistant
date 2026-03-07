using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using CSiAPIv1;

namespace EtabsTools
{
    public class PerdeEksenelUI
    {
        private Form _form;
        private Func<cSapModel> _getSapModel;
        private Func<string, int, string, Panel> _createNavigationPanel;

        private RoundedPanel pnlSelectedCombos;
        private ListBox lstCombos;
        private Label lblTotalCombos;

        private FlowLayoutPanel pnlWallSelectedCombos;
        private DataGridView dgvWallResults;
        private Label lblWallStatus;

        private TextBox txtFck;
        private TextBox txtLimit;

        private List<string> _wallSelectedCombos = new List<string>();
        private WallAxialLogic _wallLogic;
        private List<ResultRow> _lastWallResults = new List<ResultRow>();


        public PerdeEksenelUI(Form form, Func<cSapModel> getSapModel, Func<string, int, string, Panel> createNav)
        {
            _form = form;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNav;
        }

        public void Initialize(Control page)
        {
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.FromArgb(245, 247, 250)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F)); // Header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Body
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Navigation

            // =============== HEADER ===============
            Label lblHeader = Form1.CreateHeaderLabel("Perde Eksenel Yük Kontrolü");
            mainLayout.Controls.Add(lblHeader, 0, 0);

            // =============== BODY ===============
            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.Padding = new Padding(20, 10, 20, 10);

            // =============== SOL PANEL (Parametreler) ===============
            Panel pnlLeftScroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 0, 10, 0)
            };

            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 320F)); // Artırıldı ki taşmasın
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // --- Kombinasyon Kartı (Üst Sol) ---
            TableLayoutPanel tlpCombos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 1
            };
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCombos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            RoundedPanel pnlAllCombos = new RoundedPanel
            {
                Title = "Tüm Kombinasyonlar",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 5, 0)
            };
            lstCombos = new ListBox
            {
                Location = new Point(15, 35),
                Width = 140,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                SelectionMode = SelectionMode.MultiExtended
            };
            pnlAllCombos.Controls.Add(lstCombos);

            SmoothButton btnLoadCombos = new SmoothButton
            {
                Text = "Getir",
                Size = new Size(130, 30),
                Location = new Point(15, 195),
                BaseColor = Color.FromArgb(52, 152, 219),
                BorderRadius = 10,
                Font = new Font("Segoe UI", 9)
            };
            btnLoadCombos.Click += BtnLoadCombos_Click;
            pnlAllCombos.Controls.Add(btnLoadCombos);

            SmoothButton btnSelectCombos = new SmoothButton
            {
                Text = "Seç",
                Size = new Size(130, 30),
                Location = new Point(15, 235),
                BaseColor = Color.FromArgb(46, 204, 113),
                BorderRadius = 10,
                Font = new Font("Segoe UI", 9)
            };
            btnSelectCombos.Click += BtnSelectCombos_Click;
            pnlAllCombos.Controls.Add(btnSelectCombos);
            tlpCombos.Controls.Add(pnlAllCombos, 0, 0);

            pnlSelectedCombos = new RoundedPanel
            {
                Title = "Seçilenler",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(5, 0, 0, 0)
            };
            pnlWallSelectedCombos = new FlowLayoutPanel
            {
                Location = new Point(15, 35),
                Width = 155,
                Height = 230, // Yüksekliği Temizle butonunun alanına kadar uzatalım
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlSelectedCombos.Controls.Add(pnlWallSelectedCombos);
            
            tlpCombos.Controls.Add(pnlSelectedCombos, 1, 0);
            tlpLeft.Controls.Add(tlpCombos, 0, 0);

            // --- Hesap Ayarları (Orta Sol) ---
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Ayarları",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 5, 0, 5)
            };

            pnlParams.Controls.Add(new Label { Text = "Beton (fck):", Location = new Point(15, 45), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtFck = new TextBox { Location = new Point(110, 42), Width = 50, Text = "30", Font = new Font("Segoe UI", 10) };
            pnlParams.Controls.Add(txtFck);
            pnlParams.Controls.Add(new Label { Text = "MPa", Location = new Point(165, 45), AutoSize = true, Font = new Font("Segoe UI", 10) });

            pnlParams.Controls.Add(new Label { Text = "Sınır (limit):", Location = new Point(15, 80), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtLimit = new TextBox { Location = new Point(110, 77), Width = 50, Text = "0.35", Font = new Font("Segoe UI", 10) };
            pnlParams.Controls.Add(txtLimit);

            tlpLeft.Controls.Add(pnlParams, 0, 1);

            // --- Butonlar (Alt Sol) ---
            Panel pnlButton = new Panel { Dock = DockStyle.Fill };
            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "HESAPLA",
                Size = new Size(120, 40),
                Location = new Point(15, 5),
                BaseColor = Color.FromArgb(255, 230, 204), // Açık Turuncu
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI", 10f, FontStyle.Regular)
            };
            btnCalculate.Click += BtnCalculate_Click;
            pnlButton.Controls.Add(btnCalculate);

            SmoothButton btnExcel = new SmoothButton
            {
                Text = "Excel'e Aktar",
                Size = new Size(145, 40),
                Location = new Point(145, 5),
                BaseColor = Color.FromArgb(204, 255, 204), // Açık Yeşil
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular)
            };
            btnExcel.Click += BtnExcelExport_Click;
            pnlButton.Controls.Add(btnExcel);

            tlpLeft.Controls.Add(pnlButton, 0, 2);

            pnlLeftScroll.Controls.Add(tlpLeft);
            tlp.Controls.Add(pnlLeftScroll, 0, 0);

            // =============== SAĞ PANEL (Sonuçlar) ===============
            RoundedPanel pnlResults = new RoundedPanel
            {
                Title = "Sonuçlar",
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                Margin = new Padding(10, 0, 0, 0),
                TitleFont = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            dgvWallResults = new DataGridView
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
                    Font = new Font("Segoe UI Semibold", 9f),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            dgvWallResults.Columns.Add("Story", "Kat");
            dgvWallResults.Columns.Add("Label", "Perde");
            dgvWallResults.Columns.Add("Case", "Load Case");
            dgvWallResults.Columns.Add("Fck", "fck (MPa)");
            dgvWallResults.Columns.Add("Bw", "b_w (cm)");
            dgvWallResults.Columns.Add("Lw", "l_w (cm)");
            dgvWallResults.Columns.Add("Ac", "Ac (cm2)");
            dgvWallResults.Columns.Add("P", "P (kN)");
            dgvWallResults.Columns.Add("Ratio", "Oran");
            dgvWallResults.Columns.Add("Status", "Durum");

            lblWallStatus = new Label
            {
                Text = "",
                Dock = DockStyle.Bottom,
                Height = 25,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Blue,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel dgvContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 25, 15, 15) };
            dgvContainer.Controls.Add(dgvWallResults);
            dgvContainer.Controls.Add(lblWallStatus);
            pnlResults.Controls.Add(dgvContainer);
            tlp.Controls.Add(pnlResults, 1, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            int tabIndex = (int)page.Tag;
            page.VisibleChanged += (s, e) => {
                if (page.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = _createNavigationPanel(null, tabIndex, "ETABS");
                    mainLayout.Controls.Add(navPanel, 0, 2);
                }
            };

            page.Controls.Add(mainLayout);
        }

        private cSapModel SapModel => _getSapModel();

        // ----------------------------------------------------
        // KOMBİNASYON İŞLEMLERİ
        // ----------------------------------------------------
        private void BtnLoadCombos_Click(object sender, EventArgs e)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("ETABS modeli bulunamadı.", _form, 2000);
                return;
            }

            int numberNames = 0;
            string[] myName = null;
            int returnCode = SapModel.RespCombo.GetNameList(ref numberNames, ref myName);

            lstCombos.Items.Clear();
            if (returnCode == 0 && myName != null)
            {
                foreach (string name in myName)
                {
                    lstCombos.Items.Add(name);
                }
                ToastForm.ShowToast($"{numberNames} kombinasyon yüklendi.", _form, 1500);
            }
            else
            {
                ToastForm.ShowToast("Kombinasyon okunamadı.", _form, 2000);
            }
        }

        private void BtnSelectCombos_Click(object sender, EventArgs e)
        {
            foreach (var item in lstCombos.SelectedItems)
            {
                string comboName = item.ToString();
                if (!_wallSelectedCombos.Contains(comboName))
                {
                    AddComboBadge(comboName);
                }
            }
        }

        private void AddComboBadge(string comboName)
        {
            _wallSelectedCombos.Add(comboName);
            Panel badge = new Panel { AutoSize = true, BackColor = Color.FromArgb(237, 242, 247), Padding = new Padding(2), Margin = new Padding(2) };
            Label lbl = new Label { Text = comboName, AutoSize = true, Font = new Font("Segoe UI", 8f), Location = new Point(5, 5) };
            Label del = new Label { Text = "X", AutoSize = true, Font = new Font("Segoe UI", 8f, FontStyle.Bold), ForeColor = Color.Red, Cursor = Cursors.Hand, Location = new Point(lbl.Right + 2, 5) };
            del.Click += (s, ev) => { _wallSelectedCombos.Remove(comboName); pnlWallSelectedCombos.Controls.Remove(badge); };
            badge.Controls.Add(lbl); badge.Controls.Add(del);
            badge.Resize += (s, ev) => { del.Left = lbl.Right + 2; badge.Width = del.Right + 5; };
            pnlWallSelectedCombos.Controls.Add(badge);
        }

        // ----------------------------------------------------
        // HESAPLAMA
        // ----------------------------------------------------
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("ETABS modeline bağlı değil.", _form, 2000);
                return;
            }

            if (_wallSelectedCombos.Count == 0)
            {
                ToastForm.ShowToast("Lütfen kombinasyon seçiniz.", _form, 2000);
                return;
            }

            if (!double.TryParse(txtFck.Text, out double fck) || fck <= 0)
            {
                ToastForm.ShowToast("Geçerli bir fck değeri giriniz.", _form, 2000);
                return;
            }

            if (!double.TryParse(txtLimit.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double limit) || limit <= 0)
            {
                ToastForm.ShowToast("Geçerli bir limit (örn: 0.35) giriniz.", _form, 2000);
                return;
            }

            lblWallStatus.Text = "Hesaplanıyor... Seçilen kombinasyonlar taranıyor.";
            lblWallStatus.ForeColor = Color.Orange;
            Application.DoEvents();

            try
            {
                if (_wallLogic == null)
                {
                    _wallLogic = new WallAxialLogic(SapModel);
                    _wallLogic.LoadInitialData();
                }

                int count = _wallLogic.FetchWallForces(_wallSelectedCombos);
                
                if (count == 0)
                {
                    MessageBox.Show("Seçili kombinasyonlar için herhangi bir perde (Wall) verisi bulunamadı veya tablo alınamadı. 'Element Forces - Area' (Perdeler için) sonuçlarını kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblWallStatus.Text = "Veri bulunamadı.";
                    lblWallStatus.ForeColor = Color.Red;
                    return;
                }

                _lastWallResults = _wallLogic.PerformCalculation(fck, limit);

                UpdateDGV();

                lblWallStatus.Text = $"Hesaplandı. Toplam Perde Kesiti Sayısı: {_lastWallResults.Count}";
                lblWallStatus.ForeColor = Color.Green;
                ToastForm.ShowToast("Hesaplama tamamlandı.", _form, 2000);
            }
            catch (Exception ex)
            {
                lblWallStatus.Text = "Hata oluştu.";
                lblWallStatus.ForeColor = Color.Red;
                ToastForm.ShowToast("Hata: " + ex.Message, _form, 3000);
            }
        }

        private void UpdateDGV()
        {
            dgvWallResults.Rows.Clear();

            foreach (var res in _lastWallResults)
            {
                int rowIndex = dgvWallResults.Rows.Add(
                    res.story,
                    res.pier,
                    res.load_case,
                    res.fck.ToString("0.0"),
                    res.b.ToString("0.0"),
                    res.d.ToString("0.0"),
                    res.Ac.ToString("0.0"),
                    res.P.ToString("0.00"),
                    res.ratio.ToString("0.000"),
                    res.status
                );

                UpdateDgvRowColor(rowIndex, res.status);
            }
        }

        private void UpdateDgvRowColor(int rowIndex, string status)
        {
            if (status != "OK")
            {
                dgvWallResults.Rows[rowIndex].Cells[9].Style.BackColor = Color.FromArgb(255, 200, 200);
                dgvWallResults.Rows[rowIndex].Cells[9].Style.ForeColor = Color.DarkRed;
                dgvWallResults.Rows[rowIndex].Cells[9].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                dgvWallResults.Rows[rowIndex].Cells[9].Style.BackColor = Color.LightGreen;
                dgvWallResults.Rows[rowIndex].Cells[9].Style.ForeColor = Color.Black;
                dgvWallResults.Rows[rowIndex].Cells[9].Style.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            }
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            if (_lastWallResults.Count == 0 || _wallLogic == null)
            {
                ToastForm.ShowToast("Aktarılacak sonuç bulunmuyor. Önce hesaplama yapın.", _form, 2000);
                return;
            }

            if (!double.TryParse(txtFck.Text, out double fck) || fck <= 0) fck = 30;
            double limit = 0.35;
            double.TryParse(txtLimit.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out limit);

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Sonuçları Excel'e Aktar",
                FileName = $"PerdeEksenelYük_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _wallLogic.ExportExcel(sfd.FileName, fck, limit);
                    ToastForm.ShowToast("Excel başarıyla oluşturuldu.", _form, 2000);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Excel kaydedilirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    // ====== LOGIC ======

    public class WallAxialLogic
    {
        private cSapModel SapModel;

        public Dictionary<(string story, string pier), PierData> pier_data = new Dictionary<(string story, string pier), PierData>();
        public Dictionary<string, PierGeometry> pier_geometry_data = new Dictionary<string, PierGeometry>();

        public List<string> ordered_story_names = new List<string>();
        public Dictionary<string, WallStoryData> story_data = new Dictionary<string, WallStoryData>();

        public List<ResultRow> full_table_data = new List<ResultRow>();

        public List<string> combos = new List<string>();
        public List<string> defined_combos = new List<string>();
        public List<string> patterns = new List<string>();

        public List<RawRow> raw_table_data = new List<RawRow>();

        public WallAxialLogic(cSapModel sapModel)
        {
            SapModel = sapModel;
        }

        public void LoadInitialData()
        {
            try
            {
                int n = 0;
                string[] names = null;
                SapModel.RespCombo.GetNameList(ref n, ref names);
                if (n > 0) defined_combos = names.ToList();
            }
            catch { defined_combos = new List<string>(); }

            try
            {
                int n = 0;
                string[] names = null;
                SapModel.LoadPatterns.GetNameList(ref n, ref names);
                if (n > 0) patterns = names.ToList();
            }
            catch { patterns = new List<string>(); }

            combos = defined_combos.Concat(patterns).ToList();

            int storyCount = 0;
            string[] storyNames = null;
            double[] elev = null;
            double[] heights = null;
            bool[] isMasterStory = null;
            string[] similarToStory = null;
            bool[] spliceAbove = null;
            double[] spliceHeight = null;

            SapModel.Story.GetStories(ref storyCount, ref storyNames, ref elev, ref heights, ref isMasterStory, ref similarToStory, ref spliceAbove, ref spliceHeight);

            if (storyCount > 0)
            {
                ordered_story_names = storyNames.Select(x => x.Trim()).ToList();

                for (int i = 0; i < ordered_story_names.Count; i++)
                {
                    story_data[ordered_story_names[i]] = new WallStoryData
                    {
                        order = i,
                        height = heights[i]
                    };
                }
            }
        }

        public int FetchWallForces(List<string> combosToFetch)
        {
            pier_data.Clear();
            raw_table_data.Clear();

            SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

            foreach (var c in combosToFetch)
                SapModel.Results.Setup.SetComboSelectedForOutput(c);

            int NumberResults = 0;
            string[] Stories = null;
            string[] Piers = null;
            string[] LoadCases = null;
            string[] StepTypes = null;
            double[] P = null;
            double[] V2 = null;
            double[] V3 = null;
            double[] T = null;
            double[] M2 = null;
            double[] M3 = null;

            SapModel.Results.PierForce(
                ref NumberResults,
                ref Stories,
                ref Piers,
                ref LoadCases,
                ref StepTypes,
                ref P,
                ref V2,
                ref V3,
                ref T,
                ref M2,
                ref M3
            );

            Dictionary<(string, string), PierData> temp = new Dictionary<(string, string), PierData>();

            for (int i = 0; i < NumberResults; i++)
            {
                string story = Stories[i].Trim();
                string pier = Piers[i].Trim();
                string load = LoadCases[i];

                double pVal = P[i];

                raw_table_data.Add(new RawRow
                {
                    story = story,
                    pier = pier,
                    load_case = load,
                    p_val = pVal
                });

                var key = (story, pier);

                if (!temp.ContainsKey(key))
                {
                    temp[key] = new PierData
                    {
                        story = story,
                        pier = pier,
                        case_name = load,
                        max_p_abs = Math.Abs(pVal),
                        p_val = pVal
                    };
                }
                else
                {
                    if (Math.Abs(pVal) > temp[key].max_p_abs)
                    {
                        temp[key].max_p_abs = Math.Abs(pVal);
                        temp[key].p_val = pVal;
                        temp[key].case_name = load;
                    }
                }
            }

            pier_data = temp;

            GetAllPierGeometry();

            return pier_data.Count;
        }

        public void GetAllPierGeometry()
        {
            pier_geometry_data.Clear();

            int n = 0;
            string[] pierNames = null;

            SapModel.PierLabel.GetNameList(ref n, ref pierNames);

            if (n > 0)
                GetPierDimensionsFromAPI(pierNames);
        }

        public void GetPierDimensionsFromAPI(IEnumerable<string> pierNames)
        {
            foreach (var pier in pierNames)
            {
                int storyCount = 0;
                string[] stories = null;
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

                SapModel.PierLabel.GetSectionProperties(
                    pier,
                    ref storyCount,
                    ref stories,
                    ref axisAngle,
                    ref numAreaObjs,
                    ref numLineObjs,
                    ref widthBot,
                    ref thickBot,
                    ref widthTop,
                    ref thickTop,
                    ref matProp,
                    ref cgBotX,
                    ref cgBotY,
                    ref cgBotZ,
                    ref cgTopX,
                    ref cgTopY,
                    ref cgTopZ
                );

                if (!pier_geometry_data.ContainsKey(pier))
                    pier_geometry_data[pier] = new PierGeometry();

                if (storyCount > 0 && stories != null)
                {
                    for (int i = 0; i < storyCount; i++)
                    {
                        string s = stories[i].Trim();

                        double lw = widthBot[i] * 100;
                        double bw = thickBot[i] * 100;

                        pier_geometry_data[pier].stories[s] =
                            new PierSection { lw = lw, bw = bw };
                    }
                }
            }
        }

        public List<ResultRow> PerformCalculation(double fck, double limit)
        {
            full_table_data.Clear();

            var sorted =
                pier_data.Values
                .OrderBy(x => x.pier)
                .ThenByDescending(x => story_data.ContainsKey(x.story) ? story_data[x.story].order : 0);

            foreach (var data in sorted)
            {
                string story = data.story;
                string pier = data.pier;

                double bw = 0;
                double lw = 0;

                if (pier_geometry_data.ContainsKey(pier))
                {
                    if (pier_geometry_data[pier].stories.ContainsKey(story))
                    {
                        bw = pier_geometry_data[pier].stories[story].bw;
                        lw = pier_geometry_data[pier].stories[story].lw;
                    }
                }

                double Ac = bw * lw;

                double denom = Ac * fck * 0.1;

                double ratio = 0;
                string status = "HATA";

                if (denom > 0)
                {
                    ratio = data.p_val / denom;
                    status = ratio <= limit ? "OK" : "NOT OK";
                }

                full_table_data.Add(new ResultRow
                {
                    story = story,
                    pier = pier,
                    load_case = data.case_name,
                    fck = fck,
                    b = bw,
                    d = lw,
                    P = data.p_val,
                    Ac = Ac,
                    ratio = ratio,
                    status = status
                });
            }

            return full_table_data;
        }

        public void ExportExcel(string path, double fck, double limit)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Perde Eksenel");

                string[] headers =
                {
                    "Story","Pier","Load Case",
                    "fck","b(cm)","d(cm)",
                    "P(kN)","Ac(cm2)",
                    "Oran","Durum"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[1, i + 1].Value = headers[i];
                    ws.Cells[1, i + 1].Style.Font.Bold = true;
                    ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                int row = 2;

                foreach (var r in full_table_data)
                {
                    ws.Cells[row, 1].Value = r.story;
                    ws.Cells[row, 2].Value = r.pier;
                    ws.Cells[row, 3].Value = r.load_case;
                    ws.Cells[row, 4].Value = r.fck;
                    ws.Cells[row, 5].Value = r.b;
                    ws.Cells[row, 6].Value = r.d;
                    ws.Cells[row, 7].Value = r.P;
                    ws.Cells[row, 8].Value = r.Ac;
                    ws.Cells[row, 9].Value = r.ratio;
                    ws.Cells[row, 10].Value = r.status;

                    row++;
                }

                ws.Cells.AutoFitColumns();

                package.SaveAs(new FileInfo(path));
            }
        }

        public void ExportRawExcel(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Raw");

                ws.Cells[1, 1].Value = "Story";
                ws.Cells[1, 2].Value = "Pier";
                ws.Cells[1, 3].Value = "Load Case";
                ws.Cells[1, 4].Value = "P (kN)";

                int row = 2;

                foreach (var r in raw_table_data)
                {
                    ws.Cells[row, 1].Value = r.story;
                    ws.Cells[row, 2].Value = r.pier;
                    ws.Cells[row, 3].Value = r.load_case;
                    ws.Cells[row, 4].Value = r.p_val;

                    row++;
                }

                ws.Cells.AutoFitColumns();

                package.SaveAs(new FileInfo(path));
            }
        }

        public void SelectPiersOnModel(List<string> uniqueNames)
        {
            try
            {
                SapModel.SelectObj.ClearSelection();
            }
            catch { }

            foreach (var item in uniqueNames)
            {
                var parts = item.Split(new[] { "::" }, StringSplitOptions.None);
                if (parts.Length != 2) continue;

                string story = parts[0];
                string pier = parts[1];

                int n = 0;
                string[] obj = null;

                SapModel.AreaObj.GetNameListOnStory(story, ref n, ref obj);

                foreach (var uid in obj)
                {
                    string assigned = "";
                    SapModel.AreaObj.GetPier(uid, ref assigned);

                    if (assigned.Trim() == pier)
                        SapModel.AreaObj.SetSelected(uid, true);
                }
            }
        }
    }

    public class PierData
    {
        public string story;
        public string pier;
        public string case_name;
        public double max_p_abs;
        public double p_val;
    }

    public class PierGeometry
    {
        public Dictionary<string, PierSection> stories = new Dictionary<string, PierSection>();
    }

    public class PierSection
    {
        public double bw;
        public double lw;
    }

    public class WallStoryData
    {
        public int order;
        public double height;
    }

    public class RawRow
    {
        public string story;
        public string pier;
        public string load_case;
        public double p_val;
    }

    public class ResultRow
    {
        public string story;
        public string pier;
        public string load_case;

        public double fck;
        public double b;
        public double d;

        public double P;
        public double Ac;
        public double ratio;

        public string status;
    }
}
