using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LiftSystem.DTO;
using LiftSystem.Model;
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
            LogsEventEmitter.Instance.AddOnLog((id, msg) =>
                _logs.Items.Insert(0, $"{id} {msg}"));
            
            foreach (var log in new LiftModel().GetAllLogs())
            {
                var tmp = (Log)log;
                _logs.Items.Add($"{tmp.Id} {tmp.Message}");
            }
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