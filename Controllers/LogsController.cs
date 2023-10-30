using System;
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
            
            // subscribe the log event
            LogsEventEmitter.AddOnLog((id, msg, created) =>
                _logs.Dispatcher.Invoke(() => _logs.Items.Insert(0, $"{id} {msg} :: ({created})")));
            
            foreach (var log in new LiftModel().GetAllLogs())
            {
                var tmp = (Log)log;
                _logs.Items.Add($"{tmp.Id} {tmp.Message} :: ({tmp.Created})");
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