using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LiftSystem.views;

namespace LiftSystem.controllers
{
    public class LogsController
    {
        private int _panelWidth;
        private System.Windows.Controls.Button _logsButton;
        private System.Windows.Controls.ListBox _logs;

        private Storyboard _storyboard;
        private DoubleAnimation _animation;
        private bool _isLogsOpen;
        private const float AnimationDuration = 0.6f; //sec
        
        public LogsController(LogsView view)
        {
            _logsButton = view.LogsButton;
            _logs = view.Logs;
            _panelWidth = view.LogsPanelWidth;

            AddEventHandlers();
            SetupAnimation();
            HideLogsPanel();
            
            // add some random values to the logs
            for (var i = 100; i < 200; ++i)
                _logs.Items.Add(i + " Hello test");
        }
        
        private void AddEventHandlers()
        {
            _logsButton.Click += OnLogsButtonClick;
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
                _logsButton.IsEnabled = true;
            };
        }
        
        private void OnLogsButtonClick(object obj, EventArgs args)
        {
            _animation.Duration = TimeSpan.FromSeconds(AnimationDuration);
            
            _logsButton.IsEnabled = false;

            if (_isLogsOpen)
            {
                _animation.From = 0;
                _animation.To = -_logs.ActualWidth;
            }
            else
            {
                _animation.From = -_logs.ActualWidth;
                _animation.To = 0;
            }
            
            _storyboard.Begin();
            
            _logsButton.Content = _isLogsOpen ? Constants.LogsButtonShowText : Constants.LogsButtonHideText;
            _isLogsOpen = !_isLogsOpen;
        }

        private void HideLogsPanel()
        {
            _animation.Duration = TimeSpan.FromMilliseconds(10);
            _animation.From = 0;
            _animation.To = -_panelWidth * 2;
            _storyboard.Begin();
        }
        
    }
}