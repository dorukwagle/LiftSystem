using System.Windows.Controls;
using LiftSystem.interfaces;

namespace LiftSystem.views
{
    public class LiftView
    {
        private static int floorCount = 0;
        
        private StackPanel _shaftPanel;
        private int _shaftWidth;
        
        public LiftView(StackPanel panel, int width, int height)
        {
            _shaftWidth = (int)(width * 0.6);

            var scroller = new ScrollViewer();
            scroller.Height = height;
            scroller.Width = _shaftWidth;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; 
            
            _shaftPanel = new StackPanel();
            
            scroller.Content = _shaftPanel;
            panel.Children.Add(scroller);
        }

        public void AddFloor(IFloorView floor)
        {
            var panel = floor.GetView();
            Canvas.SetTop(panel, panel.Height * floorCount + (floorCount++ > 0 ? 5 : 0));
            _shaftPanel.Children.Add(panel);
        }
    }
}