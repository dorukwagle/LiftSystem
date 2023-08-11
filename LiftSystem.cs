using System;
using System.Windows.Forms;
using LiftSystem.controllers;
using LiftSystem.views;


namespace LiftSystem
{
    static class LiftSystem
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            var baseView = new BaseView();
            var logsView = new LogsView(baseView.LeftPanel, baseView.LeftPanelWidth, baseView.PanelHeight);

            new LogsController(logsView);
            Application.Run(baseView);
        }
    }
}