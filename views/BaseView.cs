using System.Drawing;
using System.Windows.Forms;

namespace LiftSystem.views
{
    public class BaseView : Form
    {
        private readonly int _leftPanelWidth = (int)(Screen.PrimaryScreen.WorkingArea.Width * Constants.LeftPanelWidthPercent);
        private readonly int _panelHeight = Screen.PrimaryScreen.WorkingArea.Height;
        private readonly int _rightPanelWidth =
            (int)(Screen.PrimaryScreen.WorkingArea.Width * Constants.RightPanelWidthPercent);

        private Panel _leftPanel;
        private Panel _rightPanel;
        
        public BaseView()
        {
            WindowState = FormWindowState.Maximized;
            ClientSize = Screen.FromHandle(Handle).WorkingArea.Size - new Size(0, 30);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            
            
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 77);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BaseView
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Name = "BaseView";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button1;
    }
}