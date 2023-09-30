using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using LiftSystem.interfaces;

namespace LiftSystem.views
{
    public class FloorView : IFloorView
    {
        private Canvas _canvas;
        private Label wallPanel;
        private Label numPadPanel;
        private Button callLift;
        private Button[] numPad;
        private Image leftDoor;
        private Image rightDoor;
        
        private ScaleTransform leftDoorAnimation;
        private ScaleTransform rightDoorAmination;
        private int leftDoorPosition = 75;
        private int animationDuration = 1;
        
        public FloorView(int width, int height)
        {
            var liftDoorWidth = 100;

            var doorOpened = false;
            
            _canvas = new Canvas();
            _canvas.Width = width / 2;
            _canvas.Height = height / 2 - 5;
            
            var numPadLeft = _canvas.Width / 2 - 42;
            var numPadTop = _canvas.Height / 2 - 50;
            
            numPad = new Button[2];
            
            var borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(200)));

            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Resources.Add(typeof(Border), borderStyle);
            
            var floor = AddImage(_canvas, "pack://application:,,,/res/LiftFloor.png");
            floor.Width = _canvas.Width;


            callLift = new Button();
            callLift.Content = GetBtnImage("pack://application:,,,/res/CallButton.png");
            callLift.Background = null;
            callLift.Style = buttonStyle;
            callLift.Width = 55;
            callLift.Click += (sender, args) =>
            {
                if (!doorOpened)
                {
                    OpenLeftDoor();
                    OpenRightDoor();
                }
                else
                {
                    CloseLeftDoor();
                    CloseRightDoor();
                }

                doorOpened = !doorOpened;
            };
            callLift.BorderThickness = new Thickness(0);
            Canvas.SetTop(callLift, _canvas.Height/2);
            Canvas.SetLeft(callLift, 320);
            _canvas.Children.Add(callLift);

            wallPanel = CreateLabel(100, 32, 17);
            wallPanel.Content = "Floor: 1";
            Canvas.SetLeft(wallPanel, _canvas.Width / 2 - 50);
            _canvas.Children.Add(wallPanel);

            numPadPanel = CreateLabel(75, 32, 17);
            numPadPanel.Content = "Floor: 1";
            Canvas.SetLeft(numPadPanel, numPadLeft);
            Canvas.SetTop(numPadPanel, numPadTop);
            _canvas.Children.Add(numPadPanel);

            for (var i = 0; i < numPad.Length; ++i)
            {
                numPad[i] = new Button()
                {
                    Content = i + 1,
                    Height = 25,
                    Width = numPadPanel.Width - 20,
                    Style = buttonStyle,
                    FontSize = 15,
                    FontWeight = FontWeights.Bold
                };
                
                Canvas.SetTop(numPad[i], numPadTop + numPadPanel.Height + 5 + i * 25);
                Canvas.SetLeft(numPad[i], numPadLeft + 10);
                _canvas.Children.Add(numPad[i]);
            }

            leftDoor = AddImage(_canvas, "pack://application:,,,/res/LiftFloorLeftDoor.png", 
                liftDoorWidth, _canvas.Height - 100, leftDoorPosition, 87);
            
            rightDoor = AddImage(_canvas, "pack://application:,,,/res/LiftFloorRightDoor.png",
                liftDoorWidth, _canvas.Height - 100, leftDoorPosition + 92, 87);
            
            SetupLeftDoorAnimation();
            SetupRightDoorAnimation();
        }
        
        private Image AddImage(Panel panel, string uri)
        {
            Image image = new Image();
            // Create a BitmapImage from imageBytes
            image.Source = new BitmapImage(new Uri(uri));

            // Set image dimensions and add it to the grid
            panel.Children.Add(image);
            return image;
        }

        private Label CreateLabel(int width, int height, int fontSize)
        {
            var label = new Label();
            label.Background = new SolidColorBrush(Colors.Black);
            label.Width = width;
            label.Height = height;
            label.FontSize = fontSize;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = new SolidColorBrush(Colors.Red);
            return label;
        }

        private Image GetBtnImage(string uri)
        {
            var image = new Image();
            image.Source = new BitmapImage(new Uri(uri));
            return image;
        }

        private Image AddImage(Canvas panel, string uri, double width, double height, int left, int top)
        {
            var image = AddImage(panel, uri);
            image.Width = width;
            image.Height = height;
            Canvas.SetLeft(image, left);
            Canvas.SetTop(image, top);

            return image;
        }

        private void SetupLeftDoorAnimation()
        {
            leftDoorAnimation = new ScaleTransform();
            leftDoor.RenderTransform = leftDoorAnimation;
        }

        private void SetupRightDoorAnimation()
        {
            rightDoorAmination = new ScaleTransform();
            rightDoor.RenderTransform = rightDoorAmination;
            rightDoor.RenderTransformOrigin = new Point(1, 0);
        }

        public void OpenLeftDoor()
        {
            var anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(animationDuration));
            leftDoorAnimation.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        }

        public void OpenRightDoor()
        {
            var anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(animationDuration));
            rightDoorAmination.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        }

        public void CloseLeftDoor()
        {
            var anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(animationDuration));
            leftDoorAnimation.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        }

        public void CloseRightDoor()
        {
            var anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(animationDuration));
            rightDoorAmination.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        }

        public Canvas GetView() => _canvas;
    }
}