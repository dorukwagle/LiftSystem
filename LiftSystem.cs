using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiftSystem.controllers;
// using LiftSystem.controllers;
using LiftSystem.views;

namespace LiftSystem
{
    public class LiftSystem
    {
        [STAThread]
        public static void Main()
        {
            // Application.EnableVisualStyles();
            // var baseView = new BaseView();
            // var liftView = new LiftView(baseView.RightPanel, baseView.RightPanelWidth, baseView.PanelHeight);
            //
            // Application.Run(baseView);
            
            var app = new Application();
            
            var baseView = new BaseView();
            var logsView = new LogsView(baseView.LeftPanel, baseView.LeftPanelWidth, baseView.PanelHeight);
            _ = new LogsController(logsView);
            
            app.Run(baseView);
        }
    }
}