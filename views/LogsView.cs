using System;
using System.Drawing;
using System.Windows.Forms;

namespace LiftSystem.views
{
    public class LogsView
    {
        private Button _logButton;
        private Panel _logPanel;
        private ListBox _logs;

        private readonly Font _font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
        
        public LogsView(Control parent, int maxWidth, int maxHeight)
        {
            var logsHeight = (int) (maxHeight * 0.93);
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;

            _logButton = new Button();
            _logButton.Text = Constants.LogsButtonShowText;
            _logButton.Font = _font;
            _logButton.Dock = DockStyle.Left;
            _logButton.Width = (int) (maxWidth * 0.3);
            panel.Controls.Add(_logButton);

            _logPanel = new Panel();
            _logPanel.Width = maxWidth;
            _logPanel.Height = logsHeight;
            _logPanel.Dock = DockStyle.Bottom;
            _logPanel.BackColor = Color.Chartreuse;
            panel.Controls.Add(_logPanel);

            _logs = new ListBox();
            _logs.ItemHeight = 25;
            _logs.Height = logsHeight;
            _logs.Dock = DockStyle.Bottom;
            _logs.Font = _font;
            
            _logPanel.Controls.Add(_logs);
            
            parent.Controls.Add(panel);
        }

        public Button LogsButton => _logButton;
        public Panel LogsPanel => _logPanel;
        public ListBox Logs => _logs;
    }
}