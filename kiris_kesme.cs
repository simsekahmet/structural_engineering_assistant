using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CSiAPIv1;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace EtabsTools
{
    // Veri Sınıfları
    public class KirisKesmeBeamData
    {
        public string Story { get; set; }
        public string Label { get; set; }
        public string Unique { get; set; }
        public string Case { get; set; }
        public double Vd { get; set; }
        public string Section { get; set; }
        public double B_m { get; set; }
        public double H_m { get; set; }
    }

    public class KirisKesmeSectionData
    {
        public double H_m { get; set; }
        public double B_m { get; set; }
    }

    public class KirisKesmeResult
    {
        public string Story { get; set; }
        public string Label { get; set; }
        public string Unique { get; set; }
        public string Case { get; set; }
        public string Section { get; set; }
        public double B { get; set; } // cm
        public double H { get; set; } // cm
        public double D { get; set; } // cm
        public double Vd { get; set; }
        
        public int N { get; set; } // Etriye Kolu
        public int Phi { get; set; } // Çap mm
        public double S { get; set; } // Aralık cm

        public double Vr { get; set; }
        public string Status { get; set; }
    }

    public class KirisKesmeUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<Panel, int, string, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        private TabPage _tabPage;

        // UI Kontrolleri
        private ListBox lstCombosBeam;
        private FlowLayoutPanel pnlBeamSelectedCombos;
        private TextBox txtFck, txtFyk, txtDprime;
        private CheckBox chkUseVc;
        private DataGridView dgvBeamResults;
        private Label lblBeamStatus;

        // Hesaplama Verileri
        private List<string> _beamSelectedCombos = new List<string>();
        private Dictionary<string, KirisKesmeBeamData> _beamData = new Dictionary<string, KirisKesmeBeamData>();
        private Dictionary<string, KirisKesmeSectionData> _beamSectionData = new Dictionary<string, KirisKesmeSectionData>();
        private List<KirisKesmeResult> _lastBeamResults = new List<KirisKesmeResult>();

        public KirisKesmeUI(Form1 form, Func<cSapModel> getSapModel,
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

            Label header = Form1.CreateHeaderLabel("Kiriş Kesme Güvenliği Kontrolü");
            mainLayout.Controls.Add(header, 0, 0);

            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp.Padding = new Padding(20, 10, 20, 10);

            // =============== SOL PANEL (Parametreler) ===============
            Panel pnlLeftScroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                RowCount = 3,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 215F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // --- Kombinasyon Seçimi (Üst Sol) ---
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
                Title = "Kombinasyon Seçimi",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 5, 0)
            };

            lstCombosBeam = new ListBox
            {
                Location = new Point(15, 35),
                Size = new Size(145, 125),
                SelectionMode = SelectionMode.MultiExtended,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.FromArgb(250, 252, 255),
                BorderStyle = BorderStyle.None
            };

            SmoothButton btnLoadCombosBeam = new SmoothButton
            {
                Text = "Getir",
                Size = new Size(55, 30),
                Location = new Point(15, 165),
                BaseColor = Color.FromArgb(225, 230, 240),
                BorderRadius = 12,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI", 10f, FontStyle.Regular)
            };
            btnLoadCombosBeam.Click += BtnLoadCombosBeam_Click;

            SmoothButton btnSelectCombosBeam = new SmoothButton
            {
                Text = "Seç",
                Size = new Size(55, 30),
                Location = new Point(80, 165),
                BaseColor = Color.FromArgb(225, 230, 240),
                BorderRadius = 12,
                EnableCenterAnimation = true,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Regular)
            };
            btnSelectCombosBeam.Click += BtnSelectCombosBeam_Click;

            pnlCombos.Controls.Add(lstCombosBeam);
            pnlCombos.Controls.Add(btnLoadCombosBeam);
            pnlCombos.Controls.Add(btnSelectCombosBeam);
            tlpCombos.Controls.Add(pnlCombos, 0, 0);

            RoundedPanel pnlSelectedWrapper = new RoundedPanel
            {
                Title = "Seçili Kombinasyonlar",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(5, 0, 0, 0)
            };

            pnlBeamSelectedCombos = new FlowLayoutPanel
            {
                Location = new Point(10, 35),
                Size = new Size(130, 160),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White
            };
            pnlSelectedWrapper.Controls.Add(pnlBeamSelectedCombos);
            tlpCombos.Controls.Add(pnlSelectedWrapper, 1, 0);
            tlpLeft.Controls.Add(tlpCombos, 0, 0);

            // --- Hesap Ayarları (Orta Sol) ---
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Ayarları",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 5, 10, 5)
            };

            int startY = 40, gapY = 30, lblX = 15, txtX = 150;

            pnlParams.Controls.Add(new Label { Text = "Beton Dayanımı (fck):", Location = new Point(lblX, startY), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtFck = new TextBox { Location = new Point(txtX, startY - 3), Width = 50, Text = "30", Font = new Font("Segoe UI", 10) };
            pnlParams.Controls.Add(txtFck);
            pnlParams.Controls.Add(new Label { Text = "MPa", Location = new Point(txtX + 55, startY), AutoSize = true, Font = new Font("Segoe UI", 9) });

            pnlParams.Controls.Add(new Label { Text = "Donatı Akma (fyk):", Location = new Point(lblX, startY + gapY), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtFyk = new TextBox { Location = new Point(txtX, startY + gapY - 3), Width = 50, Text = "420", Font = new Font("Segoe UI", 10) };
            pnlParams.Controls.Add(txtFyk);
            pnlParams.Controls.Add(new Label { Text = "MPa", Location = new Point(txtX + 55, startY + gapY), AutoSize = true, Font = new Font("Segoe UI", 9) });

            pnlParams.Controls.Add(new Label { Text = "Paspayı (d'):", Location = new Point(lblX, startY + gapY * 2), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtDprime = new TextBox { Location = new Point(txtX, startY + gapY * 2 - 3), Width = 50, Text = "5", Font = new Font("Segoe UI", 10) };
            pnlParams.Controls.Add(txtDprime);
            pnlParams.Controls.Add(new Label { Text = "cm", Location = new Point(txtX + 55, startY + gapY * 2), AutoSize = true, Font = new Font("Segoe UI", 9) });

            chkUseVc = new CheckBox { Text = "Vc (Beton Kesme Katkısı) Kullanılsın", Location = new Point(lblX, startY + gapY * 3), AutoSize = true, Checked = true, Font = new Font("Segoe UI", 9) };
            pnlParams.Controls.Add(chkUseVc);

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
                Font = new Font("Segoe UI Semibold", 9.5f)
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

            dgvBeamResults = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false, // Tablo genel interaktif
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

            dgvBeamResults.Columns.Add("Story", "Kat");
            dgvBeamResults.Columns.Add("Label", "Kiriş");
            dgvBeamResults.Columns.Add("Section", "Kesit");
            dgvBeamResults.Columns.Add("Vd", "Vd (kN)");
            dgvBeamResults.Columns.Add("B", "b(cm)");
            dgvBeamResults.Columns.Add("H", "h(cm)");
            dgvBeamResults.Columns.Add("D", "d(cm)");
            dgvBeamResults.Columns.Add("N", "Etriye (Kolu)");
            dgvBeamResults.Columns.Add("Phi", "Etriye (Çap mm)");
            dgvBeamResults.Columns.Add("S", "Aralık (s cm)");
            dgvBeamResults.Columns.Add("Vr", "Vr (kN)");
            dgvBeamResults.Columns.Add("Status", "Durum");
            dgvBeamResults.Columns.Add("Unique", "Unique");
            dgvBeamResults.Columns["Unique"].Visible = false;

            // Düzenlenebilir sütunları renklendir
            dgvBeamResults.Columns["N"].DefaultCellStyle.BackColor = Color.FromArgb(250, 255, 230);
            dgvBeamResults.Columns["Phi"].DefaultCellStyle.BackColor = Color.FromArgb(250, 255, 230);
            dgvBeamResults.Columns["S"].DefaultCellStyle.BackColor = Color.FromArgb(250, 255, 230);

            // Sadece N, Phi, S sütunları düzenlenebilir olsun
            foreach (DataGridViewColumn col in dgvBeamResults.Columns)
            {
                if (col.Name != "N" && col.Name != "Phi" && col.Name != "S")
                {
                    col.ReadOnly = true;
                }
            }

            dgvBeamResults.CellValueChanged += DgvBeamResults_CellValueChanged;

            lblBeamStatus = new Label
            {
                Text = "",
                Dock = DockStyle.Bottom,
                Height = 25,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Blue,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel dgvContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 25, 15, 15) };
            dgvContainer.Controls.Add(dgvBeamResults);
            dgvContainer.Controls.Add(lblBeamStatus);
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
        private void BtnLoadCombosBeam_Click(object sender, EventArgs e)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("ETABS modeli bulunamadı.", _form, 2000);
                return;
            }

            int numberNames = 0;
            string[] myName = null;
            int returnCode = SapModel.RespCombo.GetNameList(ref numberNames, ref myName);

            lstCombosBeam.Items.Clear();
            if (returnCode == 0 && myName != null)
            {
                foreach (string name in myName)
                {
                    lstCombosBeam.Items.Add(name);
                }
                ToastForm.ShowToast($"{numberNames} kombinasyon yüklendi.", _form, 1500);
            }
            else
            {
                ToastForm.ShowToast("Kombinasyon okunamadı.", _form, 2000);
            }
        }

        private void BtnSelectCombosBeam_Click(object sender, EventArgs e)
        {
            foreach (var item in lstCombosBeam.SelectedItems)
            {
                string comboName = item.ToString();
                if (!_beamSelectedCombos.Contains(comboName))
                {
                    AddComboBadgeBeam(comboName);
                }
            }
        }

        private void AddComboBadgeBeam(string comboName)
        {
            _beamSelectedCombos.Add(comboName);
            Panel badge = new Panel { AutoSize = true, BackColor = Color.FromArgb(237, 242, 247), Padding = new Padding(2), Margin = new Padding(2) };
            Label lbl = new Label { Text = comboName, AutoSize = true, Font = new Font("Segoe UI", 8f), Location = new Point(5, 5) };
            Label del = new Label { Text = "X", AutoSize = true, Font = new Font("Segoe UI", 8f, FontStyle.Bold), ForeColor = Color.Red, Cursor = Cursors.Hand, Location = new Point(lbl.Right + 2, 5) };
            del.Click += (s, ev) => { _beamSelectedCombos.Remove(comboName); pnlBeamSelectedCombos.Controls.Remove(badge); };
            badge.Controls.Add(lbl); badge.Controls.Add(del);
            badge.Resize += (s, ev) => { del.Left = lbl.Right + 2; badge.Width = del.Right + 5; };
            pnlBeamSelectedCombos.Controls.Add(badge);
        }

        // ----------------------------------------------------
        // ETABS VERİ ÇEKME & HESAPLAMA (BEAM SHEAR LOGIC)
        // ----------------------------------------------------
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("ETABS modeline bağlı değil.", _form, 2000);
                return;
            }

            if (_beamSelectedCombos.Count == 0)
            {
                ToastForm.ShowToast("Lütfen kombinasyon seçiniz.", _form, 2000);
                return;
            }

            if (!double.TryParse(txtFck.Text, out double fck) || !double.TryParse(txtFyk.Text, out double fyk) || !double.TryParse(txtDprime.Text, out double dprime))
            {
                ToastForm.ShowToast("Lütfen fck, fyk ve d' parametrelerini sayısal giriniz.", _form, 2000);
                return;
            }

            lblBeamStatus.Text = "Hesaplanıyor... Seçilen kombinasyonlar taranıyor.";
            lblBeamStatus.ForeColor = Color.Orange;
            Application.DoEvents();

            try
            {
                FetchBeamForces(_beamSelectedCombos);
                
                _lastBeamResults = PerformCalculation(fck, fyk, dprime, chkUseVc.Checked);
                UpdateBeamResultsDGV();

                lblBeamStatus.Text = $"Hesaplandı. Toplam Kiriş Sayısı: {_lastBeamResults.Count}";
                lblBeamStatus.ForeColor = Color.Green;
                ToastForm.ShowToast("Hesaplama tamamlandı.", _form, 2000);
            }
            catch (Exception ex)
            {
                lblBeamStatus.Text = "Hata oluştu.";
                lblBeamStatus.ForeColor = Color.Red;
                ToastForm.ShowToast("Hata: " + ex.Message, _form, 3000);
            }
        }

        private void FetchBeamForces(List<string> combos)
        {
            _beamData.Clear();

            SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            foreach (var c in combos)
            {
                SapModel.Results.Setup.SetComboSelectedForOutput(c, true);
            }

            string tableName = "Element Forces - Beams";
            string[] fieldKeys = null;
            string groupName = "All";
            int tableVersion = 0;
            string[] fieldsKeysRet = null;
            int numberRecords = 0;
            string[] tableData = null;

            int ret = SapModel.DatabaseTables.GetTableForDisplayArray(
                tableName,
                ref fieldKeys,
                groupName,
                ref tableVersion,
                ref fieldsKeysRet,
                ref numberRecords,
                ref tableData
            );

            if (ret != 0 || numberRecords == 0 || fieldsKeysRet == null || tableData == null) return;

            int idxStory = Array.IndexOf(fieldsKeysRet, "Story");
            int idxLabel = Array.IndexOf(fieldsKeysRet, "Beam");
            int idxUnique = Array.IndexOf(fieldsKeysRet, "UniqueName");
            int idxCase = Array.IndexOf(fieldsKeysRet, "OutputCase");
            int idxV2 = Array.IndexOf(fieldsKeysRet, "V2");

            int fieldCount = fieldsKeysRet.Length;

            for (int i = 0; i < numberRecords; i++)
            {
                int baseIndex = i * fieldCount;

                string story = tableData[baseIndex + idxStory];
                string label = tableData[baseIndex + idxLabel];
                string unique = tableData[baseIndex + idxUnique];
                string loadCase = tableData[baseIndex + idxCase];

                double v2 = Math.Abs(Convert.ToDouble(tableData[baseIndex + idxV2]));

                if (!combos.Contains(loadCase))
                    continue;

                string key = story + "_" + label;

                if (!_beamData.ContainsKey(key))
                {
                    _beamData[key] = new KirisKesmeBeamData
                    {
                        Story = story,
                        Label = label,
                        Unique = unique,
                        Case = loadCase,
                        Vd = v2
                    };
                }
                else
                {
                    if (v2 > _beamData[key].Vd)
                    {
                        _beamData[key].Vd = v2;
                        _beamData[key].Case = loadCase;
                    }
                }
            }

            FetchSections();
        }

        private void FetchSections()
        {
            _beamSectionData.Clear();
            foreach (var beam in _beamData.Values)
            {
                string prop = "";
                string auto = "";
                SapModel.FrameObj.GetSection(beam.Unique, ref prop, ref auto);
                beam.Section = prop;

                if (!_beamSectionData.ContainsKey(prop))
                {
                    double t3 = 0, t2 = 0;
                    string fileName = "", matProp = "", notes = "", guid = "";
                    int color = 0;
                    SapModel.PropFrame.GetRectangle(prop, ref fileName, ref matProp, ref t3, ref t2, ref color, ref notes, ref guid);

                    _beamSectionData[prop] = new KirisKesmeSectionData
                    {
                        H_m = t3,
                        B_m = t2
                    };
                }

                beam.B_m = _beamSectionData[prop].B_m;
                beam.H_m = _beamSectionData[prop].H_m;
            }
        }

        private List<KirisKesmeResult> PerformCalculation(double fck, double fyk, double d_prime, bool use_vc)
        {
            List<KirisKesmeResult> results = new List<KirisKesmeResult>();

            double fyd = fyk / 1.15;
            double fctd = 0.35 * Math.Sqrt(fck) / 1.5;
            double dprime_m = d_prime / 100.0;

            foreach (var beam in _beamData.Values)
            {
                double b = beam.B_m; // meters
                double h = beam.H_m; // meters
                double Vd = beam.Vd;

                double d = h - dprime_m;

                double Vrmax = 0.85 * b * h * Math.Sqrt(fck) * 1000;
                double Vc = use_vc ? 0.65 * fctd * b * d * 1000 : 0;
                double Vcr = 0.8 * Vc;

                // Default initial stirrup parameters
                int n = 2;
                int phi = 10;
                double s = 10;

                double Asw_s = n * Math.PI * Math.Pow(phi / 10.0, 2) / 4 / s;
                double Vw = Asw_s * (d * 100) * fyd * 0.1;
                double Vr = Vw + Vcr;

                if (Vr > Vrmax) Vr = Vrmax; // Vr cannot exceed Vrmax

                string status = Vd <= Vr ? "OK" : "NOT OK";

                results.Add(new KirisKesmeResult
                {
                    Story = beam.Story,
                    Label = beam.Label,
                    Unique = beam.Unique,
                    Case = beam.Case,
                    Section = beam.Section,
                    B = b * 100, // to cm
                    H = h * 100, // to cm
                    D = d * 100, // to cm
                    Vd = Vd,
                    N = n,
                    Phi = phi,
                    S = s,
                    Vr = Vr,
                    Status = status
                });
            }

            return results.OrderByDescending(x => x.Vd).ToList();
        }

        private void UpdateBeamResultsDGV()
        {
            dgvBeamResults.CellValueChanged -= DgvBeamResults_CellValueChanged;
            dgvBeamResults.Rows.Clear();
            foreach (var res in _lastBeamResults)
            {
                int rowIndex = dgvBeamResults.Rows.Add(
                    res.Story,
                    res.Label,
                    res.Section,
                    res.Vd.ToString("0.##"),
                    res.B.ToString("0.##"),
                    res.H.ToString("0.##"),
                    res.D.ToString("0.##"),
                    res.N,
                    res.Phi,
                    res.S.ToString("0.##"),
                    res.Vr.ToString("0.##"),
                    res.Status,
                    res.Unique
                );

                UpdateDgvRowColor(rowIndex, res.Status);
            }
            dgvBeamResults.CellValueChanged += DgvBeamResults_CellValueChanged;
        }

        private void DgvBeamResults_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && 
                (dgvBeamResults.Columns[e.ColumnIndex].Name == "N" || 
                 dgvBeamResults.Columns[e.ColumnIndex].Name == "Phi" || 
                 dgvBeamResults.Columns[e.ColumnIndex].Name == "S"))
            {
                var row = dgvBeamResults.Rows[e.RowIndex];
                if (row.Cells["N"].Value == null || row.Cells["Phi"].Value == null || row.Cells["S"].Value == null) return;

                if (int.TryParse(row.Cells["N"].Value.ToString(), out int n) &&
                    int.TryParse(row.Cells["Phi"].Value.ToString(), out int phi) &&
                    double.TryParse(row.Cells["S"].Value.ToString(), out double s))
                {
                    if (s <= 0) s = 10; // Prevent div by zero

                    // Fetch inputs
                    if (!double.TryParse(txtFck.Text, out double fck)) fck = 30;
                    if (!double.TryParse(txtFyk.Text, out double fyk)) fyk = 420;
                    bool useVc = chkUseVc.Checked;

                    double fyd = fyk / 1.15;
                    double fctd = 0.35 * Math.Sqrt(fck) / 1.5;

                    double vd = Convert.ToDouble(row.Cells["Vd"].Value);
                    double b = Convert.ToDouble(row.Cells["B"].Value) / 100.0; // cm to m
                    double h = Convert.ToDouble(row.Cells["H"].Value) / 100.0; // cm to m
                    double d = Convert.ToDouble(row.Cells["D"].Value) / 100.0; // cm to m

                    double Vrmax = 0.85 * b * h * Math.Sqrt(fck) * 1000;
                    double Vc = useVc ? 0.65 * fctd * b * d * 1000 : 0;
                    double Vcr = 0.8 * Vc;

                    double Asw_s = n * Math.PI * Math.Pow(phi / 10.0, 2) / 4 / s;
                    double Vw = Asw_s * (d * 100) * fyd * 0.1;
                    
                    double vr = Vw + Vcr;
                    if (vr > Vrmax) vr = Vrmax; // Max limit

                    string status = vd <= vr ? "OK" : "NOT OK";

                    dgvBeamResults.CellValueChanged -= DgvBeamResults_CellValueChanged;
                    row.Cells["Vr"].Value = vr.ToString("0.##");
                    row.Cells["Status"].Value = status;
                    
                    UpdateDgvRowColor(e.RowIndex, status);

                    dgvBeamResults.CellValueChanged += DgvBeamResults_CellValueChanged;

                    if (e.RowIndex < _lastBeamResults.Count)
                    {
                        var item = _lastBeamResults[e.RowIndex];
                        item.N = n;
                        item.Phi = phi;
                        item.S = s;
                        item.Vr = vr;
                        item.Status = status;
                    }
                }
                else
                {
                     ToastForm.ShowToast("Lütfen sayısal bir değer giriniz.", _form, 2000);
                }
            }
        }

        private void UpdateDgvRowColor(int rowIndex, string status)
        {
            if (status != "OK")
            {
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.White;
                dgvBeamResults.Rows[rowIndex].Cells["Status"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Black;
                dgvBeamResults.Rows[rowIndex].Cells["Status"].Style.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            }
        }

        // ----------------------------------------------------
        // EXCEL RAPORU
        // ----------------------------------------------------
        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            if (_lastBeamResults == null || _lastBeamResults.Count == 0)
            {
                ToastForm.ShowToast("Aktarılacak veri yok. Önce hesaplayınız.", _form, 2000);
                return;
            }

            if (!double.TryParse(txtFck.Text, out double fck)) fck = 30;
            if (!double.TryParse(txtFyk.Text, out double fyk)) fyk = 420;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Dosyası|*.xlsx", Title = "Excel Kaydet", FileName = "Kiriş_Kesme_Raporu.xlsx" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcel(sfd.FileName, fck, fyk, chkUseVc.Checked);
                    ToastForm.ShowToast("Excel dosyası kaydedildi.", _form, 2000);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    ToastForm.ShowToast("Hata: " + ex.Message, _form, 3000);
                }
            }
        }

        public void ExportExcel(string path, double fck, double fyk, bool useVc)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Kiriş Kesme Raporu");

                // TITLE
                ws.Cells[1, 1, 1, 12].Merge = true;
                ws.Cells[1, 1].Value = "KİRİŞ KESME GÜVENLİĞİ KONTROLÜ";
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));

                // Parametreler
                ws.Cells[2, 1].Value = "fck:";
                ws.Cells[2, 2].Value = fck;
                ws.Cells[2, 3].Value = "fyk:";
                ws.Cells[2, 4].Value = fyk;
                ws.Cells[2, 5].Value = "Vc:";
                ws.Cells[2, 6].Value = useVc ? "Var" : "Yok";

                // Başlıklar
                string[] headers =
                {
                    "Story","Beam","Section","Vd (kN)","b(cm)","h(cm)","d(cm)",
                    "Kolu (n)","Çap (mm)","Aralık (s)","Vr (kN)","Durum"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[3, i + 1].Value = headers[i];
                    ws.Cells[3, i + 1].Style.Font.Bold = true;
                    ws.Cells[3, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                }

                int row = 4;

                double fyd = fyk / 1.15;
                double fctd = 0.35 * Math.Sqrt(fck) / 1.5;

                foreach (var item in _lastBeamResults)
                {
                    ws.Cells[row, 1].Value = item.Story;
                    ws.Cells[row, 2].Value = item.Label;
                    ws.Cells[row, 3].Value = item.Section;
                    ws.Cells[row, 4].Value = item.Vd;

                    ws.Cells[row, 5].Value = item.B;
                    ws.Cells[row, 6].Value = item.H;
                    ws.Cells[row, 7].Value = item.D;

                    ws.Cells[row, 8].Value = item.N;
                    ws.Cells[row, 9].Value = item.Phi;
                    ws.Cells[row, 10].Value = item.S;

                    // Formül ile Excel'de Vr Hesabı
                    // Asw/s = (H * PI * (I/10)^2)/4 / J => (H4 * 3.14159 * (I4/10)^2)/4 / J4
                    // Vw = Asw/s * d * fyd * 0.1 => (...) * G4 * fyd * 0.1
                    // Vc = 0.65 * fctd * b_m * d_m * 1000 => 0.65 * fctd * (E4/100) * (G4/100) * 1000
                    
                    double VcComponent = useVc ? (0.65 * fctd * (item.B / 100.0) * (item.D / 100.0) * 1000 * 0.8) : 0;

                    // Vr formülü
                    ws.Cells[row, 11].Formula = $"((H{row}*3.14159265*(I{row}/10)^2)/4/J{row})*G{row}*{fyd.ToString(System.Globalization.CultureInfo.InvariantCulture)}*0.1 + {VcComponent.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
                    
                    // Durum
                    ws.Cells[row, 12].Formula = $"IF(D{row}<=K{row},\"OK\", \"NOT OK\")";

                    row++;
                }

                // Conditional Formatting (Color Scale for Vd)
                var vdRange = ws.Cells[$"D4:D{row - 1}"];
                var condScale = vdRange.ConditionalFormatting.AddThreeColorScale();
                condScale.LowValue.Color = Color.LightGreen;
                condScale.MiddleValue.Color = Color.Yellow;
                condScale.HighValue.Color = Color.Salmon;

                // Conditional Formatting (Status)
                var statusRange = ws.Cells[$"L4:L{row - 1}"];
                var condOk = statusRange.ConditionalFormatting.AddEqual();
                condOk.Formula = "\"OK\"";
                condOk.Style.Font.Color.Color = Color.Green;

                var condNotOk = statusRange.ConditionalFormatting.AddNotEqual();
                condNotOk.Formula = "\"OK\"";
                condNotOk.Style.Font.Color.Color = Color.Red;
                condNotOk.Style.Font.Bold = true;

                // Border
                var modelTable = ws.Cells[3, 1, row - 1, 12];
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells.AutoFitColumns();

                package.SaveAs(new System.IO.FileInfo(path));
            }
        }
    }
}
