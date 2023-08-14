using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
// using LiftSystem.controllers;
// using LiftSystem.views;

namespace LiftSystem
{
    public class LiftSystem: Application
    {
        [STAThread]
        public static void Main()
        {
            // Application.EnableVisualStyles();
            // var baseView = new BaseView();
            // var logsView = new LogsView(baseView.LeftPanel, baseView.LeftPanelWidth, baseView.PanelHeight);
            // var liftView = new LiftView(baseView.RightPanel, baseView.RightPanelWidth, baseView.PanelHeight);
            //
            // _ = new LogsController(logsView);
            // Application.Run(baseView);
            var app = new LiftSystem();
            Window root = new Window();
            InkCanvas inkCanvas1 = new InkCanvas();

            root.Title = "Skortchpad";

            root.ResizeMode = ResizeMode.CanResizeWithGrip;
            inkCanvas1.Background = Brushes.DarkSlateBlue;
            inkCanvas1.DefaultDrawingAttributes.Color = Colors.SpringGreen;
            inkCanvas1.DefaultDrawingAttributes.Height = 10;
            inkCanvas1.DefaultDrawingAttributes.Width = 10;

            root.Content = inkCanvas1;
            root.Show();
            app.MainWindow = root;
            app.Run();
        }
    }
}