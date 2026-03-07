using System;
using System.Drawing;
using System.Windows.Forms;
using CSiAPIv1;

namespace EtabsTools
{
    public class TemelDonesiUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<Panel, int, string, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        public TemelDonesiUI(Form1 form, Func<cSapModel> getSapModel, 
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

            Label header = Form1.CreateHeaderLabel("Temel Donesi");
            mainLayout.Controls.Add(header, 0, 0);

            // --- İÇERİK PANELİ ---
            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlp.Padding = new Padding(15, 5, 15, 5);

            // =============== SOL PANEL (Scrollable Wrapper) ===============
            Panel pnlLeftScroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            RoundedPanel pnlLeft = new RoundedPanel
            {
                Title = "Ayarlar",
                Dock = DockStyle.Top,
                BorderRadius = 20,
                Margin = new Padding(0, 0, 10, 5),
                TitleFont = new Font("Segoe UI", 12, FontStyle.Bold),
                Height = 600
            };
            pnlLeft.Controls.Add(new Label { 
                Text = "Konfigürasyon seçenekleri buraya gelecek.", 
                Location = new Point(20, 50), 
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Italic)
            });

            pnlLeftScroll.Controls.Add(pnlLeft);
            tlp.Controls.Add(pnlLeftScroll, 0, 0);

            // =============== SAĞ PANEL ===============
            Label lblContent = new Label { 
                Text = "Bu modül geliştirme aşamasındadır.", 
                Dock = DockStyle.Fill, 
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.Gray
            };
            tlp.Controls.Add(lblContent, 1, 0);

            mainLayout.Controls.Add(tlp, 0, 1);

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
    }
}
