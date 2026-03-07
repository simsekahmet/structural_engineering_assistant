using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CSiAPIv1;

namespace EtabsTools
{
    /// <summary>
    /// Artırım Hesabı UI modülü - Form1'den ayrı yönetilir
    /// </summary>
    public class ArtirimHesabiUI
    {
        private Form _parentForm;
        private Func<cSapModel> _getSapModel;
        private Func<int, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _bgColor;
        private Func<SpectrumResult> _getSpectrumResult;
        private Func<TextBox> _getSdsTextBox;
        private Func<TextBox> _getITextBox;
        private Func<List<StoryData>> _getStoryDataList;
        private Action _fetchStoryData;

        // UI Components
        private TextBox txtMt;
        private TextBox txtTx, txtVtX;
        private TextBox txtTy, txtVtY;
        private Label lblArtirimStatusX, lblArtirimStatusY;
        private ListBox lstArtirimCombinations;
        private FlowLayoutPanel pnlArtirimSelectedCombos;
        private CheckBox chkArtirimBodrum;
        private TextBox txtArtirimBodrumKat;
        private TextBox txtHN;
        private TextBox txtCt;

        // Modal data cache
        private ToolTip _periodInfoTooltip = new ToolTip();
        private List<(string Mode, double Period, double Ratio)> _cachedModalDataX = new List<(string, double, double)>();
        private List<(string Mode, double Period, double Ratio)> _cachedModalDataY = new List<(string, double, double)>();

        public ArtirimHesabiUI(
            Form parent, 
            Func<cSapModel> getSapModel, 
            Func<int, Panel> createNavPanel, 
            Action<int> goToPage, 
            Color bgColor,
            Func<SpectrumResult> getSpectrumResult,
            Func<TextBox> getSdsTextBox,
            Func<TextBox> getITextBox,
            Func<List<StoryData>> getStoryDataList,
            Action fetchStoryData)
        {
            _parentForm = parent;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNavPanel;
            _goToPage = goToPage;
            _bgColor = bgColor;
            _getSpectrumResult = getSpectrumResult;
            _getSdsTextBox = getSdsTextBox;
            _getITextBox = getITextBox;
            _getStoryDataList = getStoryDataList;
            _fetchStoryData = fetchStoryData;
        }

        public void Initialize(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Başlık
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // İçerik
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Navigasyon

            // --- BAŞLIK ---
            Label lblTitle = Form1.CreateHeaderLabel("Deprem Artırım Katsayısı Hesabı");
            mainLayout.Controls.Add(lblTitle, 0, 0);

            // --- İÇERİK - 2 Sütunlu (Şimdi 4 sütunlu: Boşluk - Prm - Sonuç - Boşluk) ---
            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(0)
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F)); // Sol Boşluk
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F)); // Parametreler (48 -> 38)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F)); // Sonuçlar (52 -> 42)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F)); // Sağ Boşluk

            // =============== SOL PANEL - PARAMETRELER ===============
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                Margin = new Padding(0, 0, 10, 0),
                TitleFont = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            int labelX = 20;
            int textX = 200;
            int textW = 80;
            int btnX = 290;
            int infoX = 385;
            int currentY = 60;
            int gapY = 36;

            // ===== BODRUM AYARI (EN ÜSTTE) =====
            chkArtirimBodrum = new CheckBox { Text = "Bodrum kabulü var mı?", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 8) };
            pnlParams.Controls.Add(chkArtirimBodrum);

            pnlParams.Controls.Add(new Label { Text = "Bodrum Kat:", Location = new Point(labelX + 180, currentY), AutoSize = true, Font = new Font("Segoe UI", 8) });
            txtArtirimBodrumKat = new TextBox { Location = new Point(labelX + 260, currentY - 3), Width = 40, Text = "0" };
            pnlParams.Controls.Add(txtArtirimBodrumKat);
            currentY += gapY;

            // ===== KÜTLE BİLGİSİ =====
            pnlParams.Controls.Add(new Label { Text = "Yapı Toplam Kütlesi (ton):", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtMt = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtMt);
            Button btnGetMt = new Button { Text = "Değeri Getir", Location = new Point(btnX, currentY - 5), Size = new Size(85, 26), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetMt.FlatAppearance.BorderSize = 1;
            btnGetMt.Click += BtnGetMt_Click;
            pnlParams.Controls.Add(btnGetMt);
            
            currentY += gapY + 5;

            // ===== HN ve Ct PARAMETRELERİ =====
            pnlParams.Controls.Add(new Label { Text = "Bina Yüsekliği Hn (m):", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtHN = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtHN);
            
            pnlParams.Controls.Add(new Label { Text = "Ct (0.07):", Location = new Point(labelX + 290, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtCt = new TextBox { Location = new Point(labelX + 350, currentY - 3), Width = 40, Text = "0.07" };
            pnlParams.Controls.Add(txtCt);

            currentY += gapY;

            // ===== KOMBİNASYON SEÇİMİ =====
            pnlParams.Controls.Add(new Label { Text = "Combinations and Cases:", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            currentY += 30;

            lstArtirimCombinations = new ListBox { Location = new Point(labelX, currentY), Size = new Size(150, 110), SelectionMode = SelectionMode.MultiExtended, Font = new Font("Segoe UI", 8) };
            pnlParams.Controls.Add(lstArtirimCombinations);

            Button btnArtirimGetir = new Button { Text = "Getir", Location = new Point(labelX + 155, currentY), Size = new Size(45, 24), BackColor = Color.FromArgb(220, 220, 220), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 7, FontStyle.Bold), Cursor = Cursors.Hand };
            btnArtirimGetir.FlatAppearance.BorderSize = 1;
            btnArtirimGetir.Click += BtnArtirimLoadCombos_Click;
            pnlParams.Controls.Add(btnArtirimGetir);

            Button btnArtirimSec = new Button { Text = "Seç", Location = new Point(labelX + 155, currentY + 28), Size = new Size(45, 24), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 7, FontStyle.Bold), Cursor = Cursors.Hand };
            btnArtirimSec.FlatAppearance.BorderSize = 1;
            btnArtirimSec.Click += BtnArtirimSelectCombos_Click;
            pnlParams.Controls.Add(btnArtirimSec);

            pnlArtirimSelectedCombos = new FlowLayoutPanel { Location = new Point(labelX + 210, currentY), Size = new Size(160, 110), FlowDirection = FlowDirection.LeftToRight, WrapContents = true, AutoScroll = true, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlParams.Controls.Add(pnlArtirimSelectedCombos);
            currentY += 125;

            // ===== AYIRICI ÇİZGİ =====
            pnlParams.Controls.Add(new Panel { Location = new Point(labelX, currentY), Size = new Size(380, 2), BackColor = Color.FromArgb(200, 200, 200) });
            currentY += 12;

            // ===== X YÖNÜ =====
            pnlParams.Controls.Add(new Label { Text = "X Yönü", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(70, 130, 180) });
            currentY += 26;

            // Periyot Tx
            pnlParams.Controls.Add(new Label { Text = "Periyot Tx (s):", Location = new Point(labelX + 15, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtTx = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtTx);
            Button btnGetTx = new Button { Text = "Değeri Getir", Location = new Point(btnX, currentY - 5), Size = new Size(85, 26), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetTx.FlatAppearance.BorderSize = 1;
            btnGetTx.Click += (s, ev) => BtnGetPeriod_Click("X");
            pnlParams.Controls.Add(btnGetTx);
            Label lblInfoTx = new Label { Text = "ℹ", Location = new Point(infoX, currentY - 2), AutoSize = true, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(70, 130, 180), Cursor = Cursors.Hand };
            lblInfoTx.MouseEnter += (s, ev) => ShowPeriodInfoPopup(lblInfoTx, "X");
            lblInfoTx.MouseLeave += (s, ev) => HidePeriodInfoPopup();
            pnlParams.Controls.Add(lblInfoTx);
            currentY += gapY;

            // Modal Vt-X
            pnlParams.Controls.Add(new Label { Text = "Modal Vt-X (kN):", Location = new Point(labelX + 15, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtVtX = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtVtX);
            Button btnGetVtX = new Button { Text = "Değeri Getir", Location = new Point(btnX, currentY - 5), Size = new Size(85, 26), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetVtX.FlatAppearance.BorderSize = 1;
            btnGetVtX.Click += (s, ev) => BtnGetVt_Click("X");
            pnlParams.Controls.Add(btnGetVtX);
            currentY += gapY;

            SmoothButton btnCalculateX = new SmoothButton { Text = "X Yönü Hesapla", BaseColor = Color.FromArgb(70, 130, 180), Size = new Size(130, 30), Location = new Point(labelX + 40, currentY), GrowAmount = 2 };
            btnCalculateX.Click += BtnCalculateArtirimX_Click;
            pnlParams.Controls.Add(btnCalculateX);
            currentY += gapY + 10;

            // ===== AYIRICI ÇİZGİ 2 =====
            pnlParams.Controls.Add(new Panel { Location = new Point(labelX, currentY), Size = new Size(380, 2), BackColor = Color.FromArgb(200, 200, 200) });
            currentY += 12;

            // ===== Y YÖNÜ =====
            pnlParams.Controls.Add(new Label { Text = "Y Yönü", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(46, 139, 87) });
            currentY += 26;

            // Periyot Ty
            pnlParams.Controls.Add(new Label { Text = "Periyot Ty (s):", Location = new Point(labelX + 15, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtTy = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtTy);
            Button btnGetTy = new Button { Text = "Değeri Getir", Location = new Point(btnX, currentY - 5), Size = new Size(85, 26), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetTy.FlatAppearance.BorderSize = 1;
            btnGetTy.Click += (s, ev) => BtnGetPeriod_Click("Y");
            pnlParams.Controls.Add(btnGetTy);
            Label lblInfoTy = new Label { Text = "ℹ", Location = new Point(infoX, currentY - 2), AutoSize = true, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(46, 139, 87), Cursor = Cursors.Hand };
            lblInfoTy.MouseEnter += (s, ev) => ShowPeriodInfoPopup(lblInfoTy, "Y");
            lblInfoTy.MouseLeave += (s, ev) => HidePeriodInfoPopup();
            pnlParams.Controls.Add(lblInfoTy);
            currentY += gapY;

            // Modal Vt-Y
            pnlParams.Controls.Add(new Label { Text = "Modal Vt-Y (kN):", Location = new Point(labelX + 15, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtVtY = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtVtY);
            Button btnGetVtY = new Button { Text = "Değeri Getir", Location = new Point(btnX, currentY - 5), Size = new Size(85, 26), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetVtY.FlatAppearance.BorderSize = 1;
            btnGetVtY.Click += (s, ev) => BtnGetVt_Click("Y");
            pnlParams.Controls.Add(btnGetVtY);
            currentY += gapY;

            SmoothButton btnCalculateY = new SmoothButton { Text = "Y Yönü Hesapla", BaseColor = Color.FromArgb(46, 139, 87), Size = new Size(130, 30), Location = new Point(labelX + 40, currentY), GrowAmount = 2 };
            btnCalculateY.Click += BtnCalculateArtirimY_Click;
            pnlParams.Controls.Add(btnCalculateY);

            tlp.Controls.Add(pnlParams, 1, 0);

            // =============== SAĞ PANEL - SONUÇLAR ===============
            RoundedPanel pnlResults = new RoundedPanel { Title = "Sonuçlar", Dock = DockStyle.Fill, BorderRadius = 25, Margin = new Padding(10, 0, 0, 0), TitleFont = new Font("Segoe UI", 14, FontStyle.Bold) };

            pnlResults.Controls.Add(new Label { Text = "X Yönü", Location = new Point(20, 50), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(70, 130, 180) });
            lblArtirimStatusX = new Label { Text = "Hesaplanmadı", Location = new Point(20, 80), Size = new Size(300, 150), Font = new Font("Segoe UI", 10), ForeColor = Color.Gray };
            pnlResults.Controls.Add(lblArtirimStatusX);

            pnlResults.Controls.Add(new Label { Text = "Y Yönü", Location = new Point(20, 300), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(46, 139, 87) });
            lblArtirimStatusY = new Label { Text = "Hesaplanmadı", Location = new Point(20, 330), Size = new Size(300, 150), Font = new Font("Segoe UI", 10), ForeColor = Color.Gray };
            pnlResults.Controls.Add(lblArtirimStatusY);

            tlp.Controls.Add(pnlResults, 2, 0);
            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            page.Tag = 2;
            page.VisibleChanged += (s, e) => { if (page.Visible && mainLayout.Controls.Count < 3) { mainLayout.Controls.Add(_createNavigationPanel(2), 0, 2); } };
            page.Controls.Add(mainLayout);
        }

        private cSapModel SapModel => _getSapModel();

        // ETABS'tan Yapı Toplam Kütlesini al
        private void BtnGetMt_Click(object sender, EventArgs e)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("Önce ETABS'a bağlanın!", _parentForm, 2000);
                return;
            }

            try
            {
                string tableName = "Mass Summary by Story";
                string groupName = "";
                string[] fieldKeyList = null;
                int numRecords = 0;
                string[] tableData = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;

                int ret = SapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref numRecords, ref tableData, ref tableVersion, ref fieldsKeysIncluded);

                if (ret == 0 && fieldsKeysIncluded != null && fieldsKeysIncluded.Length > 0)
                {
                    int numFields = tableData?.Length ?? 4;
                    int numRows = fieldsKeysIncluded.Length / numFields;

                    int storyIdx = 0;
                    for (int i = 0; i < numFields; i++)
                    {
                        if (tableData != null && i < tableData.Length)
                        {
                            string h = tableData[i].ToUpper();
                            if (h.Contains("STORY")) { storyIdx = i; break; }
                        }
                    }

                    int uxColumnIndex = 1;
                    if (tableData != null)
                    {
                        for (int i = 0; i < tableData.Length; i++)
                        {
                            if (tableData[i].ToUpper() == "UX" || tableData[i].ToUpper().Contains("MASSX")) { uxColumnIndex = i; break; }
                        }
                    }

                    var excludedStories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    excludedStories.Add("Base");

                    if (chkArtirimBodrum.Checked)
                    {
                        _fetchStoryData();
                        int bodrumCount = 0;
                        int.TryParse(txtArtirimBodrumKat.Text, out bodrumCount);

                        var storyDataList = _getStoryDataList();
                        if (bodrumCount > 0 && storyDataList.Count > 0)
                        {
                            var sortedStories = storyDataList.OrderBy(s => s.Elevation).ToList();
                            var validStories = sortedStories.Where(s => !s.Name.Equals("Base", StringComparison.OrdinalIgnoreCase)).ToList();
                            
                            for (int i = 0; i < bodrumCount && i < validStories.Count; i++)
                            {
                                excludedStories.Add(validStories[i].Name);
                            }
                        }
                    }
                    
                    double totalMass = 0;
                    
                    for (int row = 0; row < numRows; row++)
                    {
                        int baseIdx = row * numFields;
                        if (baseIdx + storyIdx >= fieldsKeysIncluded.Length) continue;

                        string story = fieldsKeysIncluded[baseIdx + storyIdx];
                        
                        if (excludedStories.Contains(story)) continue;

                        int dataIndex = baseIdx + uxColumnIndex;
                        if (dataIndex < fieldsKeysIncluded.Length)
                        {
                            string uxValue = fieldsKeysIncluded[dataIndex];
                            if (double.TryParse(uxValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double mass))
                            {
                                totalMass += mass;
                            }
                        }
                    }
                    
                    txtMt.Text = totalMass.ToString("0.00");
                    ToastForm.ShowToast("Yapı toplam kütlesi çekildi.", _parentForm, 2000);
                }
                else
                {
                    ToastForm.ShowToast($"Tablo verisi alınamadı.\n\nret={ret}, fieldsKeysIncluded={fieldsKeysIncluded?.Length ?? 0}", _parentForm, 3000);
                }
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Hata: " + ex.Message, _parentForm, 2000);
            }
        }

        private void BtnGetPeriod_Click(string direction)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("Önce ETABS'a bağlanın!", _parentForm, 2000);
                return;
            }

            try
            {
                SapModel.SelectObj.ClearSelection();
                SapModel.SelectObj.All();

                string tableName = "Modal Participating Mass Ratios";
                string groupName = "";
                string[] fieldKeyList = null;
                int numRecords = 0;
                string[] tableData = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;

                int ret = SapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref numRecords, ref tableData, ref tableVersion, ref fieldsKeysIncluded);

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    ToastForm.ShowToast("Modal Participating Mass Ratios tablosu okunamadı.\nLütfen analizi kilitli olarak çalıştırın.", _parentForm, 3000);
                    return;
                }

                int numFields = tableData?.Length ?? 8;
                int numRows = fieldsKeysIncluded.Length / numFields;

                int caseIdx = 0, modeIdx = 1, periodIdx = 2, uxIdx = 3, uyIdx = 4;
                if (tableData != null)
                {
                    for (int i = 0; i < tableData.Length; i++)
                    {
                        string col = tableData[i].ToUpper();
                        if (col == "CASE" || col == "OUTPUTCASE") caseIdx = i;
                        else if (col == "MODE" || col == "STEPNUM") modeIdx = i;
                        else if (col == "PERIOD") periodIdx = i;
                        else if (col == "UX") uxIdx = i;
                        else if (col == "UY") uyIdx = i;
                    }
                }

                var modalData = new List<(string Mode, double Period, double UX, double UY)>();
                for (int row = 0; row < numRows; row++)
                {
                    string caseValue = fieldsKeysIncluded[row * numFields + caseIdx];
                    if (caseValue != null && caseValue.Equals("Modal-Ust", StringComparison.OrdinalIgnoreCase))
                    {
                        string mode = fieldsKeysIncluded[row * numFields + modeIdx];
                        double.TryParse(fieldsKeysIncluded[row * numFields + periodIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double period);
                        double.TryParse(fieldsKeysIncluded[row * numFields + uxIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double ux);
                        double.TryParse(fieldsKeysIncluded[row * numFields + uyIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double uy);
                        modalData.Add((mode, period, ux, uy));
                    }
                }

                if (modalData.Count == 0)
                {
                    ToastForm.ShowToast("Modal verisi bulunamadı.\nModal analiz yapılmış olmalı.", _parentForm, 3000);
                    return;
                }

                if (direction == "X")
                {
                    var sorted = modalData.OrderByDescending(m => m.UX)
                        .GroupBy(m => m.Mode).Select(g => g.First()).Take(2).ToList();
                    var best = sorted.First();
                    txtTx.Text = best.Period.ToString("0.000");
                    _cachedModalDataX = sorted.Select(m => (m.Mode, m.Period, m.UX)).ToList();
                    ToastForm.ShowToast("X Yönü periyot değeri çekildi.", _parentForm, 2000);
                }
                else
                {
                    var sorted = modalData.OrderByDescending(m => m.UY)
                        .GroupBy(m => m.Mode).Select(g => g.First()).Take(2).ToList();
                    var best = sorted.First();
                    txtTy.Text = best.Period.ToString("0.000");
                    _cachedModalDataY = sorted.Select(m => (m.Mode, m.Period, m.UY)).ToList();
                    ToastForm.ShowToast("Y Yönü periyot değeri çekildi.", _parentForm, 2000);
                }
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Periyot çekme hatası: " + ex.Message, _parentForm, 2000);
            }
            finally
            {
                if (SapModel != null)
                    SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }
        }

        private void BtnArtirimLoadCombos_Click(object sender, EventArgs e)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("Önce ETABS'a bağlanın!", _parentForm, 2000);
                return;
            }

            try
            {
                lstArtirimCombinations.Items.Clear();
                int count = 0;
                string[] names = null;
                SapModel.RespCombo.GetNameList(ref count, ref names);
                if (names != null)
                    foreach (var n in names) lstArtirimCombinations.Items.Add(n);

                int caseCount = 0;
                string[] caseNames = null;
                SapModel.LoadCases.GetNameList(ref caseCount, ref caseNames);
                if (caseNames != null)
                    foreach (var n in caseNames) lstArtirimCombinations.Items.Add(n);
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Kombinasyon yükleme hatası: " + ex.Message, _parentForm, 2000);
            }
        }

        private void BtnArtirimSelectCombos_Click(object sender, EventArgs e)
        {
            foreach (var item in lstArtirimCombinations.SelectedItems)
            {
                string comboName = item.ToString();
                if (!IsArtirimComboSelected(comboName))
                    AddArtirimSelectedComboTag(comboName);
            }
        }

        private bool IsArtirimComboSelected(string name)
        {
            foreach (Control c in pnlArtirimSelectedCombos.Controls)
                if (c.Tag?.ToString() == name) return true;
            return false;
        }

        private void AddArtirimSelectedComboTag(string comboName)
        {
            Panel tag = new Panel { Size = new Size(70, 18), BackColor = Color.FromArgb(230, 240, 250), Margin = new Padding(2), Tag = comboName };
            Label lbl = new Label { Text = comboName.Length > 8 ? comboName.Substring(0, 7) + ".." : comboName, Location = new Point(2, 2), AutoSize = true, Font = new Font("Segoe UI", 6) };
            Label btnRemove = new Label { Text = "×", Location = new Point(56, 0), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Red, Cursor = Cursors.Hand };
            btnRemove.Click += (s, ev) => { pnlArtirimSelectedCombos.Controls.Remove(tag); };
            tag.Controls.Add(lbl);
            tag.Controls.Add(btnRemove);
            pnlArtirimSelectedCombos.Controls.Add(tag);
        }

        private void BtnGetVt_Click(string direction)
        {
            if (SapModel == null)
            {
                ToastForm.ShowToast("Önce ETABS'a bağlanın!", _parentForm, 2000);
                return;
            }

            var selectedCombos = new List<string>();
            foreach (Control c in pnlArtirimSelectedCombos.Controls)
                if (c.Tag != null) selectedCombos.Add(c.Tag.ToString());

            if (selectedCombos.Count == 0)
            {
                ToastForm.ShowToast("Önce kombinasyon seçin!", _parentForm, 2000);
                return;
            }

            string dirFilter = direction == "X" ? "X" : "Y";
            var matchingCombo = selectedCombos.FirstOrDefault(c => c.ToUpper().Contains(dirFilter));
            if (matchingCombo == null)
            {
                ToastForm.ShowToast($"{direction} yönü için kombinasyon bulunamadı.\nKombinayon adında '{dirFilter}' içermeli.", _parentForm, 3000);
                return;
            }

            try
            {
                SapModel.SelectObj.ClearSelection();
                SapModel.SelectObj.All();

                string tableName = "Story Forces";
                string groupName = "";
                string[] fieldKeyList = null;
                int numRecords = 0;
                string[] tableData = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;

                SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                SapModel.Results.Setup.SetCaseSelectedForOutput(matchingCombo);
                SapModel.Results.Setup.SetComboSelectedForOutput(matchingCombo);

                int ret = SapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref numRecords, ref tableData, ref tableVersion, ref fieldsKeysIncluded);

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    ToastForm.ShowToast("Story Forces tablosu okunamadı.\nLütfen analizi tamamlayın.", _parentForm, 3000);
                    return;
                }

                int numFields = tableData?.Length ?? 10;
                int numRows = fieldsKeysIncluded.Length / numFields;

                int storyIdx = 0, caseIdx = 1, locationIdx = 6, vxIdx = 8, vyIdx = 9;
                if (tableData != null)
                {
                    for (int i = 0; i < tableData.Length; i++)
                    {
                        string col = tableData[i].ToUpper();
                        if (col == "STORY" || col == "S") storyIdx = i;
                        else if (col.StartsWith("OUTPUT") || col == "CASE") caseIdx = i;
                        else if (col == "LOCATION") locationIdx = i;
                        else if (col == "VX") vxIdx = i;
                        else if (col == "VY") vyIdx = i;
                    }
                }

                int bodrumKat = 0;
                if (chkArtirimBodrum.Checked)
                    int.TryParse(txtArtirimBodrumKat.Text, out bodrumKat);

                var storyData = new List<(string Story, string Case, string Location, double Vx, double Vy)>();
                for (int row = 0; row < numRows; row++)
                {
                    string caseVal = fieldsKeysIncluded[row * numFields + caseIdx];
                    string location = fieldsKeysIncluded[row * numFields + locationIdx];
                    
                    bool caseMatch = caseVal != null && (caseVal == matchingCombo || caseVal.ToUpper().Contains(matchingCombo.ToUpper()));
                    bool locationMatch = location != null && location.ToUpper() == "BOTTOM";
                    
                    if (caseMatch && locationMatch)
                    {
                        string story = fieldsKeysIncluded[row * numFields + storyIdx];
                        double.TryParse(fieldsKeysIncluded[row * numFields + vxIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double vx);
                        double.TryParse(fieldsKeysIncluded[row * numFields + vyIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double vy);
                        storyData.Add((story, caseVal, location, vx, vy));
                    }
                }

                if (storyData.Count == 0)
                {
                    ToastForm.ShowToast($"Story Forces verisi bulunamadı.\n\nAranan: {matchingCombo}", _parentForm, 3000);
                    return;
                }

                storyData.Reverse();

                int targetRow = chkArtirimBodrum.Checked ? bodrumKat : 0;
                if (targetRow >= storyData.Count) targetRow = storyData.Count - 1;

                var targetData = storyData[targetRow];
                double vtValue = direction == "X" ? Math.Abs(targetData.Vx) : Math.Abs(targetData.Vy);

                if (direction == "X")
                {
                    txtVtX.Text = vtValue.ToString("0.00");
                    ToastForm.ShowToast("X Yönü taban kesme kuvveti çekildi.", _parentForm, 2000);
                }
                else
                {
                    txtVtY.Text = vtValue.ToString("0.00");
                    ToastForm.ShowToast("Y Yönü taban kesme kuvveti çekildi.", _parentForm, 2000);
                }
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Vt çekme hatası: " + ex.Message, _parentForm, 2000);
            }
            finally
            {
                if (SapModel != null)
                    SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }
        }

        private void ShowPeriodInfoPopup(Label infoLabel, string direction)
        {
            var data = direction == "X" ? _cachedModalDataX : _cachedModalDataY;
            if (data.Count == 0)
            {
                _periodInfoTooltip.Show("Önce 'Değeri Getir' butonuna basın", infoLabel, 0, 20, 3000);
                return;
            }

            string colName = direction == "X" ? "UX" : "UY";
            string info = $"En yüksek {colName} değerleri:\n\n";
            info += String.Format("{0,-6} {1,-10} {2,-8}\n", "Mode", "Period", colName);
            info += new string('-', 26) + "\n";
            foreach (var item in data)
            {
                info += String.Format("{0,-6} {1,-10:0.000} {2,-8:0.0000}\n", item.Mode, item.Period, item.Ratio);
            }

            _periodInfoTooltip.Show(info, infoLabel, 0, 20, 5000);
        }

        private void HidePeriodInfoPopup()
        {
            _periodInfoTooltip.Hide(_parentForm);
        }

        private void BtnCalculateArtirimX_Click(object sender, EventArgs e)
        {
            var spectrumResult = _getSpectrumResult();
            if (spectrumResult == null)
            {
                ToastForm.ShowToast("Önce Tasarım Spektrumu sayfasından spektrum hesaplayınız!", _parentForm, 3000);
                return;
            }
            
            try
            {
                double mt = double.Parse(txtMt.Text);
                double tx = double.Parse(txtTx.Text);
                double vtX = double.Parse(txtVtX.Text);
                const double g = 9.81;

                if (mt <= 0 || tx <= 0 || vtX <= 0)
                {
                    ToastForm.ShowToast("Tüm değerler sıfırdan büyük olmalıdır!", _parentForm, 2000);
                    return;
                }

                double Hn = double.Parse(txtHN.Text);
                double Ct = double.Parse(txtCt.Text);
                string periodWarning = "";
                
                if (Hn > 0 && Ct > 0)
                {
                    double tMax = Math.Pow(Hn, 0.75) * Ct * 1.4;
                    if (tx > tMax)
                    {
                        periodWarning = $"\nUYARI: Periyot Tx ({tx:0.000}s) > Tmax ({tMax:0.000}s)\nHesapta {tMax:0.000}s kullanıldı.";
                        tx = tMax;
                    }
                }

                double sae = GetSaeFromSpectrum(spectrumResult, tx);
                
                double sds = double.Parse(_getSdsTextBox().Text);
                double I = double.Parse(_getITextBox().Text);
                
                double Wt = sae * mt;
                double VTmax = 0.04 * sds * g * I * mt;
                double VtHesap = Math.Max(Wt, VTmax);
                double beta = 0.9 * VtHesap / vtX;

                string sonucText = $"Periyot Tx: {tx:0.000} s" + periodWarning + "\n" +
                                   $"SAE: {sae:0.0000} m/s²\n" +
                                   $"Wt: {Wt:0.00} kN\n" +
                                   $"VTmax: {VTmax:0.00} kN\n" +
                                   $"Artırım Katsayısı β: {beta:0.000}";

                lblArtirimStatusX.Text = sonucText;
                lblArtirimStatusX.ForeColor = Color.FromArgb(70, 130, 180);
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Hesaplama hatası: " + ex.Message, _parentForm, 2000);
            }
        }

        private void BtnCalculateArtirimY_Click(object sender, EventArgs e)
        {
            var spectrumResult = _getSpectrumResult();
            if (spectrumResult == null)
            {
                ToastForm.ShowToast("Önce Tasarım Spektrumu sayfasından spektrum hesaplayınız!", _parentForm, 3000);
                return;
            }
            
            try
            {
                double mt = double.Parse(txtMt.Text);
                double ty = double.Parse(txtTy.Text);
                double vtY = double.Parse(txtVtY.Text);
                const double g = 9.81;

                if (mt <= 0 || ty <= 0 || vtY <= 0)
                {
                    ToastForm.ShowToast("Tüm değerler sıfırdan büyük olmalıdır!", _parentForm, 2000);
                    return;
                }

                double Hn = double.Parse(txtHN.Text);
                double Ct = double.Parse(txtCt.Text);
                string periodWarning = "";
                
                if (Hn > 0 && Ct > 0)
                {
                    double tMax = Math.Pow(Hn, 0.75) * Ct * 1.4;
                    if (ty > tMax)
                    {
                        periodWarning = $"\nUYARI: Periyot Ty ({ty:0.000}s) > Tmax ({tMax:0.000}s)\nHesapta {tMax:0.000}s kullanıldı.";
                        ty = tMax;
                    }
                }

                double sae = GetSaeFromSpectrum(spectrumResult, ty);
                
                double sds = double.Parse(_getSdsTextBox().Text);
                double I = double.Parse(_getITextBox().Text);
                
                double Wt = sae * mt;
                double VTmax = 0.04 * sds * g * I * mt;
                double VtHesap = Math.Max(Wt, VTmax);
                double beta = 0.9 * VtHesap / vtY;

                string sonucText = $"Periyot Ty: {ty:0.000} s" + periodWarning + "\n" +
                                   $"SAE: {sae:0.0000} m/s²\n" +
                                   $"Wt: {Wt:0.00} kN\n" +
                                   $"VTmax: {VTmax:0.00} kN\n" +
                                   $"Artırım Katsayısı β: {beta:0.000}";

                lblArtirimStatusY.Text = sonucText;
                lblArtirimStatusY.ForeColor = Color.FromArgb(46, 139, 87);
            }
            catch (Exception ex)
            {
                ToastForm.ShowToast("Hesaplama hatası: " + ex.Message, _parentForm, 2000);
            }
        }
        
        private double GetSaeFromSpectrum(SpectrumResult result, double period)
        {
            var periods = result.Periods;
            var accelerations = result.Accelerations;
            
            for (int i = 0; i < periods.Count; i++)
                if (Math.Abs(periods[i] - period) < 0.0001)
                    return accelerations[i];
            
            for (int i = 0; i < periods.Count - 1; i++)
            {
                if (period >= periods[i] && period <= periods[i + 1])
                {
                    double t1 = periods[i], t2 = periods[i + 1];
                    double a1 = accelerations[i], a2 = accelerations[i + 1];
                    return a1 + (a2 - a1) * (period - t1) / (t2 - t1);
                }
            }
            
            return accelerations[accelerations.Count - 1];
        }
    }
}
