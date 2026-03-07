using System;
using System.Drawing;
using System.Windows.Forms;
using CSiAPIv1;

namespace EtabsTools
{
    public class PerdeDonesiUI
    {
        private Form1 _form;
        private Func<cSapModel> _getSapModel;
        private Func<Panel, int, string, Panel> _createNavigationPanel;
        private Action<int> _goToPage;
        private Color _colorBackground;

        public PerdeDonesiUI(Form1 form, Func<cSapModel> getSapModel, 
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

            Label header = Form1.CreateHeaderLabel("Perde Donesi");
            mainLayout.Controls.Add(header, 0, 0);

            Label lblContent = new Label { 
                Text = "Bu modül geliştirme aşamasındadır.", 
                Dock = DockStyle.Fill, 
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.Gray
            };
            mainLayout.Controls.Add(lblContent, 0, 1);

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
