using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Text;
using CSiAPIv1;

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
        public bool EnableCenterAnimation { get; set; } = false; // Merkezden büyüme

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

            int oldW = this.Width;
            int oldH = this.Height;

            int nextW = currentW;
            int nextH = currentH;

            if (Math.Abs(currentW - targetW) <= _step) nextW = targetW;
            else nextW += (currentW < targetW) ? _step : -_step;

            if (Math.Abs(currentH - targetH) <= _step) nextH = targetH;
            else nextH += (currentH < targetH) ? _step : -_step;

            this.Width = nextW;
            this.Height = nextH;

            if (EnableCenterAnimation)
            {
                this.Left -= (this.Width - oldW) / 2;
                this.Top -= (this.Height - oldH) / 2;
            }

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
            this.Size = new Size(160, 350); // REVERTED
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
                Location = new Point(80, 5), // REVERTED (110 -> 80)
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
                    Location = new Point(80, y), // REVERTED (110 -> 80)
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
        // ---------------------------------------------------------
        // STATIC HEADER HELPER (Standart Başlık Oluşturucu)
        // ---------------------------------------------------------
        public static Label CreateHeaderLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private cSapModel _sapModel;
        private cHelper _myHelper;
        private cOAPI _myETABSObject;

        private Panel pnlHeader;
        private TabControl mainTabControl;

        private Label lblModelName;
        private Label lblLockStatus;
        private Label lblConnectionStatus;

        // Tasarım Spektrumu UI (ayrı modülden)
        private TasarimSpektrumuUI _tasarimSpektrumuUI;

        // Göreli Kat Ötelemesi UI (ayrı modülden)
        private GoreliKatOtelemesiUI _goreliKatUI;

        // Artırım Hesabı UI (ayrı modülden)
        private ArtirimHesabiUI _artirimHesabiUI;

        // Kolon Eksenel Yük Kontrolü UI (ayrı modülden)
        private KolonEksenelYukUI _kolonEksenelYukUI;

        private Color colorBackground = Color.FromArgb(240, 244, 248);
        private Color colorHeader = Color.White;

        // İkinci Mertebe Etkileri UI (ayrı modülden)
        private TabPage tabIkinciMertebe;
        private IkinciMertebeUI _ikinciMertebeUI;

        // YENİ MODÜLLER (ETABS)
        private KirisKesmeGuvenligiUI _kirisKesmeUI;
        private KirisEksenelYukUI _kirisEksenelUI;
        private KolonPmmUI _kolonPmmUI;

        // YENİ MODÜLLER (DONE)
        private KolonDonesiUI _kolonDonesiUI;
        private PerdeDonesiUI _perdeDonesiUI;
        private KirisDonesiUI _kirisDonesiUI;
        private DosemeDonesiUI _dosemeDonesiUI;
        private BapDonesiUI _bapDonesiUI;
        private TemelDonesiUI _temelDonesiUI;

        // Data Lists - Göreli Kat ve İkinci Mertebe tarafından kullanılıyor
        private List<StoryData> _storyDataList = new List<StoryData>();
        private List<MassData> _massDataList = new List<MassData>();

        
        public Form1()
        {
            InitializeCustomUI();                    
            this.ResizeRedraw = true; // Yeniden boyutlandırmada çizimi tazele
        }

        private void InitializeCustomUI()
        {
            this.Text = "ETABS Mühendislik Asistanı";
            this.Size = new Size(1300, 900);
            this.MinimumSize = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorBackground;
            this.Font = new Font("Segoe UI", 9f);

            // 1. ÜST PANEL
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = colorHeader,
                Padding = new Padding(20),
                Width = this.Width // Width'i baştan set et ki Anchor doğru çalışsın
            };
            pnlHeader.Paint += (s, e) => { e.Graphics.DrawLine(new Pen(Color.LightGray), 0, 79, pnlHeader.Width, 79); };

            // Bağlan Tuşu
            var btnConnect = new SmoothButton
            {
                Text = "ETABS'a Bağlan",
                BaseColor = Color.FromArgb(255, 179, 186),
                Location = new Point(20, 20),
                Size = new Size(150, 40),
                BorderRadius = 20,
                EnableCenterAnimation = true
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

            // 0: Dashboard
            TabPage pageHome = new TabPage("Dashboard");
            pageHome.BackColor = colorBackground;
            InitializeDashboardResponsive(pageHome);
            mainTabControl.TabPages.Add(pageHome);

            // --- ETABS ASİSTANI (1-9) ---

            // 1: Tasarım Spektrumu
            TabPage pageSpectrum = new TabPage("Tasarım Spektrumu");
            pageSpectrum.Tag = 1;
            pageSpectrum.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageSpectrum);
            _tasarimSpektrumuUI = new TasarimSpektrumuUI(this, (i) => CreateNavigationPanel(i, "ETABS"), GoToPage, colorBackground);
            _tasarimSpektrumuUI.Initialize(pageSpectrum);

            // 2: Artırım Hesabı
            TabPage pageArtirim = new TabPage("Artırım Hesabı");
            pageArtirim.Tag = 2;
            pageArtirim.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageArtirim);
            _artirimHesabiUI = new ArtirimHesabiUI(
                this, 
                () => _sapModel, 
                (i) => CreateNavigationPanel(i, "ETABS"), 
                GoToPage, 
                colorBackground,
                () => _tasarimSpektrumuUI.SavedSpectrumResult,
                () => _tasarimSpektrumuUI.TxtSDS,
                () => _tasarimSpektrumuUI.TxtI,
                () => _storyDataList,
                FetchStoryData
            );
            _artirimHesabiUI.Initialize(pageArtirim);

            // 3: Göreli Kat Ötelemesi
            TabPage pageGoreliKat = new TabPage("Göreli Kat Ötelemesi Tahkiki");
            pageGoreliKat.Tag = 3;
            pageGoreliKat.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageGoreliKat);
            _goreliKatUI = new GoreliKatOtelemesiUI(this, () => _sapModel, (p, i) => CreateNavigationPanel(i, "ETABS"), GoToPage, colorBackground);
            _goreliKatUI.Initialize(pageGoreliKat, _tasarimSpektrumuUI.TxtSDS, _tasarimSpektrumuUI.TxtSD1);

            // 4: İkinci Mertebe
            tabIkinciMertebe = new TabPage("İkinci Mertebe Etkileri Tahkiki");
            tabIkinciMertebe.Tag = 4;
            tabIkinciMertebe.BackColor = colorBackground;
            mainTabControl.TabPages.Add(tabIkinciMertebe);
            _ikinciMertebeUI = new IkinciMertebeUI(this, () => _sapModel, (p, i) => CreateNavigationPanel(i, "ETABS"), GoToPage, colorBackground);
            _ikinciMertebeUI.Initialize(tabIkinciMertebe);

            // 5: Kolon Eksenel Yük
            TabPage pageKolonEksenel = new TabPage("Kolon Eksenel Yük Tahkiki");
            pageKolonEksenel.Tag = 5;
            pageKolonEksenel.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKolonEksenel);
            _kolonEksenelYukUI = new KolonEksenelYukUI(this, () => _sapModel, FetchStoryData, () => _storyDataList, (p, i) => CreateNavigationPanel(i, "ETABS"), GoToPage, colorBackground);
            _kolonEksenelYukUI.Initialize(pageKolonEksenel);

            // 6: Perde Kesme Kontrolü
            AddContentPage("Perde Kesme Kontrolü", 6, "ETABS");

            // 7: Kiriş Kesme Güvenliği
            TabPage pageKirisKesme = new TabPage("Kiriş Kesme Güvenliği Kontrolü");
            pageKirisKesme.Tag = 7;
            pageKirisKesme.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKirisKesme);
            _kirisKesmeUI = new KirisKesmeGuvenligiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _kirisKesmeUI.Initialize(pageKirisKesme);

            // 8: Kiriş Eksenel Yük
            TabPage pageKirisEksenel = new TabPage("Kiriş Eksenel Yük Kontrolü");
            pageKirisEksenel.Tag = 8;
            pageKirisEksenel.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKirisEksenel);
            _kirisEksenelUI = new KirisEksenelYukUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _kirisEksenelUI.Initialize(pageKirisEksenel);

            // 9: Kolon PMM
            TabPage pageKolonPmm = new TabPage("Kolon PMM Kontrolü");
            pageKolonPmm.Tag = 9;
            pageKolonPmm.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKolonPmm);
            _kolonPmmUI = new KolonPmmUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _kolonPmmUI.Initialize(pageKolonPmm);

            // --- DONE ASİSTANI (10-15) ---

            // 10: Kolon Donesi
            TabPage pageKolonDonesi = new TabPage("Kolon Donesi");
            pageKolonDonesi.Tag = 10;
            pageKolonDonesi.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKolonDonesi);
            _kolonDonesiUI = new KolonDonesiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _kolonDonesiUI.Initialize(pageKolonDonesi);

            // 11: Perde Donesi
             TabPage pagePerdeDonesi = new TabPage("Perde Donesi");
            pagePerdeDonesi.Tag = 11;
            pagePerdeDonesi.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pagePerdeDonesi);
            _perdeDonesiUI = new PerdeDonesiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _perdeDonesiUI.Initialize(pagePerdeDonesi);

            // 12: Kiriş Donesi
             TabPage pageKirisDonesi = new TabPage("Kiriş Donesi");
            pageKirisDonesi.Tag = 12;
            pageKirisDonesi.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageKirisDonesi);
            _kirisDonesiUI = new KirisDonesiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _kirisDonesiUI.Initialize(pageKirisDonesi);

            // 13: Döşeme Donesi
             TabPage pageDosemeDonesi = new TabPage("Döşeme Donesi");
            pageDosemeDonesi.Tag = 13;
            pageDosemeDonesi.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageDosemeDonesi);
            _dosemeDonesiUI = new DosemeDonesiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _dosemeDonesiUI.Initialize(pageDosemeDonesi);

            // 14: BAP Donesi
             TabPage pageBapDonesi = new TabPage("BAP Donesi");
            pageBapDonesi.Tag = 14;
            pageBapDonesi.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageBapDonesi);
            _bapDonesiUI = new BapDonesiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _bapDonesiUI.Initialize(pageBapDonesi);

            // 15: Temel Donesi
             TabPage pageTemelDonesi = new TabPage("Temel Donesi");
            pageTemelDonesi.Tag = 15;
            pageTemelDonesi.BackColor = colorBackground;
            mainTabControl.TabPages.Add(pageTemelDonesi);
            _temelDonesiUI = new TemelDonesiUI(this, () => _sapModel, (p, i, c) => CreateNavigationPanel(i, c), GoToPage, colorBackground);
            _temelDonesiUI.Initialize(pageTemelDonesi);

            this.Controls.Add(mainTabControl);
            this.Controls.Add(pnlHeader);
        }

        // ---------------------------------------------------------
        // RESPONSIVE DASHBOARD (DÜZELTİLMİŞ)
        // ---------------------------------------------------------
        // ---------------------------------------------------------
        // RESPONSIVE DASHBOARD (YENİ TASARIM)
        // ---------------------------------------------------------
        private void InitializeDashboardResponsive(TabPage page)
        {
            // Ana Düzen: TableLayoutPanel
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 1;
            tlp.RowCount = 2; // Başlık kaldırıldı, sadece 2 satır: Başlık ve İçerik
            
            // 0: Mühendislik Asistanı Başlığı (15%) - Altında çizgi yok
            // 1: Alt Başlıklar ve İçerik (85%)
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); 
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 85F)); 

            // -- 1. "Mühendislik Asistanı" Başlığı --
            Label lblMainTitle = new Label
            {
                Text = "Mühendislik Asistanı",
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Segoe UI Light", 28, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Fill,
                AutoSize = false
            };
            tlp.Controls.Add(lblMainTitle, 0, 0);

            // -- 2. Üç Sütunlu Yapı (ETABS Asistanı | Çizgi | Done Asistanı) --
            TableLayoutPanel tlpContent = new TableLayoutPanel();
            tlpContent.Dock = DockStyle.Fill;
            tlpContent.ColumnCount = 3;
            tlpContent.RowCount = 2;
            // Sol: %49.5, Çizgi: 2px, Sağ: %49.5
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); 
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 2F)); // Dikey çizgi için
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); 

            // Başlık Satırı
            tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            // Butonlar Satırı
            tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Sol Başlık
            Label lblEtabsTitle = new Label
            {
                Text = "ETABS Asistanı",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 22, FontStyle.Regular), // (28+16)/2 = 22
                ForeColor = Color.DimGray,
                Dock = DockStyle.Fill
            };
            tlpContent.Controls.Add(lblEtabsTitle, 0, 0);

            // Ortadaki Dikey Çizgi (Başlık satırından sona kadar)
            Panel vSeparator = new Panel
            {
                Width = 2,
                Dock = DockStyle.Fill,
                BackColor = Color.LightGray,
                Margin = new Padding(0, 10, 0, 10)
            };
            tlpContent.Controls.Add(vSeparator, 1, 0);
            tlpContent.SetRowSpan(vSeparator, 2); // Hem başlık hem içerik boyunca uzansın

            // Sağ Başlık
            Label lblDoneTitle = new Label
            {
                Text = "Done Asistanı",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 22, FontStyle.Regular), // (28+16)/2 = 22
                ForeColor = Color.DimGray,
                Dock = DockStyle.Fill
            };
            tlpContent.Controls.Add(lblDoneTitle, 2, 0);

            // -- Sol Panel Butonları (ETABS) --
            TableLayoutPanel tlpLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(20)
            };
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            // 5 satır eşit yükseklik
            for (int i = 0; i < 5; i++) tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            AddButtonToGrid(tlpLeft, CreateDashButton("Tasarım Spektrumu", 1, tabColors[1]), 0, 0);
            AddButtonToGrid(tlpLeft, CreateDashButton("Artırım Hesabı", 2, tabColors[2]), 0, 1);
            AddButtonToGrid(tlpLeft, CreateDashButton("Göreli Kat Ötelemesi", 3, tabColors[3]), 1, 0);
            AddButtonToGrid(tlpLeft, CreateDashButton("İkinci Mertebe", 4, tabColors[4]), 1, 1);
            AddButtonToGrid(tlpLeft, CreateDashButton("Kolon Eksenel Yük", 5, tabColors[5]), 2, 0);
            AddButtonToGrid(tlpLeft, CreateDashButton("Perde Kesme Kontrolü", 6, tabColors[6]), 2, 1);
            AddButtonToGrid(tlpLeft, CreateDashButton("Kiriş Kesme Güvenliği", 7, tabColors[7]), 3, 0);
            AddButtonToGrid(tlpLeft, CreateDashButton("Kiriş Eksenel Yük", 8, tabColors[8]), 3, 1);
            AddButtonToGrid(tlpLeft, CreateDashButton("Kolon PMM Kontrolü", 9, tabColors[9]), 4, 0);

            tlpContent.Controls.Add(tlpLeft, 0, 1);

            // -- Sağ Panel Butonları (DONE) --
            // Sol tarafla hizalı olması için burada da 5 satır oluşturuyoruz
            TableLayoutPanel tlpRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5, 
                Padding = new Padding(20)
            };
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            // 5 satır eşit yükseklik
            for (int i = 0; i < 5; i++) tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            AddButtonToGrid(tlpRight, CreateDashButton("Kolon Donesi", 10, tabColors[10]), 0, 0);
            AddButtonToGrid(tlpRight, CreateDashButton("Perde Donesi", 11, tabColors[11]), 0, 1);
            AddButtonToGrid(tlpRight, CreateDashButton("Kiriş Donesi", 12, tabColors[12]), 1, 0);
            AddButtonToGrid(tlpRight, CreateDashButton("Döşeme Donesi", 13, tabColors[13]), 1, 1);
            AddButtonToGrid(tlpRight, CreateDashButton("BAP Donesi", 14, tabColors[14]), 2, 0);
            AddButtonToGrid(tlpRight, CreateDashButton("Temel Donesi", 15, tabColors[15]), 2, 1);

            tlpContent.Controls.Add(tlpRight, 2, 1);

            tlp.Controls.Add(tlpContent, 0, 1);
            page.Controls.Add(tlp);
        }

        private void AddButtonToGrid(TableLayoutPanel grid, SmoothButton btn, int row, int col)
        {
            // Panel wrapper ile Anchor=None kullanımı, butonun boyut değişiminde merkezde kalmasını sağlar.
            // Bu sayede "merkezden büyüme" efekti doğal olarak gerçekleşir.
            Panel wrapper = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            wrapper.Controls.Add(btn);
            grid.Controls.Add(wrapper, col, row);
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
                Anchor = AnchorStyles.None, 
                EnableCenterAnimation = true // Merkezden büyüme aktif (Layout ile çakışmaması için Wrapper içinde serbest)
            };
            btn.Click += (s, e) => { GoToPage((int)((Button)s).Tag); };
            return btn;
        }











        // Story Definitions tablosundan kat verilerini çeker
        private void FetchStoryData()
        {
            _storyDataList.Clear();
            
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

                _storyDataList.Add(new StoryData
                {
                    Name = storyName,
                    Height = height / 1000.0, 
                    Elevation = elevation / 1000.0,
                    IsBodrum = false 
                });
            }
        }

        // Mass Summary tablosundan kütle verilerini çeker
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

                _massDataList.Add(new MassData
                {
                    Story = story,
                    Mass = massX,
                    Weight = massX * 9.81 // Ton -> kN
                });
            }
        }



        private void AddContentPage(string title, int index, string context)
        {
            // int tabIndex = mainTabControl.TabPages.Count; // Manuel index kullanıyoruz
            TabPage p = new TabPage(title);
            p.BackColor = colorBackground;
            p.Tag = index;

            // Ana layout: içerik + navigasyon
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // Başlık - Standardize edildi
            Label lbl = CreateHeaderLabel(title);
            mainLayout.Controls.Add(lbl, 0, 0);

            Panel contentPanel = new Panel { Dock = DockStyle.Fill };
            // Placeholder içerik
            Label lblContent = new Label { 
                Text = "Bu modül geliştirme aşamasındadır.", 
                Dock = DockStyle.Fill, 
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.Gray 
            };
            contentPanel.Controls.Add(lblContent);
            mainLayout.Controls.Add(contentPanel, 0, 1);

            // Navigasyon paneli
            p.VisibleChanged += (s, e) => {
                if (p.Visible && mainLayout.Controls.Count < 3)
                {
                    Panel navPanel = CreateNavigationPanel((int)p.Tag, context);
                    mainLayout.Controls.Add(navPanel, 0, 2);
                }
            };

            p.Controls.Add(mainLayout);
            mainTabControl.TabPages.Add(p);
        }

        // Sekme renklerini tanımla (dashboard ile aynı)
        private readonly Color[] tabColors = new Color[]
        {
            Color.FromArgb(240, 244, 248), // 0: Dashboard (kullanılmaz)
            // ETABS ASISTANI
            Color.FromArgb(255, 159, 168), // 1: Tasarım Spektrumu
            Color.FromArgb(255, 220, 180), // 2: Artırım Hesabı
            Color.FromArgb(159, 219, 255), // 3: Göreli Kat Ötelemesi
            Color.FromArgb(255, 236, 159), // 4: İkinci Mertebe
            Color.FromArgb(255, 203, 159), // 5: Kolon Eksenel Yük Kontrolü
            Color.FromArgb(219, 190, 255), // 6: Perde Kesme Kontrolü
            Color.FromArgb(255, 180, 200), // 7: Kiriş Kesme Güvenliği
            Color.FromArgb(200, 255, 200), // 8: Kiriş Eksenel Yük
            Color.FromArgb(200, 200, 255), // 9: Kolon PMM
            // DONE ASISTANI
            Color.FromArgb(255, 159, 168), // 10: Kolon Donesi
            Color.FromArgb(255, 220, 180), // 11: Perde Donesi
            Color.FromArgb(159, 219, 255), // 12: Kiriş Donesi
            Color.FromArgb(255, 236, 159), // 13: Döşeme Donesi
            Color.FromArgb(255, 203, 159), // 14: BAP Donesi
            Color.FromArgb(219, 190, 255)  // 15: Temel Donesi
        };

        private Panel CreateNavigationPanel(int currentTabIndex, string context = "ETABS")
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
                "Ana Menü",                       // 0
                "Tasarım Spektrumu",              // 1
                "Artırım Hesabı",                 // 2
                "Göreli Kat Ötelemesi",           // 3
                "İkinci Mertebe",                 // 4
                "Kolon Eksenel Yük",              // 5
                "Perde Kesme",                    // 6
                "Kiriş Kesme",                    // 7
                "Kiriş Eksenel",                  // 8
                "Kolon PMM",                      // 9
                "Kolon Donesi",                   // 10
                "Perde Donesi",                   // 11
                "Kiriş Donesi",                   // 12
                "Döşeme Donesi",                  // 13
                "BAP Donesi",                     // 14
                "Temel Donesi"                    // 15
            };

            // Görünecek butonları belirle
            List<int> visibleIndices = new List<int> { 0 }; // Ana Menü her zaman var

            if (context == "ETABS")
            {
                 visibleIndices.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            else if (context == "DONE")
            {
                 visibleIndices.AddRange(new int[] { 10, 11, 12, 13, 14, 15 });
            }

            // Mevcut sayfayı çıkar
            visibleIndices.Remove(currentTabIndex);

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
                    Margin = new Padding(3, 0, 3, 0),
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
                selectForm.Size = new Size(600, 350); // Height increased to accommodate buttons
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.MinimizeBox = false;
                
                Label lbl = new Label { Text = "Birden fazla ETABS açık. Hangisine bağlanmak istiyorsunuz?", Location = new Point(20, 20), AutoSize = true };
                selectForm.Controls.Add(lbl);
                
                ListBox lst = new ListBox { Location = new Point(20, 50), Size = new Size(550, 200) };
                for (int i = 0; i < instances.Count; i++)
                    lst.Items.Add($"ETABS #{i + 1}: {instances[i].modelName}");
                lst.SelectedIndex = 0;
                selectForm.Controls.Add(lst);
                
                Button btnOK = new Button { Text = "Bağlan", Location = new Point(160, 260), Width = 100, DialogResult = DialogResult.OK };
                Button btnCancel = new Button { Text = "İptal", Location = new Point(280, 260), Width = 100, DialogResult = DialogResult.Cancel };
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

                    }
                    else
                    {
                        lblConnectionStatus.Text = "MODEL YOK";
                        lblConnectionStatus.ForeColor = Color.Orange;
                        ToastForm.ShowToast("ETABS'a bağlanıldı ama açık model bulunamadı. Lütfen bir model açın.", this, 2000);
                    }
                }
                else
                {
                    lblConnectionStatus.Text = "BAĞLANAMADI";
                    lblConnectionStatus.ForeColor = Color.Red;
                    ToastForm.ShowToast("Açık ETABS bulunamadı. Lütfen ETABS'ın açık olduğundan emin olun.", this, 2000);
                }
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = "HATA";
                lblConnectionStatus.ForeColor = Color.Red;
                ToastForm.ShowToast("ETABS bağlantı hatası: " + ex.Message, this, 2000);
            }
        }
    }

    // ---------------------------------------------------------
    // TOAST NOTIFICATION (Geçici Bildirim Paneli)
    // ---------------------------------------------------------
    public class ToastForm : Form
    {
        private Timer _animTimer;
        private int _borderRadius = 20;
        private int _waitCounter = 0;
        private int _waitLimit = 100; // Yaklaşık 3 saniye bekleme (30ms * 100)

        // Animasyon durumları
        private bool _isFadingIn = true;
        private bool _isWaiting = false;
        private bool _isFadingOut = false;

        public ToastForm(string message, int durationMs = 3000)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(300, 60);
            this.BackColor = Color.White; // Arka plan BEYAZ
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Opacity = 0; // Başlangıçta görünmez
            _waitLimit = durationMs / 30; // 30ms her kare

            Label lblMsg = new Label
            {
                Text = message,
                ForeColor = Color.SeaGreen, // Yazı rengi YEŞİL (Çerçeve ile uyumlu)
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblMsg);

            _animTimer = new Timer { Interval = 30 }; // 30ms her kare
            _animTimer.Tick += OnAnimTick;
            _animTimer.Start();
        }

        private void OnAnimTick(object sender, EventArgs e)
        {
            if (_isFadingIn)
            {
                // Yavaş yavaş görünürlük artıyor
                this.Opacity += 0.05;
                if (this.Opacity >= 1.0)
                {
                    this.Opacity = 1.0;
                    _isFadingIn = false;
                    _isWaiting = true;
                    _waitCounter = 0;
                }
            }
            else if (_isWaiting)
            {
                // Bekleme süresi
                _waitCounter++;
                if (_waitCounter >= _waitLimit)
                {
                    _isWaiting = false;
                    _isFadingOut = true;
                }
            }
            else if (_isFadingOut)
            {
                // Yavaş yavaş kayboluyor
                this.Opacity -= 0.05;
                if (this.Opacity <= 0)
                {
                    this.Opacity = 0;
                    _animTimer.Stop();
                    this.Close();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = this.ClientRectangle;
            rect.Width -= 1; rect.Height -= 1;

            using (GraphicsPath path = GetRoundPath(rect, _borderRadius))
            {
                this.Region = new Region(path);
                // Çerçeve rengi YEŞİL
                using (Pen pen = new Pen(Color.SeaGreen, 3f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void ShowToast(string message, Form parent, int durationMs = 3000)
        {
            if (parent == null) return;
            ToastForm toast = new ToastForm(message, durationMs);
            // SAĞ ÜST KÖŞE KONUMLANDIRMA
            // Formun sağ kenarından toast genişliği + biraz boşluk (30px) kadar içeride
            int x = parent.Location.X + parent.Width - toast.Width - 30;
            // Formun üst kenarından 60px aşağıda
            int y = parent.Location.Y + 60;
            
            toast.Location = new Point(x, y);
            toast.Show(parent);
        }
    }
}