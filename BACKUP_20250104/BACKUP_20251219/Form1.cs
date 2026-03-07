using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Chart Referansı
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Text;
using CSiAPIv1; // ETABS Referansı

namespace EtabsTools
{
    // ---------------------------------------------------------
    // SMOOTH ANIMASYONLU BUTON (Responsive)
    // ---------------------------------------------------------
    public class SmoothButton : Button
    {
        public int BorderRadius { get; set; } = 25;
        public Color BaseColor { get; set; } = Color.MediumSlateBlue;
        public int GrowAmount { get; set; } = 8; // Hover büyüme miktarı

        private Timer _animTimer;
        private Size _targetSize;
        private Size _originalSize;
        private Point _originalLocation;
        private Point _centerPoint;
        private bool _isHovered = false;
        private int _step = 2;

        public SmoothButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(220, 65);
            this.BackColor = BaseColor;
            this.ForeColor = Color.FromArgb(50, 50, 50);
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 10f, FontStyle.Bold);

            _animTimer = new Timer();
            _animTimer.Interval = 10;
            _animTimer.Tick += AnimTimer_Tick;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _originalSize = this.Size;
            _originalLocation = this.Location;
            // Animasyon merkezi
            _centerPoint = new Point(_originalLocation.X + _originalSize.Width / 2,
                                     _originalLocation.Y + _originalSize.Height / 2);
            this.BackColor = BaseColor;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            _animTimer.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _animTimer.Start();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            int currentW = this.Width;
            int currentH = this.Height;

            int targetW = _isHovered ? _originalSize.Width + GrowAmount : _originalSize.Width;
            int targetH = _isHovered ? _originalSize.Height + GrowAmount : _originalSize.Height;

            if (Math.Abs(currentW - targetW) <= _step) this.Width = targetW;
            else this.Width += (currentW < targetW) ? _step : -_step;

            if (Math.Abs(currentH - targetH) <= _step) this.Height = targetH;
            else this.Height += (currentH < targetH) ? _step : -_step;

            if (this.Width == targetW && this.Height == targetH) _animTimer.Stop();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = GetRoundPath(Rect, BorderRadius))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(Color.FromArgb(100, 255, 255, 255), 2.0f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }

        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.CloseFigure();
            return GraphPath;
        }
    }

    // ROUNDED PANEL (Oval Köşeli Panel)
    // ---------------------------------------------------------
    public class RoundedPanel : Panel
    {
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.LightGray;
        public string Title { get; set; } = "";
        public Font TitleFont { get; set; } = null; // Özel başlık fontu

        public RoundedPanel()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(15, 35, 15, 15);
            this.DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Width > 0 && this.Height > 0)
            {
                RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
                using (GraphicsPath path = GetRoundPath(rect, BorderRadius))
                {
                    this.Region = new Region(path);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            RectangleF rect = new RectangleF(1, 1, this.Width - 3, this.Height - 3);
            using (GraphicsPath path = GetRoundPath(rect, BorderRadius))
            using (Pen pen = new Pen(BorderColor, 1.5f))
            {
                e.Graphics.DrawPath(pen, path);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                Font usedFont = TitleFont ?? new Font("Segoe UI", 10, FontStyle.Bold);
                using (Brush brush = new SolidBrush(Color.FromArgb(80, 80, 80)))
                {
                    e.Graphics.DrawString(Title, usedFont, brush, 15, 10);
                }
                if (TitleFont == null) usedFont.Dispose();
            }
        }

        private GraphicsPath GetRoundPath(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // ---------------------------------------------------------
    // SCROLLABLE DATA PANEL (Roll Up/Down Veri Paneli)
    // ---------------------------------------------------------
    public class ScrollableDataPanel : RoundedPanel
    {
        private Panel headerPanel;
        private Panel dataContainer;
        private VScrollBar scrollBar;
        private List<Tuple<double, double>> dataList = new List<Tuple<double, double>>();
        private int rowHeight = 22;
        private int visibleRows = 10;

        public ScrollableDataPanel()
        {
            this.Title = "Spektrum Verileri";
            this.Size = new Size(160, 350);
            this.Padding = new Padding(10, 35, 10, 10);

            // Header Panel
            headerPanel = new Panel { Height = 25, Dock = DockStyle.Top };
            Label lblPeriod = new Label
            {
                Text = "Periyot (s.)",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(5, 5),
                AutoSize = true
            };
            Label lblSaR = new Label
            {
                Text = "SaR",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(80, 5),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblPeriod);
            headerPanel.Controls.Add(lblSaR);

            // Data Container
            dataContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            dataContainer.MouseWheel += OnMouseWheel;

            // ScrollBar
            scrollBar = new VScrollBar { Dock = DockStyle.Right, Width = 15 };
            scrollBar.Scroll += (s, e) => RedrawData();

            this.Controls.Add(dataContainer);
            this.Controls.Add(scrollBar);
            this.Controls.Add(headerPanel);

            // Mouse wheel desteği için focus alabilmeli
            this.MouseEnter += (s, e) => dataContainer.Focus();
            this.MouseWheel += OnMouseWheel;
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta > 0 ? -3 : 3;
            int newValue = scrollBar.Value + delta;
            scrollBar.Value = Math.Max(0, Math.Min(scrollBar.Maximum - scrollBar.LargeChange + 1, newValue));
            RedrawData();
        }

        public void SetData(List<double> periods, List<double> accelerations)
        {
            dataList.Clear();
            for (int i = 0; i < periods.Count; i++)
                dataList.Add(new Tuple<double, double>(periods[i], accelerations[i]));

            scrollBar.Maximum = Math.Max(0, dataList.Count - visibleRows + 10);
            scrollBar.Value = 0;
            RedrawData();
        }

        private void RedrawData()
        {
            dataContainer.Controls.Clear();
            int startIndex = scrollBar.Value;
            int y = 5;

            for (int i = startIndex; i < Math.Min(startIndex + visibleRows, dataList.Count); i++)
            {
                Label lblT = new Label
                {
                    Text = dataList[i].Item1.ToString("0.000"),
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Location = new Point(5, y),
                    AutoSize = true
                };
                Label lblS = new Label
                {
                    Text = dataList[i].Item2.ToString("0.000"),
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Location = new Point(80, y),
                    AutoSize = true
                };
                dataContainer.Controls.Add(lblT);
                dataContainer.Controls.Add(lblS);
                y += rowHeight;
            }
        }
    }

    // ---------------------------------------------------------
    // ANA FORM
    // ---------------------------------------------------------
    public partial class Form1 : Form
    {
        private cSapModel _sapModel;
        private cHelper _myHelper;
        private cOAPI _myETABSObject;

        private Panel pnlHeader;
        private TabControl mainTabControl;

        private Label lblModelName;
        private Label lblLockStatus;
        private Label lblConnectionStatus;

        // Class Level Değişkenler - Tasarım Spektrumu
        private TextBox txtSDS, txtSD1, txtR, txtD, txtI;
        private Chart chartSpectrum;
        private Label lblSpectrumStatus;
        private ScrollableDataPanel scrollableDataPanel;

        // Class Level Değişkenler - Göreli Kat Ötelemesi
        private TextBox txtSDS_DD2, txtSDS_DD3, txtSD1_DD2, txtSD1_DD3, txtK, txtTp;
        private CheckBox chkEsnekDerz, chkBodrum;
        private NumericUpDown numBodrumKat;
        private ListBox lstCombinations;
        private FlowLayoutPanel pnlSelectedCombos;
        private DataGridView dgvResults;
        private Label lblGoreliStatus;

        // Class Level Değişkenler - Artırım Hesabı
        private TextBox txtMt;
        private TextBox txtTx, txtVtX; // X Yönü: Periyot, Vt
        private TextBox txtTy, txtVtY; // Y Yönü: Periyot, Vt
        private Label lblArtirimStatusX, lblArtirimStatusY;
        private ListBox lstArtirimCombinations;
        private FlowLayoutPanel pnlArtirimSelectedCombos;
        private CheckBox chkArtirimBodrum;
        private TextBox txtArtirimBodrumKat;
        private TextBox txtHN; // Bina Yüksekliği
        private TextBox txtCt; // Periyot katsayısı

        // Class Level Değişkenler - Kolon Eksenel Yük Kontrolü
        private TextBox txtFck;
        private TextBox txtLimit;                       // Nd/(Ac*fck) limit değeri
        private DataGridView dgvKolonResults;        // Sonuç tablosu
        private Label lblKolonStatus;
        private Label lblKolonFrameStatus;           // Frame Assignment durumu
        private CheckBox chkKolonBodrum;              // Bodrum var mı?
        private NumericUpDown numKolonBodrumKat;      // Bodrum kat sayısı
        private ListBox lstKolonCombinations;        // Kombinasyon listesi
        private FlowLayoutPanel pnlKolonSelectedCombos; // Seçili kombinasyonlar
        private List<ColumnForceData> _kolonColumnForces = new List<ColumnForceData>();         // Kolon kuvvetleri verisi
        private List<FrameAssignmentData> _kolonFrameAssignments = new List<FrameAssignmentData>(); // Frame assignment verisi
        private List<string> _kolonSelectedCombos = new List<string>();  // Seçili kombinasyonlar
        private ToolTip _kolonInfoTooltip = new ToolTip(); // Kolon sayfası için Info tooltip

        private Color colorBackground = Color.FromArgb(240, 244, 248);
        private Color colorHeader = Color.White;

        // İkinci Mertebe Etkileri
        private TabPage tabIkinciMertebe;
        private TextBox txtCh, txtR_Ikinci, txtD_Ikinci;
        private CheckBox chkBodrumIkinci;
        private NumericUpDown numBodrumKatIkinci;
        private ListBox lstCombosIkinci;
        private Label lblIkinciMertebeStatus;
        private FlowLayoutPanel pnlIkinciSelectedCombos;
        private DataGridView dgvIkinciMertebeResults;
        private List<string> _ikinciSelectedCombos = new List<string>();

        // Cache Variables for Second Order Effects
        private List<ForceData> _cachedForces = new List<ForceData>();
        private List<DriftData> _cachedDrifts = new List<DriftData>();

        // UI Labels for Second Order Status
        private Label lblStatusStory;
        private Label lblStatusMass;
        private Label lblStatusForce;
        private Label lblStatusDrift;

        // Data Lists
        private List<StoryData> _storyDataList = new List<StoryData>();
        private List<MassData> _massDataList = new List<MassData>();

        // Spektrum sonucu - Artırım hesabı için                                          
        private SpectrumResult _savedSpectrumResult = null;
        
        public Form1()
        {
            InitializeCustomUI();                    
            this.ResizeRedraw = true; // Yeniden boyutlandırmada çizimi tazele
        }

        private void InitializeCustomUI()
        {
            this.Text = "ETABS Mühendislik Asistanı";
            this.Size = new Size(1100, 850);
            this.MinimumSize = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorBackground;
            this.Font = new Font("Segoe UI", 9f);

            // 1. ÜST PANEL
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = colorHeader,
                Padding = new Padding(20)
            };
            pnlHeader.Paint += (s, e) => { e.Graphics.DrawLine(new Pen(Color.LightGray), 0, 79, pnlHeader.Width, 79); };

            // Bağlan Tuşu
            var btnConnect = new SmoothButton
            {
                Text = "ETABS'a Bağlan",
                BaseColor = Color.FromArgb(255, 179, 186),
                Location = new Point(20, 20),
                Size = new Size(150, 40),
                BorderRadius = 20
            };
            btnConnect.Click += BtnConnect_Click;

            // Etiketler
            lblConnectionStatus = new Label { Text = "Durum: Bekleniyor...", Location = new Point(190, 30), AutoSize = true, ForeColor = Color.Gray };
            lblModelName = new Label { Text = "Model: -", Location = new Point(350, 20), AutoSize = true, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            lblLockStatus = new Label { Text = "Kilit: -", Location = new Point(350, 45), AutoSize = true };

            // TabControl Tanımlama
            mainTabControl = new TabControl();
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Appearance = TabAppearance.FlatButtons;
            mainTabControl.ItemSize = new Size(0, 1);
            mainTabControl.SizeMode = TabSizeMode.Fixed;

            pnlHeader.Controls.Add(btnConnect);
            pnlHeader.Controls.Add(lblConnectionStatus);
            pnlHeader.Controls.Add(lblModelName);
            pnlHeader.Controls.Add(lblLockStatus);

            // Dashboard - İLK SAYFA (index 0)
            TabPage pageHome = new TabPage("Dashboard");
            pageHome.BackColor = colorBackground;
            InitializeDashboardResponsive(pageHome);
            mainTabControl.TabPages.Add(pageHome);

            // Tasarım Spektrumu
            TabPage pageSpectrum = new TabPage("Tasarım Spektrumu");
            pageSpectrum.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageSpectrum);
            InitializeSpectrumPageResponsive(pageSpectrum);

            // Artırım Hesabı
            TabPage pageArtirim = new TabPage("Artırım Hesabı");
            pageArtirim.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageArtirim);
            InitializeArtirimHesabiPage(pageArtirim);

            // Göreli Kat Ötelemesi
            TabPage pageGoreliKat = new TabPage("Göreli Kat Ötelemesi Tahkiki");
            pageGoreliKat.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageGoreliKat);
            InitializeGoreliKatOtelemesiPage(pageGoreliKat);

            // İkinci Mertebe Etkileri
            tabIkinciMertebe = new TabPage("İkinci Mertebe Etkileri Tahkiki");
            tabIkinciMertebe.BackColor = colorBackground;
            InitializeIkinciMertebePage(tabIkinciMertebe);
            mainTabControl.TabPages.Add(tabIkinciMertebe);

            // Kolon Eksenel Yük Tahkiki
            TabPage pageKolonEksenel = new TabPage("Kolon Eksenel Yük Tahkiki");
            pageKolonEksenel.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKolonEksenel);
            InitializeKolonEksenelYukPage(pageKolonEksenel);

            // Perde Kesme Kontrolü (placeholder)
            AddContentPage("Perde Kesme Kontrolü");

            this.Controls.Add(mainTabControl);
            this.Controls.Add(pnlHeader);
        }

        // ---------------------------------------------------------
        // RESPONSIVE DASHBOARD (DÜZELTİLMİŞ)
        // ---------------------------------------------------------
        private void InitializeDashboardResponsive(TabPage page)
        {
            // Ana Düzen: TableLayoutPanel
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 1;
            tlp.RowCount = 5;
            // Satır Yükseklikleri (Oransal) - ETABS Ayırıcı kaldırıldı
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F)); // Başlık
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F)); // 1. Satır: Spektrum + Artırım
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F)); // 2. Satır: Göreli + İkinci Mertebe
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F)); // 3. Satır: Kolon + Perde
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); // Alt Boşluk

            // Başlık
            Label lblWelcome = new Label
            {
                Text = "ETABS Mühendislik Asistanı",
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Segoe UI Light", 28, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Fill,
                AutoSize = false
            };
            tlp.Controls.Add(lblWelcome, 0, 0);

            // --- 1. SATIR: Tasarım Spektrumu + Artırım Hesabı ---
            TableLayoutPanel tlpRow1 = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 5 };
            tlpRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRow1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpRow1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRow1.Controls.Add(CreateDashButton("Tasarım Spektrumu", 1, Color.FromArgb(255, 159, 168)), 1, 0);
            tlpRow1.Controls.Add(CreateDashButton("Artırım Hesabı", 2, Color.FromArgb(255, 220, 180)), 3, 0);

            // --- 2. SATIR: Göreli Kat Ötelemesi + İkinci Mertebe ---
            TableLayoutPanel tlpRow2 = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 5 };
            tlpRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRow2.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpRow2.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRow2.Controls.Add(CreateDashButton("Göreli Kat Ötelemesi Tahkiki", 3, Color.FromArgb(159, 219, 255)), 1, 0);
            tlpRow2.Controls.Add(CreateDashButton("İkinci Mertebe Etkileri Tahkiki", 4, Color.FromArgb(255, 236, 159)), 3, 0);

            // --- 3. SATIR: Kolon + Perde ---
            TableLayoutPanel tlpRow3 = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 5 };
            tlpRow3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRow3.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRow3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpRow3.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRow3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRow3.Controls.Add(CreateDashButton("Kolon Eksenel Yük Tahkiki", 5, Color.FromArgb(255, 203, 159)), 1, 0);
            tlpRow3.Controls.Add(CreateDashButton("Perde Kesme Kontrolü", 6, Color.FromArgb(219, 190, 255)), 3, 0);

            tlp.Controls.Add(tlpRow1, 0, 1);
            tlp.Controls.Add(tlpRow2, 0, 2);
            tlp.Controls.Add(tlpRow3, 0, 3);

            page.Controls.Add(tlp);
        }

        private SmoothButton CreateDashButton(string text, int tag, Color color)
        {
            SmoothButton btn = new SmoothButton
            {
                Text = text,
                BaseColor = color,
                Size = new Size(220, 65),
                Tag = tag,
                BorderRadius = 25,
                Anchor = AnchorStyles.None // Hücre içinde ortala
            };
            btn.Click += (s, e) => { GoToPage((int)((Button)s).Tag); };
            return btn;
        }

        // RESPONSIVE SPECTRUM PAGE
        // ---------------------------------------------------------
        private void InitializeSpectrumPageResponsive(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Başlık
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // İçerik
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Navigasyon

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = new Label
            {
                Text = "Tasarım Spektrumu",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 3;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); // Parametre paneli
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F)); // Veri paneli
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F)); // Grafik paneli
            tlp.Padding = new Padding(20, 10, 20, 10);

            // --- SOL TARAFTAKİ INPUT GRUBU (RoundedPanel) ---
            RoundedPanel pnlInput = new RoundedPanel
            {
                Title = "TBDY 2018 Parametreleri",
                Anchor = AnchorStyles.None,
                Size = new Size(320, 350),
                BorderRadius = 25
            };

            int startY = 50;
            int gapY = 45;
            int labelX = 50;
            int textX = 100;

            pnlInput.Controls.Add(CreateLabel("SDS:", labelX, startY));
            txtSDS = CreateTextBox(textX, startY); pnlInput.Controls.Add(txtSDS);

            pnlInput.Controls.Add(CreateLabel("SD1:", labelX, startY + gapY));
            txtSD1 = CreateTextBox(textX, startY + gapY); pnlInput.Controls.Add(txtSD1);

            pnlInput.Controls.Add(CreateLabel("R:", labelX, startY + gapY * 2));
            txtR = CreateTextBox(textX, startY + gapY * 2); pnlInput.Controls.Add(txtR);

            pnlInput.Controls.Add(CreateLabel("D:", labelX, startY + gapY * 3));
            txtD = CreateTextBox(textX, startY + gapY * 3); pnlInput.Controls.Add(txtD);

            pnlInput.Controls.Add(CreateLabel("I:", labelX, startY + gapY * 4));
            txtI = CreateTextBox(textX, startY + gapY * 4); pnlInput.Controls.Add(txtI);

            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "Hesapla ve Kaydet",
                Size = new Size(220, 50),
                Location = new Point(50, 280),
                BaseColor = Color.SeaGreen,
                BorderRadius = 20
            };
            btnCalculate.Click += BtnCalculateSpectrum_Click;
            pnlInput.Controls.Add(btnCalculate);

            tlp.Controls.Add(pnlInput, 0, 0);

            // --- ORTA VERİ PANELİ (ScrollableDataPanel) ---
            scrollableDataPanel = new ScrollableDataPanel
            {
                Anchor = AnchorStyles.None,
                Size = new Size(160, 350),
                BorderRadius = 25
            };
            tlp.Controls.Add(scrollableDataPanel, 1, 0);

            // --- SAĞ TARAFTAKİ GRAFİK PANELİ (RoundedPanel içinde) ---
            Panel pnlRight = new Panel { Dock = DockStyle.Fill };
            RoundedPanel pnlChartContainer = new RoundedPanel
            {
                Title = "",
                Anchor = AnchorStyles.None,
                BorderRadius = 25,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(10)
            };
            pnlRight.Resize += (s, ev) => {
                int w = (int)(pnlRight.Width * 0.95);
                int h = (int)(pnlRight.Height * 0.9);
                if (w > 0 && h > 0) {
                    pnlChartContainer.Size = new Size(w, h);
                    pnlChartContainer.Location = new Point((pnlRight.Width - w) / 2, (pnlRight.Height - h) / 2);
                }
            };

            chartSpectrum = new Chart();
            chartSpectrum.Dock = DockStyle.Fill;
            chartSpectrum.BackColor = Color.WhiteSmoke;

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Title = "Periyot (s)";
            area.AxisY.Title = "SaR";
            area.AxisX.TitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            area.AxisY.TitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.LabelStyle.Format = "0.0";
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = 6;
            chartSpectrum.ChartAreas.Add(area);

            Legend legend = new Legend("Legend1") { Docking = Docking.Top };
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
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Blue,
                Padding = new Padding(0, 5, 0, 0)
            };

            pnlChartContainer.Controls.Add(chartSpectrum);
            pnlRight.Controls.Add(pnlChartContainer);

            tlp.Controls.Add(pnlRight, 2, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            Panel navContainer = CreateNavigationPanel(1); // 1 = Tasarım Spektrumu tab index
            mainLayout.Controls.Add(navContainer, 0, 2);

            page.Controls.Add(mainLayout);
        }

        // ---------------------------------------------------------
        // GÖRELİ KAT ÖTELEMESİ SAYFASI
        // ---------------------------------------------------------
        private void InitializeGoreliKatOtelemesiPage(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Başlık
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // İçerik
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Navigasyon

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = new Label
            {
                Text = "Göreli Kat Ötelemesi Tahkiki",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
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
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 185F)); // Kombinasyonlar + Seçili (yan yana) - artırıldı
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
                TitleFont = new Font("Segoe UI", 14, FontStyle.Bold) // 2x büyük başlık
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
                        Panel navPanel = CreateNavigationPanel(3);
                        mainLayout.Controls.Add(navPanel, 0, 2);
                    }

                    // Tasarım Spektrumu değerlerini aktar (eğer girilmişse)
                    if (txtSDS != null && !string.IsNullOrEmpty(txtSDS.Text) && txtSDS.Text != "0")
                        txtSDS_DD2.Text = txtSDS.Text;
                    if (txtSD1 != null && !string.IsNullOrEmpty(txtSD1.Text) && txtSD1.Text != "0")
                        txtSD1_DD2.Text = txtSD1.Text;
                }
            };

            page.Controls.Add(mainLayout);
        }

        // ---------------------------------------------------------
        // ARTIRIM HESABI SAYFASI
        // ---------------------------------------------------------
        private void InitializeArtirimHesabiPage(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Başlık
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // İçerik
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Navigasyon

            // --- BAŞLIK ---
            Label lblTitle = new Label
            {
                Text = "Deprem Artırım Katsayısı Hesabı",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50)
            };
            mainLayout.Controls.Add(lblTitle, 0, 0);

            // --- İÇERİK - 2 Sütunlu ---
            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(20)
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));

            // =============== SOL PANEL - PARAMETRELER ===============
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                Margin = new Padding(0, 0, 10, 0)
            };

            int labelX = 20;
            int textX = 200;
            int textW = 80;
            int btnX = 290;
            int infoX = 385;
            int currentY = 50;
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
            // HN
            pnlParams.Controls.Add(new Label { Text = "Bina Yüsekliği Hn (m):", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtHN = new TextBox { Location = new Point(textX, currentY - 3), Width = textW, Text = "0" };
            pnlParams.Controls.Add(txtHN);
            
            // Ct
            pnlParams.Controls.Add(new Label { Text = "Ct (0.07):", Location = new Point(labelX + 290, currentY), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtCt = new TextBox { Location = new Point(labelX + 350, currentY - 3), Width = 40, Text = "0.07" };
            pnlParams.Controls.Add(txtCt);

            currentY += gapY;

            // ===== KOMBİNASYON SEÇİMİ =====
            pnlParams.Controls.Add(new Label { Text = "Kombinasyon Seçimi:", Location = new Point(labelX, currentY), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            currentY += 20;

            lstArtirimCombinations = new ListBox { Location = new Point(labelX, currentY), Size = new Size(150, 60), SelectionMode = SelectionMode.MultiExtended, Font = new Font("Segoe UI", 7) };
            pnlParams.Controls.Add(lstArtirimCombinations);

            Button btnArtirimGetir = new Button { Text = "Getir", Location = new Point(labelX + 155, currentY), Size = new Size(45, 24), BackColor = Color.FromArgb(220, 220, 220), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 7, FontStyle.Bold), Cursor = Cursors.Hand };
            btnArtirimGetir.FlatAppearance.BorderSize = 1;
            btnArtirimGetir.Click += BtnArtirimLoadCombos_Click;
            pnlParams.Controls.Add(btnArtirimGetir);

            Button btnArtirimSec = new Button { Text = "Seç", Location = new Point(labelX + 155, currentY + 28), Size = new Size(45, 24), BackColor = Color.FromArgb(159, 219, 255), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 7, FontStyle.Bold), Cursor = Cursors.Hand };
            btnArtirimSec.FlatAppearance.BorderSize = 1;
            btnArtirimSec.Click += BtnArtirimSelectCombos_Click;
            pnlParams.Controls.Add(btnArtirimSec);

            pnlArtirimSelectedCombos = new FlowLayoutPanel { Location = new Point(labelX + 210, currentY), Size = new Size(160, 60), FlowDirection = FlowDirection.LeftToRight, WrapContents = true, AutoScroll = true, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlParams.Controls.Add(pnlArtirimSelectedCombos);
            currentY += 65;

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
            // Info icon for Tx
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
            // Info icon for Ty
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

            tlp.Controls.Add(pnlParams, 0, 0);

            // =============== SAĞ PANEL - SONUÇLAR ===============
            RoundedPanel pnlResults = new RoundedPanel { Title = "Sonuçlar", Dock = DockStyle.Fill, BorderRadius = 25, Margin = new Padding(10, 0, 0, 0), TitleFont = new Font("Segoe UI", 14, FontStyle.Bold) };

            pnlResults.Controls.Add(new Label { Text = "X Yönü", Location = new Point(20, 50), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(70, 130, 180) });
            lblArtirimStatusX = new Label { Text = "Hesaplanmadı", Location = new Point(20, 80), Size = new Size(300, 150), Font = new Font("Segoe UI", 10), ForeColor = Color.Gray };
            pnlResults.Controls.Add(lblArtirimStatusX);

            pnlResults.Controls.Add(new Label { Text = "Y Yönü", Location = new Point(20, 300), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(46, 139, 87) });
            lblArtirimStatusY = new Label { Text = "Hesaplanmadı", Location = new Point(20, 330), Size = new Size(300, 150), Font = new Font("Segoe UI", 10), ForeColor = Color.Gray };
            pnlResults.Controls.Add(lblArtirimStatusY);

            tlp.Controls.Add(pnlResults, 1, 0);
            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            page.Tag = 2;
            page.VisibleChanged += (s, e) => { if (page.Visible && mainLayout.Controls.Count < 3) { mainLayout.Controls.Add(CreateNavigationPanel(2), 0, 2); } };
            page.Controls.Add(mainLayout);
        }

        // ETABS'tan Yapı Toplam Kütlesini al
        private void BtnGetMt_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string tableName = "Mass Summary by Story";

                // Tablo verilerini al
                string groupName = "";
                string[] fieldKeyList = null;
                int numRecords = 0;
                string[] tableData = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;

                int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref numRecords, ref tableData, ref tableVersion, ref fieldsKeysIncluded);

                // Gerçek veriler fieldsKeysIncluded içinde!
                if (ret == 0 && fieldsKeysIncluded != null && fieldsKeysIncluded.Length > 0)
                {
                    // Sütun sayısı
                    int numFields = tableData?.Length ?? 4;
                    int numRows = fieldsKeysIncluded.Length / numFields;

                    // Story sütununu bul
                    int storyIdx = 0;
                    for (int i = 0; i < numFields; i++)
                    {
                        if (tableData != null && i < tableData.Length)
                        {
                            string h = tableData[i].ToUpper();
                            if (h.Contains("STORY")) { storyIdx = i; break; }
                        }
                    }

                    // UX sütunu index 1 (Varsayılan) - Kontrol et
                    int uxColumnIndex = 1;
                    if (tableData != null)
                    {
                        for (int i = 0; i < tableData.Length; i++)
                        {
                            if (tableData[i].ToUpper() == "UX" || tableData[i].ToUpper().Contains("MASSX")) { uxColumnIndex = i; break; }
                        }
                    }

                    // --- BODRUM FİLTRESİ ---
                    var excludedStories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    excludedStories.Add("Base"); // Base her zaman hariç

                    if (chkArtirimBodrum.Checked)
                    {
                        // Kat verilerini çek ve sırala
                        FetchStoryData();
                        int bodrumCount = 0;
                        int.TryParse(txtArtirimBodrumKat.Text, out bodrumCount);

                        if (bodrumCount > 0 && _storyDataList.Count > 0)
                        {
                            // Elevation'a göre sırala (Alttan üste)
                            var sortedStories = _storyDataList.OrderBy(s => s.Elevation).ToList();
                            
                            // Base hariç en alttaki N katı bul
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
                        
                        // FİLTRE: Eğer kat excluded listesindeyse (Base veya Bodrum) TOPLAMA
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
                }
                else
                {
                    MessageBox.Show($"Tablo verisi alınamadı.\n\nret={ret}, fieldsKeysIncluded={fieldsKeysIncluded?.Length ?? 0}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Periyot popup için class-level değişken
        private ToolTip _periodInfoTooltip = new ToolTip();
        private List<(string Mode, double Period, double Ratio)> _cachedModalDataX = new List<(string, double, double)>();
        private List<(string Mode, double Period, double Ratio)> _cachedModalDataY = new List<(string, double, double)>();

        // ETABS'tan Periyot değerini al (Modal Participating Mass Ratios tablosundan)
        private void BtnGetPeriod_Click(string direction)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // SEÇİMİ TEMİZLE VE TÜMÜNÜ SEÇ (Veri gelmeme sorununu çözer)
                _sapModel.SelectObj.ClearSelection();
                _sapModel.SelectObj.All();

                string tableName = "Modal Participating Mass Ratios";
                string groupName = "";
                string[] fieldKeyList = null;
                int numRecords = 0;
                string[] tableData = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;

                int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref numRecords, ref tableData, ref tableVersion, ref fieldsKeysIncluded);

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    MessageBox.Show("Modal Participating Mass Ratios tablosu okunamadı.\nLütfen analizi kilitli olarak çalıştırın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Sütun sayısını belirle
                int numFields = tableData?.Length ?? 8;
                int numRows = fieldsKeysIncluded.Length / numFields;

                // Sütun indekslerini bul (Case, Mode, Period, UX, UY)
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

                // Modal-Ust satırlarını filtrele ve verileri topla
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
                    MessageBox.Show("Modal verisi bulunamadı.\nModal analiz yapılmış olmalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (direction == "X")
                {
                    var sorted = modalData.OrderByDescending(m => m.UX)
                        .GroupBy(m => m.Mode).Select(g => g.First()).Take(2).ToList();
                    var best = sorted.First();
                    txtTx.Text = best.Period.ToString("0.000");
                    _cachedModalDataX = sorted.Select(m => (m.Mode, m.Period, m.UX)).ToList();
                }
                else
                {
                    var sorted = modalData.OrderByDescending(m => m.UY)
                        .GroupBy(m => m.Mode).Select(g => g.First()).Take(2).ToList();
                    var best = sorted.First();
                    txtTy.Text = best.Period.ToString("0.000");
                    _cachedModalDataY = sorted.Select(m => (m.Mode, m.Period, m.UY)).ToList();
                }
            }
    catch (Exception ex)
    {
        MessageBox.Show("Periyot çekme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        // Temizlik: Seçimleri kaldır
        if (_sapModel != null)
             _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
    }
}

        // Artırım Hesabı için kombinasyon yükle
        private void BtnArtirimLoadCombos_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lstArtirimCombinations.Items.Clear();
                int count = 0;
                string[] names = null;
                _sapModel.RespCombo.GetNameList(ref count, ref names);
                if (names != null)
                    foreach (var n in names) lstArtirimCombinations.Items.Add(n);

                // LoadCase'leri de ekle
                int caseCount = 0;
                string[] caseNames = null;
                _sapModel.LoadCases.GetNameList(ref caseCount, ref caseNames);
                if (caseNames != null)
                    foreach (var n in caseNames) lstArtirimCombinations.Items.Add(n);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kombinasyon yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Artırım Hesabı için kombinasyon seç
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

        // Vt değerini çek (Story Forces tablosundan)
        private void BtnGetVt_Click(string direction)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili kombinasyonları kontrol et
            var selectedCombos = new List<string>();
            foreach (Control c in pnlArtirimSelectedCombos.Controls)
                if (c.Tag != null) selectedCombos.Add(c.Tag.ToString());

            if (selectedCombos.Count == 0)
            {
                MessageBox.Show("Önce kombinasyon seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // X veya Y yönüne göre filtrele
            string dirFilter = direction == "X" ? "X" : "Y";
            var matchingCombo = selectedCombos.FirstOrDefault(c => c.ToUpper().Contains(dirFilter));
            if (matchingCombo == null)
            {
                MessageBox.Show($"{direction} yönü için kombinasyon bulunamadı.\nKombinayon adında '{dirFilter}' içermeli.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // SEÇİMİ TEMİZLE VE TÜMÜNÜ SEÇ
                _sapModel.SelectObj.ClearSelection();
                _sapModel.SelectObj.All();

                string tableName = "Story Forces";
                string groupName = "";
                string[] fieldKeyList = null;
                int numRecords = 0;
                string[] tableData = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;

                // Tabloyu çekmeden önce SEÇİMİ AYARLA
                _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                _sapModel.Results.Setup.SetCaseSelectedForOutput(matchingCombo);
                _sapModel.Results.Setup.SetComboSelectedForOutput(matchingCombo);

                int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref numRecords, ref tableData, ref tableVersion, ref fieldsKeysIncluded);

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    MessageBox.Show("Story Forces tablosu okunamadı.\nLütfen analizi tamamlayın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int numFields = tableData?.Length ?? 10;
                int numRows = fieldsKeysIncluded.Length / numFields;


                // Sütun indekslerini bul - ETABS sütun adları kısaltılmış olabilir
                // Story=0, OutputCas=1, CaseType=2, StepType=3, StepNumb=4, StepLabel=5, Location=6, P=7, VX=8, VY=9
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

                // Bodrum ayarını kontrol et
                int bodrumKat = 0;
                if (chkArtirimBodrum.Checked)
                    int.TryParse(txtArtirimBodrumKat.Text, out bodrumKat);

                // Story listesini al - seçilen kombinasyona göre filtrele
                var storyData = new List<(string Story, string Case, string Location, double Vx, double Vy)>();
                for (int row = 0; row < numRows; row++)
                {
                    string caseVal = fieldsKeysIncluded[row * numFields + caseIdx];
                    string location = fieldsKeysIncluded[row * numFields + locationIdx];
                    
                    // Filtrele: seçilen kombinasyon (tam eşleşme veya içerme) ve Location = Bottom
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
                    MessageBox.Show($"Story Forces verisi bulunamadı.\n\nAranan: {matchingCombo}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Listeyi ters çevir - en alt kat (base'e en yakın) en başta olsun
                storyData.Reverse();

                // Bodrum katına göre okuma satırını belirle
                // Bodrum yoksa: Base'in 1 üstü (index 0)
                // 2 bodrum varsa: Base'in 3 üstü (index 2)
                int targetRow = chkArtirimBodrum.Checked ? bodrumKat : 0;
                if (targetRow >= storyData.Count) targetRow = storyData.Count - 1;

                var targetData = storyData[targetRow];
                double vtValue = direction == "X" ? Math.Abs(targetData.Vx) : Math.Abs(targetData.Vy);

                if (direction == "X")
                    txtVtX.Text = vtValue.ToString("0.00");
                else
                    txtVtY.Text = vtValue.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vt çekme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Temizlik: Seçimleri kaldır
                if (_sapModel != null)
                     _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }
        }

        // Periyot bilgi popup'ını göster
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

        // Periyot bilgi popup'ını kapat
        private void HidePeriodInfoPopup()
        {
            _periodInfoTooltip.Hide(this);
        }

        // Artırım Katsayısı Hesapla - X Yönü
        private void BtnCalculateArtirimX_Click(object sender, EventArgs e)
        {
            // Spektrum kontrolü
            if (_savedSpectrumResult == null)
            {
                MessageBox.Show("Önce Tasarım Spektrumu sayfasından spektrum hesaplayınız!", 
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                double mt = double.Parse(txtMt.Text);   // ton
                double tx = double.Parse(txtTx.Text);   // periyot (s)
                double vtX = double.Parse(txtVtX.Text); // Modal Vt (kN)
                const double g = 9.81;

                if (mt <= 0 || tx <= 0 || vtX <= 0)
                {
                    MessageBox.Show("Tüm değerler sıfırdan büyük olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // PERİYOT KONTROLÜ (YENİ)
                double Hn = double.Parse(txtHN.Text);
                double Ct = double.Parse(txtCt.Text);
                string periodWarning = "";
                
                if (Hn > 0 && Ct > 0)
                {
                     double tMax = Math.Pow(Hn, 0.75) * Ct * 1.4;
                     if (tx > tMax)
                     {
                         periodWarning = $"\nUYARI: Periyot Tx ({tx:0.000}s) > Tmax ({tMax:0.000}s)\nHesapta {tMax:0.000}s kullanıldı.";
                         tx = tMax; // Periyodu sınırla
                     }
                }

                // SAE değerini spektrumdan interpolasyonla al
                double sae = GetSaeFromSpectrum(tx);
                
                // SDS ve I değerlerini spektrum parametrelerinden al
                double sds = double.Parse(txtSDS.Text);
                double I = double.Parse(txtI.Text);
                
                // Wt hesabı: SAE × Mt (kN cinsinden)
                double Wt = sae * mt;
                
                // VTmax hesabı: 0.04 × SDS × g × I × Mt
                double VTmax = 0.04 * sds * g * I * mt;
                
                // En büyük değeri al
                double VtHesap = Math.Max(Wt, VTmax);
                
                // Artırım katsayısı β
                double beta = 0.9 * VtHesap / vtX;

                // Sonuç göster
                // GÜNCELLEME: Hesaplanan (sınırlandırılmış) tx değerini göster
                string sonucText = $"Periyot Tx: {tx:0.000} s" + periodWarning + "\n" +
                                   $"SAE: {sae:0.0000} m/s²\n" +
                                   $"Wt: {Wt:0.00} kN\n" +
                                   $"VTmax: {VTmax:0.00} kN\n" +
                                   $"Artırım Katsayısı β: {beta:0.000}";

                lblArtirimStatusX.Text = sonucText;
                lblArtirimStatusX.ForeColor = (beta >= 0.9 && beta <= 1.4) ? Color.FromArgb(46, 139, 87) : Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesaplama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Artırım Katsayısı Hesapla - Y Yönü
        private void BtnCalculateArtirimY_Click(object sender, EventArgs e)
        {
            // Spektrum kontrolü
            if (_savedSpectrumResult == null)
            {
                MessageBox.Show("Önce Tasarım Spektrumu sayfasından spektrum hesaplayınız!", 
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                double mt = double.Parse(txtMt.Text);   // ton
                double ty = double.Parse(txtTy.Text);   // periyot (s)
                double vtY = double.Parse(txtVtY.Text); // Modal Vt (kN)
                const double g = 9.81;

                if (mt <= 0 || ty <= 0 || vtY <= 0)
                {
                    MessageBox.Show("Tüm değerler sıfırdan büyük olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // PERİYOT KONTROLÜ (YENİ)
                double Hn = double.Parse(txtHN.Text);
                double Ct = double.Parse(txtCt.Text);
                string periodWarning = "";
                
                if (Hn > 0 && Ct > 0)
                {
                     double tMax = Math.Pow(Hn, 0.75) * Ct * 1.4;
                     if (ty > tMax)
                     {
                         periodWarning = $"\nUYARI: Periyot Ty ({ty:0.000}s) > Tmax ({tMax:0.000}s)\nHesapta {tMax:0.000}s kullanıldı.";
                         ty = tMax; // Periyodu sınırla
                     }
                }

                // SAE değerini spektrumdan interpolasyonla al
                double sae = GetSaeFromSpectrum(ty);
                
                // SDS ve I değerlerini spektrum parametrelerinden al
                double sds = double.Parse(txtSDS.Text);
                double I = double.Parse(txtI.Text);
                
                // Wt hesabı: SAE × Mt (kN cinsinden)
                double Wt = sae * mt;
                
                // VTmax hesabı: 0.04 × SDS × g × I × Mt
                double VTmax = 0.04 * sds * g * I * mt;
                
                // En büyük değeri al
                double VtHesap = Math.Max(Wt, VTmax);
                
                // Artırım katsayısı β
                double beta = 0.9*VtHesap / vtY;

                // Sonuç göster
                // GÜNCELLEME: Hesaplanan (sınırlandırılmış) ty değerini göster
                string sonucText = $"Periyot Ty: {ty:0.000} s" + periodWarning + "\n" +
                                   $"SAE: {sae:0.0000} m/s²\n" +
                                   $"Wt: {Wt:0.00} kN\n" +
                                   $"VTmax: {VTmax:0.00} kN\n" +
                                   $"Artırım Katsayısı β: {beta:0.000}";

                lblArtirimStatusY.Text = sonucText;
                lblArtirimStatusY.ForeColor = (beta >= 0.9 && beta <= 1.4) ? Color.FromArgb(46, 139, 87) : Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesaplama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Periyot değerine göre SAE interpolasyonu
        private double GetSaeFromSpectrum(double period)
        {
            if (_savedSpectrumResult == null) return -1;
            
            var periods = _savedSpectrumResult.Periods;
            var accelerations = _savedSpectrumResult.Accelerations;
            
            // Periyot tam olarak varsa doğrudan döndür
            for (int i = 0; i < periods.Count; i++)
                if (Math.Abs(periods[i] - period) < 0.0001)
                    return accelerations[i];
            
            // Lineer interpolasyon
            for (int i = 0; i < periods.Count - 1; i++)
            {
                if (period >= periods[i] && period <= periods[i + 1])
                {
                    double t1 = periods[i], t2 = periods[i + 1];
                    double a1 = accelerations[i], a2 = accelerations[i + 1];
                    return a1 + (a2 - a1) * (period - t1) / (t2 - t1);
                }
            }
            
            // Aralık dışındaysa son değer
            return accelerations[accelerations.Count - 1];
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
            if (_sapModel == null)
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
                _sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                if (comboNames != null)
                    foreach (var name in comboNames)
                        lstCombinations.Items.Add(name);

                // Load Cases
                int numCases = 0;
                string[] caseNames = null;
                _sapModel.LoadCases.GetNameList(ref numCases, ref caseNames);
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
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kilit kontrolü
            bool isLocked = _sapModel.GetModelIsLocked();
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
                    var xData = GetStoryDriftFromETABS(xCombosUST);
                    foreach (var item in xData)
                    {
                        if (item.Direction.ToUpper() == "X")
                        {
                            // Bodrum katları için ALT kombinasyonları kullan, üst katlar için UST
                            int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(item.Story);
                            if (!chkBodrum.Checked || storyNum == null || storyNum > bodrumKatSayisi)
                                allDriftData.Add(item);
                        }
                    }
                }

                // X yönü hesapları - ALT kombinasyonlar (bodrum katları için)
                if (chkBodrum.Checked && xCombosALT.Count > 0)
                {
                    var xData = GetStoryDriftFromETABS(xCombosALT);
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
                    var yData = GetStoryDriftFromETABS(yCombosUST);
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
                    var yData = GetStoryDriftFromETABS(yCombosALT);
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
        private List<StoryDriftData> GetStoryDriftFromETABS(List<string> combinations)
        {
            var results = new List<StoryDriftData>();

            try
            {
                // Önce tüm case seçimlerini temizle
                _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                // Seçili kombinasyonları aktifleştir
                foreach (var combo in combinations)
                {
                    _sapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    _sapModel.Results.Setup.SetComboSelectedForOutput(combo);
                }

                // Story Drift sonuçlarını al
                int numResults = 0;
                string[] story = null, loadCase = null, stepType = null, direction = null, label = null;
                double[] stepNum = null, drift = null, x = null, y = null, z = null;

                _sapModel.Results.StoryDrifts(ref numResults, ref story, ref loadCase, ref stepType,
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
                if (_sapModel != null) _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }

            return results;
        }

        // Sonuçları CSV dosyasına kaydet
        private void SaveGoreliKatResults(GoreliKatResult result)
        {
            try
            {
                string fileName = $"GoreliKat_Sonuc_{DateTime.Now:yyyyMMdd}.csv";
                string filePath = System.IO.Path.Combine(Application.StartupPath, fileName);

                // CSV formatında kaydet - UTF-8 BOM (semboller için)
                using (var sw = new System.IO.StreamWriter(filePath, false, new System.Text.UTF8Encoding(true)))
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

        private RichTextBox rtbFailedColumns;

        private void InitializeKolonEksenelYukPage(TabPage page)
        {
            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = new Label
            {
                Text = "Kolon Eksenel Yük Tahkiki",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ - 2 Sütun ---
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));  // Sol panel
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));  // Sağ panel - Sonuçlar
            tlp.Padding = new Padding(15, 5, 15, 5);

            // =============== SOL PANEL ===============
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 320F));  // Hesap Parametreleri (Combinations + Bodrum içinde)
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));   // Frame/Element (küçültüldü - sadece buton ve status)
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

            // fck girişi
            pnlParams.Controls.Add(new Label { Text = "fck (N/mm²):", Location = new Point(15, 38), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtFck = new TextBox { Location = new Point(100, 35), Width = 50, Text = "30" };
            pnlParams.Controls.Add(txtFck);

            // Limit değeri girişi
            pnlParams.Controls.Add(new Label { Text = "Limit:", Location = new Point(160, 38), AutoSize = true, Font = new Font("Segoe UI", 9) });
            txtLimit = new TextBox { Location = new Point(200, 35), Width = 50, Text = "0.40" };
            pnlParams.Controls.Add(txtLimit);

            // Bodrum kontrolleri - fck satırının altında
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

            // Sol: Combinations and Cases
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
                Size = new Size(150, 175),  // Genişlik azaltıldı: 180 -> 150
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

            // ========== FRAME ASSIGNMENT & ELEMENT FORCES TABLOLARI ==========
            TableLayoutPanel tlpDataTables = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 10, 5)
            };
            tlpDataTables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDataTables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Sol: Frame Assignment
            RoundedPanel pnlFrameData = new RoundedPanel { Title = "Frame Assignment", Dock = DockStyle.Fill, BorderRadius = 20, Margin = new Padding(0, 0, 5, 0) };
            Button btnGetFrame = new Button { Text = "Getir", Size = new Size(60, 28), Location = new Point(15, 35), BackColor = Color.FromArgb(220, 220, 220), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand };
            btnGetFrame.FlatAppearance.BorderSize = 1;
            btnGetFrame.Click += BtnGetFrameAssignment_Click;
            pnlFrameData.Controls.Add(btnGetFrame);

            lblKolonFrameStatus = new Label { Text = "", Location = new Point(85, 40), Size = new Size(100, 20), Font = new Font("Segoe UI", 8), ForeColor = Color.Green };
            pnlFrameData.Controls.Add(lblKolonFrameStatus);
            tlpDataTables.Controls.Add(pnlFrameData, 0, 0);

            // Sağ: Element Forces
            // Sağ: Element Forces
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
                BaseColor = Color.FromArgb(159, 219, 255),  // Göreli Kat Ötelemesi ile aynı
                BorderRadius = 20,                           // Göreli Kat Ötelemesi ile aynı
                GrowAmount = 2
            };
            btnCalculate.Click += BtnCalculateKolonEksenel_Click;
            pnlCalcBtn.Controls.Add(btnCalculate);
            tlpLeft.Controls.Add(pnlCalcBtn, 0, 2);

            // ========== SINIRI AŞAN KOLONLAR (YENİ) ==========
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
            dgvKolonResults.Columns.Add("Limit", "0.40");

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

            // --- ALT NAVİGASYON PANELİ ---
            page.Tag = 5;
            page.VisibleChanged += (s, e) => {
                if (page.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = CreateNavigationPanel(5);
                    mainLayout.Controls.Add(navPanel, 0, 2);
                }
            };

            page.Controls.Add(mainLayout);
        }

        // Info Popup Göster - Frame Assignment
        private void ShowFrameInfoPopup(Label infoLabel)
        {
            if (_kolonFrameAssignments.Count == 0)
            {
                _kolonInfoTooltip.Show("Veri yok. Önce 'Getir' butonuna basın.", infoLabel, 0, 20, 3000);
                return;
            }

            string info = $"Frame Assignments (İlk 10 Kayıt):\n\n";
            info += String.Format("{0,-12} {1,-8} {2,-12} {3,-10}\n", "Label", "Story", "Section", "Area(mm2)");
            info += new string('-', 45) + "\n";
            foreach (var item in _kolonFrameAssignments.Take(10))
                info += String.Format("{0,-12} {1,-8} {2,-12} {3,-10:0.0}\n", item.Label, item.Story, item.SectionName, item.Area);
            if (_kolonFrameAssignments.Count > 10) info += $"\n... ve {_kolonFrameAssignments.Count - 10} kayıt daha.";

            _kolonInfoTooltip.Show(info, infoLabel, 0, 20, 8000);
        }

        // Info Popup Göster - Column Forces
        private void ShowColumnForcesInfoPopup(Label infoLabel)
        {
            if (_kolonColumnForces.Count == 0)
            {
                _kolonInfoTooltip.Show("Veri yok. Önce 'Değerleri Getir' butonuna basın.", infoLabel, 0, 20, 3000);
                return;
            }

            string info = $"Column Forces (İlk 10 Kayıt):\n\n";
            info += String.Format("{0,-8} {1,-8} {2,-12} {3,-10}\n", "Story", "Column", "LoadCase", "P(kN)");
            info += new string('-', 40) + "\n";
            foreach (var item in _kolonColumnForces.Take(10))
            {
                string lc = item.LoadCase.Length > 12 ? item.LoadCase.Substring(0, 10) + ".." : item.LoadCase;
                info += String.Format("{0,-8} {1,-8} {2,-12} {3,-10:0.0}\n", item.Story, item.Column, lc, item.P);
            }
            if (_kolonColumnForces.Count > 10) info += $"\n... ve {_kolonColumnForces.Count - 10} kayıt daha.";

            _kolonInfoTooltip.Show(info, infoLabel, 0, 20, 8000);
        }

        // Info Popup Kapat
        private void HideKolonInfoPopup()
        {
            _kolonInfoTooltip.Hide(this);
        }

        // Kolon sayfası için kombinasyonları yükle
        private void BtnKolonLoadCombos_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lstKolonCombinations.Items.Clear();

                // Load Cases'leri al (tüm tipler için)
                int numCases = 0;
                string[] caseNames = null;
                _sapModel.LoadCases.GetNameList(ref numCases, ref caseNames);

                if (caseNames != null)
                {
                    foreach (string caseName in caseNames)
                        lstKolonCombinations.Items.Add(caseName);
                }

                // Kombinasyonları al
                int numCombos = 0;
                string[] comboNames = null;
                _sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);

                if (comboNames != null)
                {
                    foreach (string combo in comboNames)
                        lstKolonCombinations.Items.Add(combo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kombinasyon/Case yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kolon sayfası için kombinasyon seç (sadece seçim, veri çekmez)
        private void BtnKolonSelectCombos_Click(object sender, EventArgs e)
        {
            if (lstKolonCombinations.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir kombinasyon seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var item in lstKolonCombinations.SelectedItems)
            {
                string combo = item.ToString();
                if (!_kolonSelectedCombos.Contains(combo))
                {
                    _kolonSelectedCombos.Add(combo);
                    AddKolonComboTag(combo);
                }
            }
        }

        // Kolon sayfası için kombinasyon çift tıkla
        private void LstKolonCombinations_DoubleClick(object sender, EventArgs e)
        {
            if (lstKolonCombinations.SelectedItem == null) return;
            
            string combo = lstKolonCombinations.SelectedItem.ToString();
            if (!_kolonSelectedCombos.Contains(combo))
            {
                _kolonSelectedCombos.Add(combo);
                AddKolonComboTag(combo);
            }

            FetchKolonColumnForcesForSelectedCombos();
        }

        // Kolon sayfası için kombinasyon tag'i ekle - X tuşlu
        private void AddKolonComboTag(string comboName)
        {
            Panel tag = new Panel
            {
                AutoSize = false,
                Height = 22,
                Width = 110,
                BackColor = Color.FromArgb(100, 160, 255),
                Padding = new Padding(3, 2, 3, 2),
                Margin = new Padding(2)
            };

            Label lbl = new Label
            {
                Text = comboName.Length > 12 ? comboName.Substring(0, 10) + ".." : comboName,
                AutoSize = false,
                Size = new Size(85, 18),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 7),
                Location = new Point(2, 2),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label btnX = new Label
            {
                Text = "✕",
                AutoSize = false,
                Size = new Size(18, 18),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(88, 2),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(200, 80, 80)
            };
            btnX.Click += (s, e) =>
            {
                _kolonSelectedCombos.Remove(comboName);
                pnlKolonSelectedCombos.Controls.Remove(tag);
            };

            tag.Controls.Add(lbl);
            tag.Controls.Add(btnX);
            pnlKolonSelectedCombos.Controls.Add(tag);
        }

// Seçili kombinasyonlar için TÜM SÜTUNLARI çek, FİLTRELE (Gruplama YOK)
        private void FetchKolonColumnForcesForSelectedCombos()
        {
            if (_sapModel == null) return;

            try
            {
                // 1. ETABS Ayarları
                _sapModel.SelectObj.ClearSelection(); // Önce temizle
                _sapModel.SelectObj.All(); // TÜMÜNÜ SEÇ (Bazı versiyonlarda boş seçim veri getirmiyor)
                _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                foreach (var combo in _kolonSelectedCombos)
                {
                    _sapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    _sapModel.Results.Setup.SetComboSelectedForOutput(combo);
                }

                string tableName = "Element Forces - Columns";
                string groupName = "";
                string[] fieldKeyList = null;
                int tableVersion = 0;
                string[] fieldsKeysIncluded = null;
                int numRecords = 0;
                string[] tableData = null;

                int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);

                // Alternatif Arama (Eski Versiyonlar veya Veri Yoksa)
                if (ret != 0 || numRecords == 0)
                {
                    int numTables = 0;
                    string[] availableTables = null; string[] desc = null; int[] import = null;
                    _sapModel.DatabaseTables.GetAvailableTables(ref numTables, ref availableTables, ref desc, ref import);
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
                    ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
                }

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    MessageBox.Show($"Element Forces tablosu okunamadı veya boş!\nTablo: {tableName}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int numFields = fieldsKeysIncluded.Length;

                // 1. İndeksleri Bul (Önce bul ki CSV yazarken filtreleyelim)
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
                    
                    // Station kontrolü - orijinal sütun adını da kontrol et
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

                // CSV DUMP kaldırıldı - Veri işleme devam ediyor 

                _kolonColumnForces.Clear();
                int numRows = numRecords;

                // Verileri İşle ve Listeye Ekle
                for (int row = 0; row < numRows; row++)
                {
                    int baseIndex = row * numFields;
                    string loadCase = caseIdx >= 0 ? tableData[baseIndex + caseIdx] : "";

                    // A) Kombinasyon Filtresi - Seçili kombinasyonlardan biri olmalı
                    bool matchedCombo = false;
                    foreach (var combo in _kolonSelectedCombos)
                    {
                        if (string.Equals(loadCase, combo, StringComparison.OrdinalIgnoreCase))
                        {
                            matchedCombo = true;
                            break;
                        }
                    }
                    if (!matchedCombo) continue;

                    // B) Station = 0 Filtresi (ZORUNLU - Eski haline getirildi)
                    string stationStr = stationIdx >= 0 ? tableData[baseIndex + stationIdx] : "";
                    stationStr = stationStr.Replace(",", ".");
                    
                    double stationValue = -999;
                    if (double.TryParse(stationStr, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out stationValue))
                    {
                        if (Math.Abs(stationValue) > 0.0001) continue;
                    }

                    // C) StepType = Min Filtresi (ZORUNLU - FIX: Boş ise Static, atlama!)
                    string stepType = stepTypeIdx >= 0 ? tableData[baseIndex + stepTypeIdx] : "";
                    if (!string.IsNullOrEmpty(stepType) && !string.Equals(stepType, "Min", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Veriyi Oku
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

                if (_kolonColumnForces.Count > 0)
                {
                    if (lblKolonStatus != null)
                    {
                        lblKolonStatus.Text = $"{_kolonColumnForces.Count} satır veri hazır.";
                        lblKolonStatus.ForeColor = Color.Green;
                    }
                }
                else
                {
                    if (lblKolonStatus != null)
                    {
                        lblKolonStatus.Text = "Filtrelere uygun veri bulunamadı.";
                        lblKolonStatus.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Element Forces işleme hatası:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Temizlik: Seçimleri kaldır
                if (_sapModel != null)
                     _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            }
        }

        // Element Forces tablosunu getir tuşu
        private void BtnKolonGetValues_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_sapModel.GetModelIsLocked())
            {
                MessageBox.Show("Model kilitli değil. Lütfen analiz yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_kolonSelectedCombos.Count == 0)
            {
                MessageBox.Show("Önce kombinasyon seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verileri çek (Filtreleme ve Listeye atma işlemi helper metodun içinde yapılır)
            FetchKolonColumnForcesForSelectedCombos();

            // Durum Label'ını güncelle
            var button = sender as Button;
            if (button?.Parent != null)
            {
                foreach (Control ctrl in button.Parent.Controls)
                {
                    if (ctrl is Label lbl && lbl.Name == "lblKolonElemStatus")
                    {
                        if (_kolonColumnForces.Count > 0)
                        {
                            lbl.Text = "Tablo alındı.";
                            lbl.ForeColor = Color.Green;
                        }
                        else
                        {
                            lbl.Text = "Hata / Veri Yok";
                            lbl.ForeColor = Color.Red;
                        }
                        break;
                    }
                }
            }
        }

        private void BtnGetFrameAssignment_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
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

                int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);

                if (ret != 0 || fieldsKeysIncluded == null || fieldsKeysIncluded.Length == 0)
                {
                    MessageBox.Show("Frame Assignments tablosu okunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                int numFields = fieldsKeysIncluded.Length;

                // 1. Sütun İndekslerini Belirle (Önce bul ki CSV yazarken filtreleyelim)
                int storyIdx = -1, labelIdx = -1, uniqueIdx = -1, designTypeIdx = -1;
                int analysisSectionIdx = -1, designSectionIdx = -1, sectionPropIdx = -1;

                for (int i = 0; i < numFields; i++)
                {
                    string col = fieldsKeysIncluded[i];

                    if (col.IndexOf("Story", StringComparison.OrdinalIgnoreCase) >= 0) storyIdx = i;
                    else if (col.IndexOf("Label", StringComparison.OrdinalIgnoreCase) >= 0) labelIdx = i;
                    
                    // Unique check
                    else if (col.IndexOf("Unique", StringComparison.OrdinalIgnoreCase) >= 0) uniqueIdx = i;
                    
                    // Type check
                    else if (col.IndexOf("Type", StringComparison.OrdinalIgnoreCase) >= 0) designTypeIdx = i;

                    // Section checks
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
                    
                    // Sadece Kolonları Al (Eğer kolon tipi sütunu bulunduysa)
                    // Bulunamadıysa hepsini al ki tablo boş kalmasın.
                    if (designTypeIdx >= 0 && designType.IndexOf("Column", StringComparison.OrdinalIgnoreCase) < 0) continue;

                    string story = storyIdx >= 0 ? tableData[baseIndex + storyIdx] : "";
                    string label = labelIdx >= 0 ? tableData[baseIndex + labelIdx] : "";
                    string unique = uniqueIdx >= 0 ? tableData[baseIndex + uniqueIdx] : "";
                    
                    // KRİTİK DÜZELTME: Veri eşleşmesi için UniqueName ŞART.
                    // Eğer ETABS'tan boş geliyorsa (ki geliyor), Label'i (C1 vb.) UniqueName olarak kabul ediyoruz.
                    // Bu olmazsa sonuç tablosu BOŞ gelir (0 eşleşme).
                    if (string.IsNullOrEmpty(unique)) unique = label;
                    
                    // Kesit İsmi Belirleme Stratejisi
                    string finalSection = "";
                    if (analysisSectionIdx >= 0) finalSection = tableData[baseIndex + analysisSectionIdx];
                    if (string.IsNullOrEmpty(finalSection) && designSectionIdx >= 0) finalSection = tableData[baseIndex + designSectionIdx];
                    if (string.IsNullOrEmpty(finalSection) && sectionPropIdx >= 0) finalSection = tableData[baseIndex + sectionPropIdx];

                    // Boyutları parse et (C40X90 -> 400x900 mm)
                    double width = 0, height = 0;
                    if (!string.IsNullOrEmpty(finalSection))
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(finalSection, @"(\d+(?:[.,]\d+)?)\s*[xX*]\s*(\d+(?:[.,]\d+)?)");
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
                    if (lblKolonFrameStatus != null)
                    {
                        lblKolonFrameStatus.Text = "Tablo alındı.";
                        lblKolonFrameStatus.ForeColor = Color.Green;
                    }
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
                if (lblKolonFrameStatus != null)
                {
                    lblKolonFrameStatus.Text = "Hata";
                    lblKolonFrameStatus.ForeColor = Color.Red;
                }
                }
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========================================================================================================
        //                               İkinci Mertebe Etkileri Sayfası
        // ========================================================================================================

        private void InitializeIkinciMertebePage(TabPage page)
        {
            page.BackColor = colorBackground;

            // Ana layout: 3 satırlı - başlık, içerik, navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // --- SAYFA BAŞLIĞI ---
            Label lblPageTitle = new Label
            {
                Text = "İkinci Mertebe Etkileri Tahkiki",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainLayout.Controls.Add(lblPageTitle, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlp.Padding = new Padding(20, 10, 20, 10);

            // =============== SOL PANEL ===============
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill };
            
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 185F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            // --- KOMBİNASYON SEÇİM ALANI ---
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

            lstCombosIkinci = new ListBox
            {
                Location = new Point(15, 35),
                Size = new Size(145, 95),
                SelectionMode = SelectionMode.MultiExtended,
                Font = new Font("Segoe UI", 8)
            };

            Button btnLoadCombosIkinci = new Button
            {
                Text = "Getir",
                Size = new Size(55, 28),
                Location = new Point(15, 135),
                BackColor = Color.FromArgb(220, 220, 220),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLoadCombosIkinci.FlatAppearance.BorderSize = 1;
            btnLoadCombosIkinci.Click += BtnGetCombosIkinci_Click;

            Button btnSelectCombosIkinci = new Button
            {
                Text = "Seç",
                Size = new Size(55, 28),
                Location = new Point(80, 135),
                BackColor = Color.FromArgb(255, 236, 159),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSelectCombosIkinci.FlatAppearance.BorderSize = 1;
            btnSelectCombosIkinci.Click += BtnSelectCombosIkinci_Click;

            pnlCombos.Controls.Add(lstCombosIkinci);
            pnlCombos.Controls.Add(btnLoadCombosIkinci);
            pnlCombos.Controls.Add(btnSelectCombosIkinci);
            tlpCombos.Controls.Add(pnlCombos, 0, 0);

            // Sağ: Seçili Kombinasyonlar paneli
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
                Size = new Size(145, 130),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White
            };
            pnlSelectedWrapper.Controls.Add(pnlIkinciSelectedCombos);
            tlpCombos.Controls.Add(pnlSelectedWrapper, 1, 0);

            tlpLeft.Controls.Add(tlpCombos, 0, 0);

            // --- PARAMETRELER ---
            RoundedPanel pnlParams = new RoundedPanel
            {
                Title = "Hesap Parametreleri",
                Dock = DockStyle.Fill,
                BorderRadius = 20,
                Margin = new Padding(0, 5, 10, 5)
            };

            int startY = 40;
            int gapY = 32;
            int labelX = 15;
            int textX = 120;
            int textW = 80;

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

            // --- VERİ SATIRLARI (Her biri: İsim + Getir Butonu + Durum) ---
            int dataRowStartY = startY + gapY * 5;
            int dataRowGap = 28;
            int btnW = 60;
            int btnH = 22;

            // 1. Kat Yükseklikleri (Story Definitions)
            pnlParams.Controls.Add(new Label { Text = "Kat Yüksek.:", Location = new Point(labelX, dataRowStartY), AutoSize = true, Font = new Font("Segoe UI", 8) });
            Button btnGetStory = new Button { Text = "Getir", Location = new Point(textX, dataRowStartY - 3), Size = new Size(btnW, btnH), FlatStyle = FlatStyle.Flat, BackColor = Color.LightBlue };
            btnGetStory.Click += BtnGetStoryData_Click;
            pnlParams.Controls.Add(btnGetStory);
            lblStatusStory = new Label { Text = "⚠ Veri yok", Location = new Point(textX + btnW + 5, dataRowStartY), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8) };
            pnlParams.Controls.Add(lblStatusStory);

            // 2. Kat Kütleleri (Mass Summary by Story)
            pnlParams.Controls.Add(new Label { Text = "Kat Kütleleri:", Location = new Point(labelX, dataRowStartY + dataRowGap), AutoSize = true, Font = new Font("Segoe UI", 8) });
            Button btnGetMass = new Button { Text = "Getir", Location = new Point(textX, dataRowStartY + dataRowGap - 3), Size = new Size(btnW, btnH), FlatStyle = FlatStyle.Flat, BackColor = Color.LightBlue };
            btnGetMass.Click += BtnGetMassData_Click;
            pnlParams.Controls.Add(btnGetMass);
            lblStatusMass = new Label { Text = "⚠ Veri yok", Location = new Point(textX + btnW + 5, dataRowStartY + dataRowGap), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8) };
            pnlParams.Controls.Add(lblStatusMass);

            // 3. Kat Kuvvetleri (Story Forces)
            pnlParams.Controls.Add(new Label { Text = "Kat Kuvvetleri:", Location = new Point(labelX, dataRowStartY + dataRowGap * 2), AutoSize = true, Font = new Font("Segoe UI", 8) });
            Button btnGetForce = new Button { Text = "Getir", Location = new Point(textX, dataRowStartY + dataRowGap * 2 - 3), Size = new Size(btnW, btnH), FlatStyle = FlatStyle.Flat, BackColor = Color.LightBlue };
            btnGetForce.Click += BtnGetForceData_Click;
            pnlParams.Controls.Add(btnGetForce);
            lblStatusForce = new Label { Text = "⚠ Veri yok", Location = new Point(textX + btnW + 5, dataRowStartY + dataRowGap * 2), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8) };
            pnlParams.Controls.Add(lblStatusForce);

            // 4. Kat Ötelemeleri (Story Max Over Avg Drifts)
            pnlParams.Controls.Add(new Label { Text = "Kat Ötelemeleri:", Location = new Point(labelX, dataRowStartY + dataRowGap * 3), AutoSize = true, Font = new Font("Segoe UI", 8) });
            Button btnGetDrift = new Button { Text = "Getir", Location = new Point(textX, dataRowStartY + dataRowGap * 3 - 3), Size = new Size(btnW, btnH), FlatStyle = FlatStyle.Flat, BackColor = Color.LightBlue };
            btnGetDrift.Click += BtnGetDriftData_Click;
            pnlParams.Controls.Add(btnGetDrift);
            lblStatusDrift = new Label { Text = "⚠ Veri yok", Location = new Point(textX + btnW + 5, dataRowStartY + dataRowGap * 3), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8) };
            pnlParams.Controls.Add(lblStatusDrift);

            tlpLeft.Controls.Add(pnlParams, 0, 1);

            // --- HESAPLA BUTONU ---
            Panel pnlButton = new Panel { Dock = DockStyle.Fill };
            SmoothButton btnCalculate = new SmoothButton
            {
                Text = "Hesapla ve Kaydet",
                Size = new Size(200, 40),
                Location = new Point(50, 5),
                BaseColor = Color.FromArgb(255, 236, 159),
                BorderRadius = 20,
                GrowAmount = 2
            };
            btnCalculate.Click += BtnCalculateIkinciMertebe_Click;
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
                    BackColor = Color.FromArgb(255, 236, 159),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
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

            Panel dgvContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 40, 15, 15)
            };
            dgvContainer.Controls.Add(dgvIkinciMertebeResults);
            dgvContainer.Controls.Add(lblIkinciMertebeStatus);

            pnlResults.Controls.Add(dgvContainer);
            tlp.Controls.Add(pnlResults, 1, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

            // --- ALT NAVİGASYON PANELİ ---
            page.Tag = 4;
            page.VisibleChanged += (s, e) => {
                if (page.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = CreateNavigationPanel(4);
                    mainLayout.Controls.Add(navPanel, 0, 2);
                }
            };

            page.Controls.Add(mainLayout);
        }

        // İkinci Mertebe için kombinasyon seç butonu
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

        // İkinci Mertebe için kombinasyon tag'i ekle
        private void AddIkinciComboTag(string comboName)
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
                    e.Graphics.FillPath(new SolidBrush(Color.FromArgb(255, 236, 159)), path);
                    e.Graphics.DrawPath(new Pen(Color.FromArgb(200, 180, 100)), path);
                }
            };

            Label lbl = new Label
            {
                Text = comboName.Length > 12 ? comboName.Substring(0, 10) + ".." : comboName,
                Location = new Point(5, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 7)
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
                pnlIkinciSelectedCombos.Controls.Remove(tag); 
                _ikinciSelectedCombos.Remove(comboName);
            };

            tag.Controls.Add(lbl);
            tag.Controls.Add(btnRemove);
            pnlIkinciSelectedCombos.Controls.Add(tag);
        }

        private void BtnGetCombosIkinci_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lstCombosIkinci.Items.Clear();

                // Response Combinations
                int numCombos = 0;
                string[] comboNames = null;
                _sapModel.RespCombo.GetNameList(ref numCombos, ref comboNames);
                if (comboNames != null)
                    foreach (var name in comboNames)
                        lstCombosIkinci.Items.Add(name);

                // Load Cases
                int numCases = 0;
                string[] caseNames = null;
                _sapModel.LoadCases.GetNameList(ref numCases, ref caseNames);
                if (caseNames != null)
                    foreach (var name in caseNames)
                        lstCombosIkinci.Items.Add(name);

                if (lstCombosIkinci.Items.Count == 0)
                    MessageBox.Show("Kombinasyon veya case bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kombinasyonlar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void BtnCalculateIkinciMertebe_Click(object sender, EventArgs e)
        {
            if (_sapModel == null)
            {
                MessageBox.Show("Önce ETABS'a bağlanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Girdileri Al
            if (!double.TryParse(txtCh.Text, out double Ch) || !double.TryParse(txtR_Ikinci.Text, out double R) || !double.TryParse(txtD_Ikinci.Text, out double D))
            {
                MessageBox.Show("Lütfen Ch, R ve D parametrelerini geçerli sayı olarak girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_ikinciSelectedCombos.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir kombinasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // Veri listelerini oluştur - her kat için doğru kombinasyonları kullanarak
                var xForces = new List<ForceData>();
                var xDrifts = new List<DriftData>();
                var yForces = new List<ForceData>();
                var yDrifts = new List<DriftData>();

                foreach (var f in forces)
                {
                    int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(f.Story);
                    bool isBodrumKat = isBodrum && storyNum != null && storyNum <= bodrumKatSayisi;
                    
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
                    int? storyNum = GoreliKatOtelemesiManager.ExtractStoryNumber(d.Story);
                    bool isBodrumKat = isBodrum && storyNum != null && storyNum <= bodrumKatSayisi;
                    
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

                // Sonuçları CSV'ye kaydet
                SaveResultsToCSV(results);
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
            if (results.Count == 0)
            {
                lblIkinciMertebeStatus.Text = "Hesaplanacak veri bulunamadı.";
                return;
            }

            try
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = System.IO.Path.Combine(appPath, "IkinciMertebe_Sonuclari.csv");
                
                using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("Story;LoadCase;Direction;Vi (kN);Wij (kN);Drift Ratio;Theta;Limit;Status");
                    foreach (var res in results)
                    {
                        writer.WriteLine($"{res.Story};{res.LoadCase};{res.Direction};{res.Vi:F2};{res.Wij:F2};{res.DriftRatio:F6};{res.Theta:F6};{res.Limit:F4};{res.Status}");
                    }
                }
                
                // Status mesajı değiştirme - allOk kontrolünde zaten ayarlandı
                MessageBox.Show($"Sonuçlar başarıyla kaydedildi:\n{filePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // --- BİREYSEL VERİ ÇEKME BUTONLARI ---
        private void BtnGetStoryData_Click(object sender, EventArgs e)
        {
            if (_sapModel == null) { MessageBox.Show("Önce ETABS'a bağlanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            try
            {
                // Cache için doldur
                _storyDataList.Clear();
                FetchStoryData();
                
                lblStatusStory.Text = $"✅ {_storyDataList.Count}";
                lblStatusStory.ForeColor = Color.Green;
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnGetMassData_Click(object sender, EventArgs e)
        {
            if (_sapModel == null) { MessageBox.Show("Önce ETABS'a bağlanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            try
            {
                // Cache için doldur
                _massDataList.Clear();
                FetchMassData();

                lblStatusMass.Text = $"✅ {_massDataList.Count}";
                lblStatusMass.ForeColor = Color.Green;
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnGetForceData_Click(object sender, EventArgs e)
        {
            if (_sapModel == null) { MessageBox.Show("Önce ETABS'a bağlanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (_ikinciSelectedCombos.Count == 0) { MessageBox.Show("Lütfen kombinasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            
            // ETABS kilitli mi kontrol et (analiz yapılmış mı)
            bool isLocked = _sapModel.GetModelIsLocked();
            if (!isLocked)
            {
                MessageBox.Show("ETABS modeli kilitli değil. Lütfen önce analizi çalıştırın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Kombinasyonları seç
                _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                foreach (var combo in _ikinciSelectedCombos)
                {
                    _sapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    _sapModel.Results.Setup.SetComboSelectedForOutput(combo);
                }

                // Cache için doldur
                _cachedForces = FetchStoryForces(_ikinciSelectedCombos);

                lblStatusForce.Text = $"✅ {_cachedForces.Count}";
                lblStatusForce.ForeColor = Color.Green;
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { _sapModel?.Results.Setup.DeselectAllCasesAndCombosForOutput(); }
        }

        private void BtnGetDriftData_Click(object sender, EventArgs e)
        {
            if (_sapModel == null) { MessageBox.Show("Önce ETABS'a bağlanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (_ikinciSelectedCombos.Count == 0) { MessageBox.Show("Lütfen kombinasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            
            // ETABS kilitli mi kontrol et (analiz yapılmış mı)
            bool isLocked = _sapModel.GetModelIsLocked();
            if (!isLocked)
            {
                MessageBox.Show("ETABS modeli kilitli değil. Lütfen önce analizi çalıştırın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Kombinasyonları seç
                _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                foreach (var combo in _ikinciSelectedCombos)
                {
                    _sapModel.Results.Setup.SetCaseSelectedForOutput(combo);
                    _sapModel.Results.Setup.SetComboSelectedForOutput(combo);
                }

                // Cache için doldur
                _cachedDrifts = FetchStoryDrifts(_ikinciSelectedCombos);

                lblStatusDrift.Text = $"✅ {_cachedDrifts.Count}";
                lblStatusDrift.ForeColor = Color.Green;
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { _sapModel?.Results.Setup.DeselectAllCasesAndCombosForOutput(); }
        }

        /// <summary>
        /// Story Forces tablosundan kuvvet verilerini çeker.
        /// Sütunlar: Story, Output Case, Location, VX, VY
        /// Sadece Location = "Bottom" olan satırlar alınır (Kat kesme kuvveti).
        /// </summary>
        private List<ForceData> FetchStoryForces(List<string> combos)
        {
            var forces = new List<ForceData>();
            
            string tableName = "Story Forces";
            string groupName = "";
            string[] fieldKeyList = null;
            int tableVersion = 0;
            string[] fieldsKeysIncluded = null;
            int numRecords = 0;
            string[] tableData = null;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            
            if (ret != 0 || fieldsKeysIncluded == null || numRecords == 0) return forces;

            int numFields = fieldsKeysIncluded.Length;
            int storyIdx = -1, caseIdx = -1, locIdx = -1, vxIdx = -1, vyIdx = -1;

            for (int i = 0; i < numFields; i++)
            {
                string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", "");
                if (col == "STORY") storyIdx = i;
                else if (col == "OUTPUTCASE" || col == "LOADCASE" || col == "CASE") caseIdx = i;
                else if (col == "LOCATION") locIdx = i;
                else if (col == "VX") vxIdx = i;
                else if (col == "VY") vyIdx = i;
            }

            if (storyIdx == -1 || caseIdx == -1) return forces;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields;
                
                // Location filtresi: Sadece "Bottom" satırları al
                if (locIdx >= 0)
                {
                    string loc = tableData[baseIdx + locIdx] ?? "";
                    if (!loc.Equals("Bottom", StringComparison.OrdinalIgnoreCase)) continue;
                }

                string story = tableData[baseIdx + storyIdx] ?? "";
                string loadCase = tableData[baseIdx + caseIdx] ?? "";

                // Seçili kombinasyonlarla eşleşiyor mu?
                if (!combos.Any(c => loadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0)) continue;

                double vx = 0, vy = 0;
                if (vxIdx >= 0) double.TryParse(tableData[baseIdx + vxIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vx);
                if (vyIdx >= 0) double.TryParse(tableData[baseIdx + vyIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vy);

                forces.Add(new ForceData { Story = story, LoadCase = loadCase, VX = Math.Abs(vx), VY = Math.Abs(vy) });
            }

            return forces;
        }

        /// <summary>
        /// Story Max Over Avg Drifts veya Story Drifts tablosundan öteleme verilerini çeker.
        /// Sütunlar: Story, Output Case, Direction, Avg Drift (veya Drift)
        /// </summary>
        private List<DriftData> FetchStoryDrifts(List<string> combos)
        {
            var drifts = new List<DriftData>();
            
            // Önce "Story Max Over Avg Drifts" dene
            string tableName = "Story Max Over Avg Drifts";
            string groupName = "";
            string[] fieldKeyList = null;
            int tableVersion = 0;
            string[] fieldsKeysIncluded = null;
            int numRecords = 0;
            string[] tableData = null;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            
            // Yoksa "Story Drifts" dene
            if (ret != 0 || numRecords == 0)
            {
                tableName = "Story Drifts";
                ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            }
            
            if (ret != 0 || fieldsKeysIncluded == null || numRecords == 0) return drifts;

            int numFields = fieldsKeysIncluded.Length;
            int storyIdx = -1, caseIdx = -1, dirIdx = -1, driftIdx = -1;

            // Sütun adlarını logla (debug için)
            System.Diagnostics.Debug.WriteLine($"Drift Table Columns: {string.Join(", ", fieldsKeysIncluded)}");

            for (int i = 0; i < numFields; i++)
            {
                string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", "").Replace(".", "");
                if (col == "STORY") storyIdx = i;
                else if (col == "OUTPUTCASE" || col == "LOADCASE" || col == "CASE") caseIdx = i;
                else if (col == "DIRECTION") dirIdx = i;
                // Drift sütunu için daha fazla varyasyon
                else if (col == "AVGDRIFT" || col == "DRIFT" || col == "MAXDRIFT" || col == "DRIFTRATIO" || col.Contains("DRIFT")) 
                {
                    if (driftIdx == -1 || col == "AVGDRIFT") driftIdx = i; // Öncelik Avg Drift'e
                }
            }

            System.Diagnostics.Debug.WriteLine($"driftIdx: {driftIdx}, storyIdx: {storyIdx}, caseIdx: {caseIdx}");

            // Direkt index 6'yı kullan (7. sütun = Avg Drift)
            driftIdx = 6;

            if (storyIdx == -1 || caseIdx == -1) return drifts;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields;

                string story = tableData[baseIdx + storyIdx] ?? "";
                string loadCase = tableData[baseIdx + caseIdx] ?? "";

                // Seçili kombinasyonlarla eşleşiyor mu?
                if (!combos.Any(c => loadCase.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0)) continue;

                string direction = (dirIdx >= 0) ? (tableData[baseIdx + dirIdx] ?? "") : "";
                double drift = 0;
                if (driftIdx >= 0 && driftIdx < numFields) 
                {
                    double.TryParse(tableData[baseIdx + driftIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out drift);
                }

                drifts.Add(new DriftData { Story = story, LoadCase = loadCase, Direction = direction, Drift = drift });
            }

            // DEBUG: İlk 5 drift verisini göster
            if (drifts.Count > 0)
            {
                var debugInfo = string.Join("\n", drifts.Take(5).Select(d => $"Story={d.Story}, Case={d.LoadCase}, Dir={d.Direction}, Drift={d.Drift}"));
                System.Diagnostics.Debug.WriteLine($"Fetched {drifts.Count} drifts:\n{debugInfo}");
            }

            return drifts;
        }

        private void FetchStoryData()
        {
            _storyDataList.Clear();
            
            // "Story Definitions" tablosunu kullan
            string tableName = "Story Definitions";
            string groupName = "";
            string[] fieldKeyList = null;
            int tableVersion = 0;
            string[] fieldsKeysIncluded = null;
            int numRecords = 0;
            string[] tableData = null;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            
            if (ret != 0 || fieldsKeysIncluded == null) return;
            
            int numFields = fieldsKeysIncluded.Length;
            int storyIdx = -1, heightIdx = -1, elevationIdx = -1;

            for (int i = 0; i < numFields; i++)
            {
                string col = fieldsKeysIncluded[i];
                if (col == "Story") storyIdx = i;
                else if (col == "Height") heightIdx = i;
                else if (col == "Elevation") elevationIdx = i;
            }

            if (storyIdx == -1 || heightIdx == -1) return;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields;
                string storyName = tableData[baseIdx + storyIdx];
                if (storyName.Equals("Base", StringComparison.OrdinalIgnoreCase)) continue;

                double height = 0;
                double elevation = 0;
                double.TryParse(tableData[baseIdx + heightIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out height);
                if (elevationIdx != -1)
                    double.TryParse(tableData[baseIdx + elevationIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out elevation);

                // Yükseklik birimi mm ise m'ye çevir.
                // API dökümanına göre tablo verileri "Database Units" ile gelir. 
                // Standart SI birimlerde genellikle mm çalışılırsa mm gelir.
                // Şimdilik mm varsayıp /1000 yapıyoruz (eski koddaki gibi).
                // DOĞRUSU: Birim kontrolü yapmaktır ama basitlik açısından 1000'e bölüyoruz. 
                
                _storyDataList.Add(new StoryData
                {
                    Name = storyName,
                    Height = height / 1000.0, 
                    Elevation = elevation / 1000.0,
                    IsBodrum = false 
                });
            }

            // Bodrum katları işaretle
            if (chkBodrumIkinci.Checked)
            {
                int bodrumCount = (int)numBodrumKatIkinci.Value;
                var sortedStories = _storyDataList.OrderBy(s => s.Elevation).ToList();
                
                for (int i = 0; i < bodrumCount && i < sortedStories.Count; i++)
                {
                    sortedStories[i].IsBodrum = true;
                }
            }
        }

        private void FetchMassData()
        {
            _massDataList.Clear();
            string tableName = "Mass Summary by Story";
            string groupName = "";
            string[] fieldKeyList = null;
            int tableVersion = 0;
            string[] fieldsKeysIncluded = null;
            int numRecords = 0;
            string[] tableData = null;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(tableName, ref fieldKeyList, groupName, ref tableVersion, ref fieldsKeysIncluded, ref numRecords, ref tableData);
            
            if (ret != 0 || fieldsKeysIncluded == null) return;

            int numFields = fieldsKeysIncluded.Length;
            int storyIdx = -1, massXIdx = -1, massYIdx = -1;

            for (int i = 0; i < numFields; i++)
            {
                string col = fieldsKeysIncluded[i].ToUpper().Replace(" ", "");
                if (col == "STORY") storyIdx = i;
                else if (col == "UX" || col == "MASSX") massXIdx = i;
                else if (col == "UY" || col == "MASSY") massYIdx = i;
            }

            if (storyIdx == -1) return;

            for (int row = 0; row < numRecords; row++)
            {
                int baseIdx = row * numFields;
                string story = tableData[baseIdx + storyIdx];
                if (story.Equals("Base", StringComparison.OrdinalIgnoreCase)) continue;

                double massX = 0, massY = 0;
                if (massXIdx >= 0) double.TryParse(tableData[baseIdx + massXIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out massX);
                if (massYIdx >= 0) double.TryParse(tableData[baseIdx + massYIdx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out massY);

                // Mass birimi: ETABS çıktısı Ton-s^2/m veya kg olabilir.
                // Genellikle Mass Summary tablosunda kütle birimi verilir.
                // Biz kN ağırlığa çevireceğiz: Weight = Mass * 9.81 (Eğer mass Ton ise -> kN = Ton * 9.81)
                // Varsayım: Mass birimi Ton.

                _massDataList.Add(new MassData
                {
                    Story = story,
                    Mass = massX, // X ve Y kütlesi genellikle aynıdır, fark varsa ortalama veya yönüne göre alınabilir. Şimdilik X.
                    Weight = massX * 9.81 // Ton -> kN
                });
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

                // A Kombinasyon Kontrolü (Bodrum için)
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

                if (_storyDataList.Count == 0) FetchStoryData(); 

                // Manager ile hesapla
                bool isBodrum = chkKolonBodrum.Checked;
                int bodrumCount = (int)numKolonBodrumKat.Value;

                var manager = new KolonEksenelYukManager(fck, limit, isBodrum, bodrumCount);
                var results = manager.Calculate(_kolonColumnForces, _kolonFrameAssignments, _storyDataList);

                // Sonuçları Tabloya Yaz
                dgvKolonResults.Rows.Clear();
                dgvKolonResults.Columns.Clear();
                
                // Excel'deki Sütun Yapısı
                dgvKolonResults.Columns.Add("Story", "Story");
                dgvKolonResults.Columns.Add("Column", "Column");
                // Unique Name kaldırıldı
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
                        // res.UniqueName, // Kaldırıldı
                        res.LoadCase,
                        res.Location,
                        res.Nd.ToString("0.00"), // Negatif değer görünebilir
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

                // Durum Label
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

                // İSTEK: Sınırı Aşanları Listele
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

                // İSTEK: Sonuç Excelini Kaydet
                SaveKolonEksenelResults(results, fck);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesaplama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kolon Eksenel Yük Kontrolü sonuçlarını CSV'ye kaydet
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
                    sw.WriteLine("Story;Column;Load Case/Combo;Location;P (kN);Analysis Section;b (cm);d (cm);Ac (cm²);Ac*fck (kN);0.40");

                    foreach (var result in results)
                    {
                        sw.WriteLine($"{result.Story};{result.Column};{result.LoadCase};{result.Location};{result.Nd:0.00};{result.Section};{result.B:0.00};{result.D:0.00};{result.Ac:0.00};{result.AcFck:0.00};{result.Limit:0.00}");
                    }
                }

                MessageBox.Show($"Sonuçlar kaydedildi:\n{filePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddContentPage(string title)
        {
            int tabIndex = mainTabControl.TabPages.Count; // Yeni sayfa index'i
            TabPage p = new TabPage(title);
            p.BackColor = colorBackground;

            // Ana layout: içerik + navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // Başlık
            Label lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel contentPanel = new Panel { Dock = DockStyle.Fill };
            contentPanel.Controls.Add(lbl);
            mainLayout.Controls.Add(contentPanel, 0, 0);

            // Navigasyon paneli - gecikmeli oluşturma (tüm sayfalar eklendikten sonra)
            p.Tag = tabIndex;
            p.VisibleChanged += (s, e) => {
                if (p.Visible && mainLayout.Controls.Count < 2)
                {
                    Panel navPanel = CreateNavigationPanel((int)p.Tag);
                    mainLayout.Controls.Add(navPanel, 0, 1);
                }
            };

            p.Controls.Add(mainLayout);
            mainTabControl.TabPages.Add(p);
        }

        // Sekme renklerini tanımla (dashboard ile aynı)
        private readonly Color[] tabColors = new Color[]
        {
            Color.FromArgb(240, 244, 248), // 0: Dashboard (kullanılmaz)
            Color.FromArgb(255, 159, 168), // 1: Tasarım Spektrumu
            Color.FromArgb(255, 220, 180), // 2: Artırım Hesabı
            Color.FromArgb(159, 219, 255), // 3: Göreli Kat Ötelemesi
            Color.FromArgb(255, 236, 159), // 4: İkinci Mertebe
            Color.FromArgb(255, 203, 159), // 5: Kolon Eksenel Yük Kontrolü
            Color.FromArgb(219, 190, 255)  // 6: Perde Kesme Kontrolü
        };

        private Panel CreateNavigationPanel(int currentTabIndex)
        {
            // Ana container - üst çizgi ile birlikte
            Panel container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            container.Paint += (s, e) => {
                e.Graphics.DrawLine(new Pen(Color.LightGray), 0, 0, container.Width, 0);
            };

            string[] tabNames = { 
                "Ana Menü", "Tasarım Spektrumu", "Artırım Hesabı", 
                "Göreli Kat Ötelemesi", "İkinci Mertebe", 
                "Kolon Eksenel Yük", "Perde Kesme"
            };

            // Görünecek butonları topla
            List<int> visibleIndices = new List<int>();
            for (int i = 0; i < tabNames.Length; i++)
                if (i != currentTabIndex) visibleIndices.Add(i);

            // TableLayoutPanel ile ortalanmış düzen
            TableLayoutPanel navPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                RowCount = 1,
                ColumnCount = visibleIndices.Count + 2, // +2 for side spacers
                Padding = new Padding(0, 8, 0, 8)
            };

            // Sol boşluk (esnek)
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Buton sütunları
            for (int i = 0; i < visibleIndices.Count; i++)
                navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // Sağ boşluk (esnek)
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Butonları ekle
            for (int i = 0; i < visibleIndices.Count; i++)
            {
                int targetIndex = visibleIndices[i];
                SmoothButton btn = new SmoothButton
                {
                    Text = tabNames[targetIndex],
                    Size = new Size(115, 32),
                    BaseColor = targetIndex < tabColors.Length ? tabColors[targetIndex] : Color.LightGray,
                    BorderRadius = 12,
                    Font = new Font("Segoe UI", 7f, FontStyle.Bold),
                    Margin = new Padding(6, 0, 6, 0),
                    GrowAmount = 3,
                    Anchor = AnchorStyles.None
                };
                btn.Click += (s, e) => GoToPage(targetIndex);
                navPanel.Controls.Add(btn, i + 1, 0); // +1 for left spacer
            }

            container.Controls.Add(navPanel);
            return container;
        }

        private void GoToPage(int index) { mainTabControl.SelectedIndex = index; }
        private void GoToHome() { mainTabControl.SelectedIndex = 0; }

        private Label CreateLabel(string text, int x, int y) { return new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true }; }
        private TextBox CreateTextBox(int x, int y) { return new TextBox { Location = new Point(x, y), Width = 100, Text = "0" }; }

        private void BtnCalculateSpectrum_Click(object sender, EventArgs e)
        {
            try
            {
                double valSDS = double.Parse(txtSDS.Text), valSD1 = double.Parse(txtSD1.Text);
                double valR = double.Parse(txtR.Text), valD = double.Parse(txtD.Text), valI = double.Parse(txtI.Text);

                if (valSDS == 0 || valSD1 == 0 || valR == 0 || valI == 0) { MessageBox.Show("Sıfırdan farklı değer giriniz."); return; }

                // Kayıt klasörü: Uygulamanın çalıştığı klasör
                string saveFolder = Application.StartupPath;

                var manager = new SpectrumManager(valSDS, valSD1, valR, valD, valI);
                var result = manager.Calculate(saveFolder);
                
                // Spektrum sonucunu artırım hesabı için kaydet
                _savedSpectrumResult = result;

                chartSpectrum.Series.Clear();
                var series = new Series("Tasarım Spektrumu (SaR)") { ChartType = SeriesChartType.Line, BorderWidth = 3, Color = Color.Crimson };
                for (int i = 0; i < result.Periods.Count; i++) series.Points.AddXY(result.Periods[i], result.Accelerations[i]);
                chartSpectrum.Series.Add(series);

                // Veri panelini güncelle
                scrollableDataPanel.SetData(result.Periods, result.Accelerations);

                lblSpectrumStatus.Text = $"Dosya kaydedildi: {result.FilePath}";
                lblSpectrumStatus.ForeColor = Color.Green;
                MessageBox.Show($"Spektrum hesaplandı ve kaydedildi:\n{result.FilePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        // ROT için Win32 API
        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(uint reserved, out IRunningObjectTable pprot);

        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);

        // ROT'tan tüm ETABS örneklerini bul
        private List<(cOAPI etabs, string modelName)> GetAllRunningETABSInstances()
        {
            var instances = new List<(cOAPI, string)>();
            
            IRunningObjectTable rot;
            if (GetRunningObjectTable(0, out rot) != 0) return instances;
            
            IEnumMoniker enumMoniker;
            rot.EnumRunning(out enumMoniker);
            enumMoniker.Reset();
            
            IMoniker[] monikers = new IMoniker[1];
            IntPtr fetched = IntPtr.Zero;
            
            IBindCtx bindCtx;
            CreateBindCtx(0, out bindCtx);
            
            while (enumMoniker.Next(1, monikers, fetched) == 0)
            {
                string displayName;
                monikers[0].GetDisplayName(bindCtx, null, out displayName);
                
                if (displayName != null && displayName.Contains("ETABS"))
                {
                    try
                    {
                        object obj;
                        rot.GetObject(monikers[0], out obj);
                        cOAPI etabsObj = obj as cOAPI;
                        if (etabsObj != null)
                        {
                            string modelName = "Bilinmiyor";
                            try { modelName = System.IO.Path.GetFileName(etabsObj.SapModel.GetModelFilename()); } catch { }
                            instances.Add((etabsObj, modelName));
                        }
                    }
                    catch { }
                }
            }
            
            return instances;
        }

        // ETABS seçim dialog'u göster
        private cOAPI ShowETABSSelectionDialog(List<(cOAPI etabs, string modelName)> instances)
        {
            using (Form selectForm = new Form())
            {
                selectForm.Text = "ETABS Seçimi";
                selectForm.Size = new Size(400, 250);
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.MinimizeBox = false;
                
                Label lbl = new Label { Text = "Birden fazla ETABS açık. Hangisine bağlanmak istiyorsunuz?", Location = new Point(20, 20), AutoSize = true };
                selectForm.Controls.Add(lbl);
                
                ListBox lst = new ListBox { Location = new Point(20, 50), Size = new Size(340, 100) };
                for (int i = 0; i < instances.Count; i++)
                    lst.Items.Add($"ETABS #{i + 1}: {instances[i].modelName}");
                lst.SelectedIndex = 0;
                selectForm.Controls.Add(lst);
                
                Button btnOK = new Button { Text = "Bağlan", Location = new Point(100, 165), Width = 80, DialogResult = DialogResult.OK };
                Button btnCancel = new Button { Text = "İptal", Location = new Point(200, 165), Width = 80, DialogResult = DialogResult.Cancel };
                selectForm.Controls.Add(btnOK);
                selectForm.Controls.Add(btnCancel);
                selectForm.AcceptButton = btnOK;
                selectForm.CancelButton = btnCancel;
                
                if (selectForm.ShowDialog() == DialogResult.OK && lst.SelectedIndex >= 0)
                    return instances[lst.SelectedIndex].etabs;
                    
                return null;
            }
        }


        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                lblConnectionStatus.Text = "Aranıyor...";
                lblConnectionStatus.ForeColor = Color.Orange;
                Application.DoEvents();
                
                // Önceki bağlantıyı temizle
                if (_sapModel != null)
                {
                    try { Marshal.ReleaseComObject(_sapModel); } catch { }
                    _sapModel = null;
                }
                if (_myETABSObject != null)
                {
                    try { Marshal.ReleaseComObject(_myETABSObject); } catch { }
                    _myETABSObject = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                
                // YÖNTEM 0: ROT ile tüm ETABS örneklerini bul
                var etabsInstances = GetAllRunningETABSInstances();
                
                if (etabsInstances.Count > 1)
                {
                    // Birden fazla ETABS açık - kullanıcıdan seç
                    _myETABSObject = ShowETABSSelectionDialog(etabsInstances);
                }
                else if (etabsInstances.Count == 1)
                {
                    // Tek ETABS açık - doğrudan bağlan
                    _myETABSObject = etabsInstances[0].etabs;
                }
                
                // YÖNTEM 1: ROT'ta bulunamadıysa Helper.GetObject ile dene
                if (_myETABSObject == null)
                {
                    try
                    {
                        _myHelper = new Helper();
                        _myETABSObject = _myHelper.GetObject("CSI.ETABS.API.ETABSObject");
                    }
                    catch { _myETABSObject = null; }
                }
                
                // YÖNTEM 2: Marshal.GetActiveObject ile dene
                if (_myETABSObject == null)
                {
                    try
                    {
                        object etabsObject = Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
                        _myETABSObject = etabsObject as cOAPI;
                    }
                    catch { _myETABSObject = null; }
                }
                
                if (_myETABSObject != null)
                {
                    _sapModel = _myETABSObject.SapModel;
                    if (_sapModel != null)
                    {
                        lblConnectionStatus.Text = "BAĞLANDI";
                        lblConnectionStatus.ForeColor = Color.Green;
                        try { lblModelName.Text = System.IO.Path.GetFileName(_sapModel.GetModelFilename()); } catch { lblModelName.Text = "Model"; }
                        try
                        {
                            bool isLocked = _sapModel.GetModelIsLocked();
                            lblLockStatus.Text = isLocked ? "🔒 Kilitli" : "🔓 Açık";
                            lblLockStatus.ForeColor = isLocked ? Color.Red : Color.Green;
                        }
                        catch { lblLockStatus.Text = ""; }
                        
                        // Önceki sayfa verilerini temizle
                        _kolonColumnForces.Clear();
                        _kolonFrameAssignments.Clear();
                        _kolonSelectedCombos.Clear();
                        if (pnlKolonSelectedCombos != null) pnlKolonSelectedCombos.Controls.Clear();
                        if (lstKolonCombinations != null) lstKolonCombinations.Items.Clear();
                        if (lblKolonFrameStatus != null) lblKolonFrameStatus.Text = "";
                    }
                    else
                    {
                        lblConnectionStatus.Text = "MODEL YOK";
                        lblConnectionStatus.ForeColor = Color.Orange;
                        MessageBox.Show("ETABS'a bağlanıldı ama açık model bulunamadı.\nLütfen bir model açın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    lblConnectionStatus.Text = "BAĞLANAMADI";
                    lblConnectionStatus.ForeColor = Color.Red;
                    MessageBox.Show("Açık ETABS bulunamadı.\n\nLütfen:\n1. ETABS'ın açık olduğundan emin olun\n2. ETABS'ta bir model yüklenmiş olsun\n3. ETABS Admin modunda çalışıyorsa, bu uygulamayı da Admin olarak çalıştırın", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = "HATA";
                lblConnectionStatus.ForeColor = Color.Red;
                MessageBox.Show("ETABS bağlantı hatası:\n" + ex.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}