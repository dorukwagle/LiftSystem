using System;
using System.Windows.Forms;
using LiftSystem.views;


namespace LiftSystem
{
    static class LiftSystem
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            var baseView = new BaseView();
            var logsView = new LogsView(baseView.LeftPanel, baseView.LeftPanelWidth, baseView.PanelHeight);
            
            Application.Run(baseView);
        }
    }
}