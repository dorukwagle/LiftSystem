using System.Drawing;
using System.Windows.Forms;

namespace LiftSystem.views
{
    public class BaseView : Form
    {
        private RadioButton _radioButton;
        private readonly int _leftPanelWidth = (int)(Screen.PrimaryScreen.WorkingArea.Width * Constants.LeftPanelWidthPercent);
        private readonly int _panelHeight = Screen.PrimaryScreen.WorkingArea.Height;
        private readonly int _rightPanelWidth =
            (int)(Screen.PrimaryScreen.WorkingArea.Width * Constants.RightPanelWidthPercent);

        private Panel _leftPanel;
        private Panel _rightPanel;
        
        public BaseView()
        {
            WindowState = FormWindowState.Normal;
            ClientSize = Screen.PrimaryScreen.WorkingArea.Size;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            
            base.Text = "Lift System";
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var basePanel = new Panel();
            basePanel.Dock = DockStyle.Fill;

            _leftPanel = new Panel();
            _leftPanel.Dock = DockStyle.Left;
            _leftPanel.Width = _leftPanelWidth;
            basePanel.Controls.Add(_leftPanel);
            

            _rightPanel = new Panel();
            _rightPanel.Dock = DockStyle.Right;
            _rightPanel.Width = _rightPanelWidth;
            basePanel.Controls.Add(_rightPanel);
            
            Controls.Add(basePanel);
        }

        public int PanelHeight => _panelHeight;
        public int LeftPanelWidth => _leftPanelWidth;
        public int RightPanelWidth => _rightPanelWidth;

        public Panel LeftPanel => _leftPanel;
        public Panel RightPanel => _rightPanel;
    }
}