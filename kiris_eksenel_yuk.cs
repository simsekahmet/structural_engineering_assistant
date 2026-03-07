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
    public class KirisEksenelBeamData
    {
        public string Story { get; set; }
        public string Label { get; set; }
        public string Unique { get; set; }
        public string Case { get; set; }
        public double P { get; set; }
        public string Section { get; set; }
        public double B { get; set; }
        public double D { get; set; }
    }

    public class KirisEksenelBeamSectionData
    {
        public double H { get; set; }
        public double B { get; set; }
    }

    public class BeamResult
    {
        public string Story { get; set; }
        public string Label { get; set; }
        public string Unique { get; set; }
        public string Case { get; set; }
        public string Section { get; set; }
        public double B { get; set; }
        public double D { get; set; }
        public double Ac { get; set; }
        public double Capacity { get; set; }
        public double P { get; set; }
        public double Ratio { get; set; }
        public string Status { get; set; }
    }

    public class KirisEksenelYukUI
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
        private TextBox txtFck;
        private DataGridView dgvBeamResults;
        private Label lblBeamStatus;

        // Hesaplama Verileri
        private List<string> _beamSelectedCombos = new List<string>();
        private Dictionary<string, KirisEksenelBeamData> _beamData = new Dictionary<string, KirisEksenelBeamData>();
        private Dictionary<string, KirisEksenelBeamSectionData> _beamSectionData = new Dictionary<string, KirisEksenelBeamSectionData>();
        private List<BeamResult> _lastBeamResults = new List<BeamResult>();

        public KirisEksenelYukUI(Form1 form, Func<cSapModel> getSapModel,
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

            Label header = Form1.CreateHeaderLabel("Kiriş Eksenel Yük Kontrolü");
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
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
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
                Font = new Font("Segoe UI", 9f, FontStyle.Regular)
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

            pnlParams.Controls.Add(new Label { Text = "Beton Dayanımı (fck):", Location = new Point(15, 45), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtFck = new TextBox { Location = new Point(155, 42), Width = 60, Text = "30", Font = new Font("Segoe UI", 10) };
            pnlParams.Controls.Add(txtFck);
            pnlParams.Controls.Add(new Label { Text = "MPa", Location = new Point(220, 45), AutoSize = true, Font = new Font("Segoe UI", 10) });

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
            btnCalculate.Click += BtnCalculateBeamAxial_Click;
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
            btnExcel.Click += BtnExcelExportBeamAxial_Click;
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
                ReadOnly = false, // Tablo genel olarak düzenlenebilir, sadece B ve D açık olacak
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

            dgvBeamResults.Columns.Add("Story", "Story");
            dgvBeamResults.Columns.Add("Label", "Beam");
            dgvBeamResults.Columns.Add("Case", "Load Case");
            dgvBeamResults.Columns.Add("Section", "Section");
            dgvBeamResults.Columns.Add("B", "b(cm)");
            dgvBeamResults.Columns.Add("D", "d(cm)");
            dgvBeamResults.Columns.Add("Ac", "Ac");
            dgvBeamResults.Columns.Add("Capacity", "Ac*fck");
            dgvBeamResults.Columns.Add("P", "P (kN)");
            dgvBeamResults.Columns.Add("Ratio", "Ratio");
            dgvBeamResults.Columns.Add("Status", "Durum");
            dgvBeamResults.Columns.Add("Unique", "Unique");
            dgvBeamResults.Columns["Unique"].Visible = false;

            // Düzenlenebilir sütunları renklendir
            dgvBeamResults.Columns["B"].DefaultCellStyle.BackColor = Color.FromArgb(250, 255, 230);
            dgvBeamResults.Columns["D"].DefaultCellStyle.BackColor = Color.FromArgb(250, 255, 230);

            // Sadece B ve D sütunları düzenlenebilir olsun
            foreach (DataGridViewColumn col in dgvBeamResults.Columns)
            {
                if (col.Name != "B" && col.Name != "D")
                {
                    col.ReadOnly = true;
                }
            }

            // CellValueChanged eventi interaktif formüller için eklendi
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
        // ETABS VERİ ÇEKME & HESAPLAMA (BEAM LOGIC)
        // ----------------------------------------------------
        private void BtnCalculateBeamAxial_Click(object sender, EventArgs e)
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

            if (!double.TryParse(txtFck.Text, out double fck) || fck <= 0)
            {
                ToastForm.ShowToast("Geçerli bir fck değeri giriniz.", _form, 2000);
                return;
            }

            lblBeamStatus.Text = "Hesaplanıyor... Seçilen kombinasyonlar taranıyor.";
            lblBeamStatus.ForeColor = Color.Orange;
            Application.DoEvents();

            try
            {
                FetchBeamForces(_beamSelectedCombos);
                
                _lastBeamResults = PerformCalculation(fck);
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
            int idxP = Array.IndexOf(fieldsKeysRet, "P");

            int fieldCount = fieldsKeysRet.Length;

            for (int i = 0; i < numberRecords; i++)
            {
                int baseIndex = i * fieldCount;

                string story = tableData[baseIndex + idxStory];
                string label = tableData[baseIndex + idxLabel];
                string unique = tableData[baseIndex + idxUnique];
                string loadCase = tableData[baseIndex + idxCase];

                double p = Convert.ToDouble(tableData[baseIndex + idxP]);

                if (!combos.Contains(loadCase))
                    continue;

                if (!_beamData.ContainsKey(unique))
                {
                    _beamData[unique] = new KirisEksenelBeamData
                    {
                        Story = story,
                        Label = label,
                        Unique = unique,
                        Case = loadCase,
                        P = Math.Abs(p)
                    };
                }
                else
                {
                    if (Math.Abs(p) > _beamData[unique].P)
                    {
                        _beamData[unique].P = Math.Abs(p);
                        _beamData[unique].Case = loadCase;
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

                    _beamSectionData[prop] = new KirisEksenelBeamSectionData
                    {
                        H = t3,
                        B = t2
                    };
                }

                beam.B = _beamSectionData[prop].B * 100;
                beam.D = _beamSectionData[prop].H * 100;
            }
        }

        private List<BeamResult> PerformCalculation(double fck)
        {
            List<BeamResult> results = new List<BeamResult>();
            double limit = 0.1;

            foreach (var beam in _beamData.Values)
            {
                double Ac = beam.B * beam.D;
                double capacity = Ac * fck / 10.0;
                double ratio = beam.P / capacity;

                string status = ratio <= limit ? "OK" : "KOLON GİBİ DONATILACAK";

                results.Add(new BeamResult
                {
                    Story = beam.Story,
                    Label = beam.Label,
                    Unique = beam.Unique,
                    Case = beam.Case,
                    Section = beam.Section,
                    B = beam.B,
                    D = beam.D,
                    Ac = Ac,
                    Capacity = capacity,
                    P = beam.P,
                    Ratio = ratio,
                    Status = status
                });
            }

            return results.OrderByDescending(x => x.Ratio).ToList();
        }

        private void UpdateBeamResultsDGV()
        {
            dgvBeamResults.CellValueChanged -= DgvBeamResults_CellValueChanged; // Geçici kapat
            dgvBeamResults.Rows.Clear();
            foreach (var res in _lastBeamResults)
            {
                int rowIndex = dgvBeamResults.Rows.Add(
                    res.Story,
                    res.Label,
                    res.Case,
                    res.Section,
                    res.B.ToString("0.##"),
                    res.D.ToString("0.##"),
                    res.Ac.ToString("0.##"),
                    res.Capacity.ToString("0.##"),
                    res.P.ToString("0.##"),
                    res.Ratio.ToString("0.###"),
                    res.Status,
                    res.Unique
                );

                UpdateDgvRowColor(rowIndex, res.Status);
            }
            dgvBeamResults.CellValueChanged += DgvBeamResults_CellValueChanged;
        }

        private void DgvBeamResults_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dgvBeamResults.Columns[e.ColumnIndex].Name == "B" || dgvBeamResults.Columns[e.ColumnIndex].Name == "D"))
            {
                var row = dgvBeamResults.Rows[e.RowIndex];
                if (row.Cells["B"].Value == null || row.Cells["D"].Value == null) return;

                if (double.TryParse(row.Cells["B"].Value.ToString(), out double b) &&
                    double.TryParse(row.Cells["D"].Value.ToString(), out double d))
                {
                    if (!double.TryParse(txtFck.Text, out double fck) || fck <= 0) fck = 30;

                    double p = Convert.ToDouble(row.Cells["P"].Value);
                    double limit = 0.1;

                    // Formüller
                    double ac = b * d;
                    double capacity = (ac * fck) / 10.0;
                    double ratio = capacity != 0 ? p / capacity : 0;
                    string status = ratio <= limit ? "OK" : "KOLON GİBİ DONATILACAK";

                    // DGV Güncellemesi
                    dgvBeamResults.CellValueChanged -= DgvBeamResults_CellValueChanged;
                    row.Cells["Ac"].Value = ac.ToString("0.##");
                    row.Cells["Capacity"].Value = capacity.ToString("0.##");
                    row.Cells["Ratio"].Value = ratio.ToString("0.###");
                    row.Cells["Status"].Value = status;
                    
                    // Renk Güncellemesi
                    UpdateDgvRowColor(e.RowIndex, status);

                    dgvBeamResults.CellValueChanged += DgvBeamResults_CellValueChanged;

                    // Arka plandaki listeyi güncelle
                    string unique = row.Cells["Unique"]?.Value?.ToString();
                    if (string.IsNullOrEmpty(unique))
                    {
                        // Unique name kolonunu sonradan eklemediysek, listeden satır indeksine göre bulalım.
                        // Sonuçlar DGV'ye _lastBeamResults listesi sırasıyla eklendiğinden index aynıdır.
                        if (e.RowIndex < _lastBeamResults.Count)
                        {
                            var item = _lastBeamResults[e.RowIndex];
                            item.B = b;
                            item.D = d;
                            item.Ac = ac;
                            item.Capacity = capacity;
                            item.Ratio = ratio;
                            item.Status = status;
                        }
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
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Black;
                dgvBeamResults.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            }
        }

        // ----------------------------------------------------
        // EXCEL RAPORU
        // ----------------------------------------------------
        private void BtnExcelExportBeamAxial_Click(object sender, EventArgs e)
        {
            if (_lastBeamResults == null || _lastBeamResults.Count == 0)
            {
                ToastForm.ShowToast("Aktarılacak veri yok. Önce hesaplayınız.", _form, 2000);
                return;
            }

            if (!double.TryParse(txtFck.Text, out double fck)) fck = 30;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Dosyası|*.xlsx", Title = "Excel Kaydet", FileName = "Kiriş_Eksenel_Yük_Raporu.xlsx" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcel(sfd.FileName, fck, 0.1);
                    ToastForm.ShowToast("Excel dosyası kaydedildi.", _form, 2000);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    ToastForm.ShowToast("Hata: " + ex.Message, _form, 3000);
                }
            }
        }

        public void ExportExcel(string path, double fck, double limit)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Kiriş Eksenel Raporu");

                // TITLE
                ws.Cells[1, 1, 1, 13].Merge = true;
                ws.Cells[1, 1].Value = "KİRİŞ EKSENEL YÜK KONTROLÜ";
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));

                // Parametreler
                ws.Cells[2, 1].Value = "fck";
                ws.Cells[2, 2].Value = fck;

                ws.Cells[2, 12].Value = "Sınır Oran";
                ws.Cells[2, 13].Value = limit;
                ws.Cells[2, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 13].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);

                // Başlıklar
                string[] headers =
                {
                    "Story","Beam","Unique Name","fck","Load Case",
                    "Section","b(cm)","d(cm)","Ac","Ac*fck","P","ratio","Durum"
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

                foreach (var item in _lastBeamResults)
                {
                    ws.Cells[row, 1].Value = item.Story;
                    ws.Cells[row, 2].Value = item.Label;
                    ws.Cells[row, 3].Value = item.Unique;

                    ws.Cells[row, 4].Formula = "$B$2"; // Dinamik fck
                    
                    ws.Cells[row, 5].Value = item.Case;
                    ws.Cells[row, 6].Value = item.Section;

                    ws.Cells[row, 7].Value = item.B;
                    ws.Cells[row, 8].Value = item.D;

                    ws.Cells[row, 9].Formula = $"G{row}*H{row}"; // Ac = b*d
                    ws.Cells[row, 10].Formula = $"(I{row}*D{row})/10"; // Kapasite = Ac*fck/10

                    ws.Cells[row, 11].Value = item.P;

                    ws.Cells[row, 12].Formula = $"IF(J{row}<>0,K{row}/J{row},0)"; // ratio = P / Kapasite

                    // Durum Formülü: Dinamik Limit (M2) referanslı
                    ws.Cells[row, 13].Formula = $"IF(L{row}<=$M$2,\"OK\",\"KOLON GİBİ DONATILACAK\")";

                    row++;
                }

                // Conditional Formatting (Color Scale for Ratio)
                var ratioRange = ws.Cells[$"L4:L{row - 1}"];
                var condScale = ratioRange.ConditionalFormatting.AddThreeColorScale();
                condScale.LowValue.Color = Color.LightGreen;
                condScale.MiddleValue.Color = Color.Yellow;
                condScale.HighValue.Color = Color.Salmon;

                // Conditional Formatting (Status)
                var statusRange = ws.Cells[$"M4:M{row - 1}"];
                var condOk = statusRange.ConditionalFormatting.AddEqual();
                condOk.Formula = "\"OK\"";
                condOk.Style.Font.Color.Color = Color.Green;

                var condNotOk = statusRange.ConditionalFormatting.AddNotEqual();
                condNotOk.Formula = "\"OK\"";
                condNotOk.Style.Font.Color.Color = Color.Red;
                condNotOk.Style.Font.Bold = true;

                // Border
                var modelTable = ws.Cells[3, 1, row - 1, 13];
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
