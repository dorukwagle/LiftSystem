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
using Microsoft.Win32.SafeHandles;
using Color = System.Drawing.Color;

namespace LiftSystem.views
{
    public class LiftView
    {
        private Grid _shaftPanel;
        private int _shaftHeight;
        private int _shaftWidth;
        
        public LiftView(StackPanel panel, int height, int width)
        {
            _shaftWidth = (int)(width * 0.6);
            var roofHeight = 300;

            _shaftPanel = new Grid();
            _shaftPanel.Height = height; 
            _shaftPanel.Width = _shaftWidth;
            _shaftPanel.Background = new SolidColorBrush(Colors.Aqua);
            panel.Children.Add(_shaftPanel);

            var roof = AddImageToGrid(_shaftPanel, "pack://application:,,,/res/LiftShaftRoof.png", 0, 0);
            roof.Height = roofHeight;
            roof.Width = _shaftWidth;

            var floor = AddImageToGrid(_shaftPanel, "pack://application:,,,/res/LiftShaftFloor.png", 0, 0);
            floor.Width = _shaftWidth;
            floor.Height = roofHeight;

            _shaftHeight = (int) (height - (roof.Height + floor.Height));
        }

        private Image AddImageToGrid(Grid grid, string uri, int row, int column)
        {
            Image image = new Image();
            // Create a BitmapImage from imageBytes
            image.Source = new BitmapImage(new Uri(uri));

            // Set image dimensions and add it to the grid
            Grid.SetRow(image, row);
            Grid.SetColumn(image, column);
            grid.Children.Add(image);

            return image;
        }
    }
}