

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LiftSystem.views
{
    public class LogsView
    {
        private Button _logButton;
        private StackPanel _logPanel;
        private ListBox _logs;
        
        private Storyboard _storyboard;
        private DoubleAnimation _animation;
        private const float AnimationDuration = 0.6f; //sec

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
            
            SetupAnimation();
        }
        
        public void HideLogsPanelOnStartup()
        {
            _animation.Duration = TimeSpan.FromMilliseconds(10);
            _animation.From = 0;
            _animation.To = -_logsPanelWidth * 2;
            _storyboard.Begin();
        }

        public void OpenLogsPanel()
        {
            _logButton.IsEnabled = false;
            _animation.Duration = TimeSpan.FromSeconds(AnimationDuration);
            _animation.From = -_logs.ActualWidth;
            _animation.To = 0;
            
            _storyboard.Begin();
            _logButton.Content = Constants.LogsButtonHideText;
        }

        public void CloseLogsPanel()
        {
            _logButton.IsEnabled = false;
            _animation.Duration = TimeSpan.FromSeconds(AnimationDuration);
            _animation.From = 0;
            _animation.To = -_logs.ActualWidth;
            
            _storyboard.Begin();
            _logButton.Content = Constants.LogsButtonShowText;
        }
        
        private void SetupAnimation()
        {
            _storyboard = new Storyboard();
            _animation = new DoubleAnimation();
            
            _logs.RenderTransform = new TranslateTransform();
            
            Storyboard.SetTarget(_animation, _logs);
            Storyboard.SetTargetProperty(_animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            _storyboard.Children.Add(_animation);

            // Add completed event to enable button
            _storyboard.Completed += (sender, e) =>
            {
                _logButton.IsEnabled = true;
            };
        }

        public Button LogsButton => _logButton;
        public StackPanel LogsPanel => _logPanel;
        public ListBox Logs => _logs;
        public int LogsPanelWidth => _logsPanelWidth;
    }
}