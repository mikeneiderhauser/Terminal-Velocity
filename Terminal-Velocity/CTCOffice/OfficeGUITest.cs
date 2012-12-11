using System.Windows.Forms;

namespace CTCOffice
{
    public partial class OfficeGUITest : UserControl
    {
        public OfficeGUITest(CTCOfficeGUI ctc, RequestFrame red, RequestFrame green, TestingControls tc)
        {
            InitializeComponent();
            _panelCTC.Controls.Clear();
            _panelCTC.Controls.Add(ctc);

            _panelRequestRed.Controls.Clear();
            _panelRequestRed.Controls.Add(red);

            _panelRequestGreen.Controls.Clear();
            _panelRequestGreen.Controls.Add(green);

            _panelTestingControls.Controls.Clear();
            _panelTestingControls.Controls.Add(tc);
        }
    }
}