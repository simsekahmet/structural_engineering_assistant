using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EtabsTools
{
    public class SpectrumResult
    {
        public List<double> Periods { get; set; }
        public List<double> Accelerations { get; set; }
        public string FilePath { get; set; }
    }

    public class SpectrumManager
    {
        public double SDS { get; set; }
        public double SD1 { get; set; }
        public double R { get; set; }
        public double D { get; set; }
        public double I { get; set; }

        public SpectrumManager(double sds, double sd1, double r, double d, double i)
        {
            SDS = sds; SD1 = sd1; R = r; D = d; I = i;
        }

        public SpectrumResult Calculate(string saveFolder = null, bool saveToFile = true)
        {
            double TA = 0.2 * SD1 / SDS, TB = SD1 / SDS;

            var TList = new List<double> { 0.0, TA / 3, TA / 2, TA };
            for (double t = TA + 0.01; t <= TB; t += 0.01) TList.Add(Math.Round(t, 3));
            TList.Add(TB);
            for (double t = TB + 0.05; t <= 8.0; t += 0.05) TList.Add(Math.Round(t, 3));
            TList = TList.Distinct().OrderBy(t => t).ToList();

            var SaRList = TList.Select(T =>
            {
                double Se = T <= TA ? SDS * (0.4 + 0.6 * T / TA) : T <= TB ? SDS : T <= 6.0 ? SD1 / T : SD1 * 6 / (T * T);
                double Reff = T <= TB ? D + ((R / I) - D) * (T / TB) : R / I;
                return 9.81 * (Se / Reff);
            }).ToList();

            string filePath = null;
            if (saveToFile)
            {
                string fileName = $"R{R}_D{D}_I{I}.txt";
                string folder = saveFolder ?? Path.GetTempPath();
                filePath = Path.Combine(folder, fileName);

                using (var sw = new StreamWriter(filePath))
                    for (int i = 0; i < TList.Count; i++)
                        sw.WriteLine($"{TList[i]:0.000}\t{SaRList[i]:0.0000}");
            }

            return new SpectrumResult { Periods = TList, Accelerations = SaRList, FilePath = filePath };
        }
    }

    /// <summary>
    /// Tasarım Spektrumu UI modülü - Form1'den ayrı yönetilir
    /// </summary>
    public class TasarimSpektrumuUI
    {
        private Form _parentForm;
        private Func<int, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _bgColor;

        // UI Components
        private TextBox txtSDS, txtSD1, txtR, txtD, txtI;
        private Chart chartSpectrum;
        private Label lblSpectrumStatus;
        private ScrollableDataPanel scrollableDataPanel;

        // Result - diğer modüller tarafından erişilebilir
        public SpectrumResult SavedSpectrumResult { get; private set; }

        // Göreli Kat Ötelemesi modülü için SDS/SD1 erişimi
        public TextBox TxtSDS => txtSDS;
        public TextBox TxtSD1 => txtSD1;
        public TextBox TxtR => txtR;
        public TextBox TxtD => txtD;
        public TextBox TxtI => txtI;

        public TasarimSpektrumuUI(Form parent, Func<int, Panel> createNavPanel, Action<int> goToPage, Color bgColor)
        {
            _parentForm = parent;
            _createNavigationPanel = createNavPanel;
            _goToPage = goToPage;
            _bgColor = bgColor;
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

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = Form1.CreateHeaderLabel("Tasarım Spektrumu");
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 3;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); // Parametre paneli
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F)); // Veri paneli (ESKİ BOYUT)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F)); // Grafik paneli
            tlp.Padding = new Padding(20, 10, 20, 10);

            // --- SOL TARAFTAKİ INPUT GRUBU (RoundedPanel) ---
            RoundedPanel pnlInput = new RoundedPanel
            {
                Title = "TBDY 2018 Parametreleri",
                Anchor = AnchorStyles.None,
                Size = new Size(330, 360),
                BorderRadius = 15,
                Margin = new Padding(0, 0, 15, 0)
            };

            int startY = 55;
            int gapY = 40;
            int labelX = 35;
            int textX = 140;

            Font lblFont = new Font("Segoe UI", 10f, FontStyle.Regular);
            Color lblColor = Color.FromArgb(113, 128, 150);

            pnlInput.Controls.Add(CreateLabel("SDS (g):", labelX, startY));
            txtSDS = CreateTextBox(textX, startY-2); pnlInput.Controls.Add(txtSDS);

            pnlInput.Controls.Add(CreateLabel("SD1 (g):", labelX, startY + gapY));
            txtSD1 = CreateTextBox(textX, startY + gapY-2); pnlInput.Controls.Add(txtSD1);

            pnlInput.Controls.Add(CreateLabel("R Kats.:", labelX, startY + gapY * 2));
            txtR = CreateTextBox(textX, startY + gapY * 2-2); pnlInput.Controls.Add(txtR);

            pnlInput.Controls.Add(CreateLabel("D Kats.:", labelX, startY + gapY * 3));
            txtD = CreateTextBox(textX, startY + gapY * 3-2); pnlInput.Controls.Add(txtD);

            pnlInput.Controls.Add(CreateLabel("I Kats.:", labelX, startY + gapY * 4));
            txtI = CreateTextBox(textX, startY + gapY * 4-2); pnlInput.Controls.Add(txtI);

            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "HESAPLA",
                Size = new Size(130, 40),
                Location = new Point(25, 290),
                BaseColor = Color.FromArgb(210, 227, 243),
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Anchor = AnchorStyles.None
            };
            btnCalculate.Click += BtnCalculateSpectrum_Click;
            pnlInput.Controls.Add(btnCalculate);

            SmoothButton btnSave = new SmoothButton
            {
                Text = "KAYDET",
                Size = new Size(130, 40),
                Location = new Point(170, 290),
                BaseColor = Color.FromArgb(235, 240, 245),
                BorderRadius = 15,
                EnableCenterAnimation = true,
                Anchor = AnchorStyles.None
            };
            btnSave.Click += BtnSaveSpectrum_Click;
            pnlInput.Controls.Add(btnSave);

            tlp.Controls.Add(pnlInput, 0, 0);

            // --- ORTA VERİ PANELİ (ScrollableDataPanel) ---
            scrollableDataPanel = new ScrollableDataPanel
            {
                Anchor = AnchorStyles.None,
                Size = new Size(160, 360), // ESKİ BOYUT
                BorderRadius = 15,
                Margin = new Padding(0, 0, 15, 0)
            };
            tlp.Controls.Add(scrollableDataPanel, 1, 0);

            // --- SAĞ TARAFTAKİ GRAFİK PANELİ (RoundedPanel içinde) ---
            Panel pnlRight = new Panel { Dock = DockStyle.Fill };
            RoundedPanel pnlChartContainer = new RoundedPanel
            {
                Title = "",
                Anchor = AnchorStyles.None,
                BorderRadius = 15,
                BackColor = Color.FromArgb(250, 252, 255), // Çok hafif buz mavisi-beyaz karışımı
                Padding = new Padding(15)
            };
            pnlRight.Resize += (s, ev) => {
                int w = (int)(pnlRight.Width * 0.95);
                int h = (int)(pnlRight.Height * 0.92);
                if (w > 0 && h > 0) {
                    pnlChartContainer.Size = new Size(w, h);
                    pnlChartContainer.Location = new Point((pnlRight.Width - w) / 2, (pnlRight.Height - h) / 2);
                }
            };

            chartSpectrum = new Chart();
            chartSpectrum.Dock = DockStyle.Fill;
            chartSpectrum.BackColor = Color.FromArgb(250, 252, 255);

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.FromArgb(250, 252, 255);
            area.AxisX.Title = "Periyot (s)";
            area.AxisY.Title = "SaR (m/s²)";
            area.AxisX.TitleFont = new Font("Segoe UI Semibold", 10.5f);
            area.AxisY.TitleFont = new Font("Segoe UI Semibold", 10.5f);
            area.AxisX.TitleForeColor = Color.FromArgb(113, 128, 150);
            area.AxisY.TitleForeColor = Color.FromArgb(113, 128, 150);
            
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(234, 240, 246);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(234, 240, 246);
            area.AxisX.LineColor = Color.FromArgb(200, 210, 220);
            area.AxisY.LineColor = Color.FromArgb(200, 210, 220);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(113, 128, 150);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(113, 128, 150);
            
            area.AxisX.LabelStyle.Format = "0.0";
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = 6;
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 10;
            chartSpectrum.ChartAreas.Add(area);

            Legend legend = new Legend("Legend1") { Docking = Docking.Top, BackColor = Color.Transparent, Font = new Font("Segoe UI", 9f) };
            chartSpectrum.Legends.Add(legend);

            // Hover ile periyot ve ivme gösterimi
            chartSpectrum.GetToolTipText += (s, ev) => {
                if (ev.HitTestResult.ChartElementType == ChartElementType.DataPoint && chartSpectrum.Series.Count > 0) {
                    var dp = chartSpectrum.Series[0].Points[ev.HitTestResult.PointIndex];
                    ev.Text = $"T = {dp.XValue:0.000} s\nSaR = {dp.YValues[0]:0.0000} m/s²";
                }
            };

            lblSpectrumStatus = new Label
            {
                Text = "",
                AutoSize = true,
                Dock = DockStyle.Bottom,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(163, 174, 208),
                Padding = new Padding(0, 5, 0, 0)
            };

            pnlChartContainer.Controls.Add(chartSpectrum);
            pnlRight.Controls.Add(pnlChartContainer);

            tlp.Controls.Add(pnlRight, 2, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            Panel navContainer = _createNavigationPanel(1); // 1 = Tasarım Spektrumu tab index
            mainLayout.Controls.Add(navContainer, 0, 2);

            page.Controls.Add(mainLayout);
        }

        private void BtnCalculateSpectrum_Click(object sender, EventArgs e)
        {
            try
            {
                double valSDS = double.Parse(txtSDS.Text), valSD1 = double.Parse(txtSD1.Text);
                double valR = double.Parse(txtR.Text), valD = double.Parse(txtD.Text), valI = double.Parse(txtI.Text);

                if (valSDS == 0 || valSD1 == 0 || valR == 0 || valI == 0) { ToastForm.ShowToast("Sıfırdan farklı değer giriniz.", _parentForm, 2000); return; }

                var manager = new SpectrumManager(valSDS, valSD1, valR, valD, valI);
                // Sadece hesapla, kaydetme (saveToFile = false)
                var result = manager.Calculate(null, false);
                
                // Spektrum sonucunu artırım hesabı için kaydet (hafızada)
                SavedSpectrumResult = result;

                chartSpectrum.Series.Clear();
                var series = new Series("Tasarım Spektrumu (SaR)") { ChartType = SeriesChartType.Line, BorderWidth = 3, Color = Color.Crimson };
                for (int i = 0; i < result.Periods.Count; i++) series.Points.AddXY(result.Periods[i], result.Accelerations[i]);
                chartSpectrum.Series.Add(series);

                // --- AXIS UPDATE START ---
                if (result.Accelerations.Any())
                {
                    double maxY = result.Accelerations.Max();
                    double roundedMax = Math.Ceiling(maxY);
                    // Kullanıcı talebi: 4.6 -> 5 olsun. (Math.Ceiling)
                    // Eğer tam sayı ise de (örn 4.0) 4'te kalır, belki offset istenebilir ama "4.6 -> 5" örneği Ceiling'i işaret eder.
                    if (roundedMax < 1) roundedMax = 1;

                    var area = chartSpectrum.ChartAreas[0];
                    area.AxisY.Maximum = roundedMax;
                    area.AxisY.Minimum = 0;
                    area.AxisX.Interval = 1;
                    area.AxisY.Interval = 1;
                }
                // --- AXIS UPDATE END ---

                // Veri panelini güncelle
                scrollableDataPanel.SetData(result.Periods, result.Accelerations);

                lblSpectrumStatus.Text = "Hesaplandı (Kaydedilmedi)";
                lblSpectrumStatus.ForeColor = Color.Orange;
            }
            catch (Exception ex) { ToastForm.ShowToast("Hata: " + ex.Message, _parentForm, 2000); }
        }

        private void BtnSaveSpectrum_Click(object sender, EventArgs e)
        {
            try
            {
                double valSDS = double.Parse(txtSDS.Text), valSD1 = double.Parse(txtSD1.Text);
                double valR = double.Parse(txtR.Text), valD = double.Parse(txtD.Text), valI = double.Parse(txtI.Text);

                if (SavedSpectrumResult == null)
                {
                    ToastForm.ShowToast("Önce hesaplama yapınız!", _parentForm, 2000);
                    return;
                }

                // Kayıt klasörü: Uygulamanın çalıştığı klasör
                string saveFolder = Application.StartupPath;

                var manager = new SpectrumManager(valSDS, valSD1, valR, valD, valI);
                // Hesapla ve KAYDET (saveToFile = true)
                var result = manager.Calculate(saveFolder, true);

                SavedSpectrumResult = result; // Sonucu güncelle
                
                lblSpectrumStatus.Text = $"Dosya kaydedildi: {result.FilePath}";
                lblSpectrumStatus.ForeColor = Color.Green;
                
                // ESKİ: MessageBox.Show($"Spektrum hesaplandı ve kaydedildi:\n{result.FilePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // YENİ: Toast Bildirim
                ToastForm.ShowToast("Dosyanız başarıyla oluşturuldu.", _parentForm, 2000);
            }
            catch (Exception ex) { ToastForm.ShowToast("Hata: " + ex.Message, _parentForm, 2000); }
        }

        // Sıfırlama Metodu (Reset)
        public void Reset()
        {
            txtSDS.Text = "0";
            txtSD1.Text = "0";
            txtR.Text = "0";
            txtD.Text = "0";
            txtI.Text = "0";

            chartSpectrum.Series.Clear();
            scrollableDataPanel.SetData(new List<double>(), new List<double>());
            
            lblSpectrumStatus.Text = "";
            SavedSpectrumResult = null;
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Regular), ForeColor = Color.FromArgb(113, 128, 150) };
        }

        private TextBox CreateTextBox(int x, int y)
        {
            return new TextBox { Location = new Point(x, y), Width = 110, Text = "0", Font = new Font("Segoe UI", 9.5f, FontStyle.Regular) };
        }
    }
}
