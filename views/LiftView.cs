using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiftSystem.interfaces;
using Microsoft.Win32.SafeHandles;
using Color = System.Drawing.Color;

namespace LiftSystem.views
{
    public class LiftView
    {
        private static int floorCount = 0;
        
        private Canvas _shaftPanel;
        private int _shaftWidth;
        
        public LiftView(StackPanel panel, int width, int height)
        {
            _shaftWidth = (int)(width * 0.6);

            _shaftPanel = new Canvas();
            _shaftPanel.Height = height; 
            _shaftPanel.Width = _shaftWidth;
            _shaftPanel.Background = new SolidColorBrush(Colors.Aqua);
            panel.Children.Add(_shaftPanel);
        }

        public void AddFloor(IFloorView floor)
        {
            var panel = floor.GetView();
            Canvas.SetTop(panel, panel.Height * floorCount + (floorCount++ > 0 ? 5 : 0));
            _shaftPanel.Children.Add(panel);
        }
    }
}