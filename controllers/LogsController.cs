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
        private System.Windows.Controls.Button _logsButton;
        private System.Windows.Controls.ListBox _logs;
        
        private bool _isLogsOpen;
        private LogsView _view;
        
        public LogsController(LogsView view)
        {
            _logsButton = view.LogsButton;
            _view = view;
            _logs = view.Logs;
            _isLogsOpen = false;

            AddEventHandlers();
            view.HideLogsPanelOnStartup();
            
            // add some random values to the logs
            for (var i = 100; i < 200; ++i)
                _logs.Items.Add(i + " Hello test");
        }
        
        private void AddEventHandlers()
        {
            _logsButton.Click += OnLogsButtonClick;
        }

        
        
        private void OnLogsButtonClick(object obj, EventArgs args)
        {
            if (_isLogsOpen) _view.CloseLogsPanel();
            else _view.OpenLogsPanel();
            _isLogsOpen = !_isLogsOpen;
        }
    }
}