using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiftSystem.interfaces;

namespace LiftSystem.views
{
    public class FloorView : IFloorView
    {
        private Canvas _canvas;
        private Label visualPanel;
        public FloorView(int width, int height)
        {
            _canvas = new Canvas();
            // "pack://application:,,,/res/LiftFloor.png"
            _canvas.Width = width / 2;
            _canvas.Height = height / 2 - 5;
            
            var floor = AddImage(_canvas, "pack://application:,,,/res/LiftFloor.png");
            floor.Width = _canvas.Width;
        }
        
        private Image AddImage(Canvas panel, string uri)
        {
            Image image = new Image();
            // Create a BitmapImage from imageBytes
            image.Source = new BitmapImage(new Uri(uri));

            // Set image dimensions and add it to the grid
            panel.Children.Add(image);
            return image;
        }


        public void OpenLeftDoor()
        {
            throw new NotImplementedException();
        }

        public void OpenRightDoor()
        {
            throw new NotImplementedException();
        }

        public void CloseLeftDoor()
        {
            throw new NotImplementedException();
        }

        public void CloseRightDoor()
        {
            throw new NotImplementedException();
        }

        public Canvas GetView() => _canvas;
    }
}