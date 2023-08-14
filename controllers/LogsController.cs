// using System;
// using System.Threading;
// using LiftSystem.views;
//
// namespace LiftSystem.controllers
// {
//     public class LogsController
//     {
//         private LogsView _view;
//         private System.Windows.Forms.Panel _panel;
//         private int _panelWidth;
//         private System.Windows.Forms.Button _logsButton;
//         private System.Windows.Forms.ListBox _logs;
//
//         private bool _isLogsOpen;
//         private readonly int _panelAnimationStep = 20;
//         private const int AnimationDelay = 1; // millis
//
//         public LogsController(LogsView view)
//         {
//             _view = view;
//             _panel = view.LogsPanel;
//             _panelWidth = view.LogsPanelWidth;
//             _logsButton = view.LogsButton;
//             _logs = view.Logs;
//
//             HideLogsPanel();
//             AddEventHandlers();
//             
//             // add some random values to the logs
//             for (var i = 100; i < 200; ++i)
//                 _logs.Items.Add(i + " Hello test");
//         }
//         
//         private void AddEventHandlers()
//         {
//             _logsButton.Click += OnLogsButtonClick;
//         }
//
//         private void OnLogsButtonClick(object obj, EventArgs args)
//         {
//             _logsButton.Enabled = false;
//             
//             if (_isLogsOpen) CloseLogsPanel();
//             else OpenLogsPanel();
//             
//             _logsButton.Text = _isLogsOpen ? Constants.LogsButtonShowText : Constants.LogsButtonHideText;
//             _isLogsOpen = !_isLogsOpen;
//             
//             _logsButton.Enabled = true;
//         }
//
//         private void CloseLogsPanel()
//         {
//             while ((_panel.Left + _panelWidth) > 0)
//             {
//                 _panel.Left -= _panelAnimationStep;
//                 Thread.Sleep(AnimationDelay);
//             }
//             //end correction
//             _panel.Left = -_panelWidth;
//         }
//
//         private void OpenLogsPanel()
//         {
//             while (_panel.Left < 0)
//             {
//                 _panel.Left += _panelAnimationStep;
//                 Thread.Sleep(AnimationDelay);
//             }
//             // end correction
//             _panel.Left = 0;
//         }
//
//         private void HideLogsPanel()
//         {
//             _panel.Left -= _panelWidth;
//         }
//     }
// }