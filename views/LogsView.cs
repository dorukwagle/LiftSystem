

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LiftSystem.views
{
    public class LogsView
    {
        private Button _logButton;
        private StackPanel _logPanel;
        private ListBox _logs;

        private readonly int _logsPanelWidth;
        
        public LogsView(StackPanel parent, int maxWidth, int maxHeight)
        {
            _logsPanelWidth = maxWidth;
            const float logsHeightPercent = 0.95f;
            
            var logsHeight = (int) (maxHeight * logsHeightPercent);
            var panel = new StackPanel();

            var dock = new StackPanel();
            dock.Orientation = Orientation.Horizontal;
            dock.Margin = new Thickness(5);
            _logButton = new Button();
            _logButton.Content = Constants.LogsButtonShowText;
            _logButton.FontWeight = FontWeights.Bold;
            _logButton.FontSize = 16;
            _logButton.Width = (int) (maxWidth * 0.3);
            _logButton.Height = (int)(maxHeight * (1 - logsHeightPercent));
            dock.Children.Add(_logButton);
            panel.Children.Add(dock);

            _logPanel = new StackPanel();
            panel.Children.Add(_logPanel);

            _logs = new ListBox();
            _logs.FontSize = 16;
            _logs.Name = "logs" + System.Guid.NewGuid().ToString("N");
            
            _logs.FontWeight = FontWeights.Bold;
            _logs.Height = logsHeight;
            
            _logPanel.Children.Add(_logs);
            
            parent.Children.Add(panel);
        }

        public Button LogsButton => _logButton;
        public StackPanel LogsPanel => _logPanel;
        public ListBox Logs => _logs;
        public int LogsPanelWidth => _logsPanelWidth;
    }
}