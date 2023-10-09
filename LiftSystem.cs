using System;
using System.Windows;
using LiftSystem.controllers;
using LiftSystem.interfaces;
// using LiftSystem.controllers;
using LiftSystem.views;

namespace LiftSystem
{
    public class LiftSystem
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            
            var baseView = new BaseView();
            var logsView = new LogsView(baseView.LeftPanel, baseView.LeftPanelWidth, baseView.PanelHeight);
            var liftView = new LiftView(baseView.RightPanel, baseView.RightPanelWidth, baseView.PanelHeight);

            IFloor[] floors = { new Floor1(), new Floor2() };

            _ = new LogsController(logsView); // set before LiftController : order matters
            _ = new LiftController(floors, liftView, baseView.RightPanelWidth, baseView.PanelHeight);
            
            app.Run(baseView);
        }
    }
}