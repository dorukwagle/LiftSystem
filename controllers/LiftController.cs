using System.Windows.Media.TextFormatting;
using LiftSystem.interfaces;
using LiftSystem.views;

namespace LiftSystem.controllers
{
    public class LiftController
    {
        public LiftController(IFloor[] floors, LiftView liftView, int width, int height)
        {
            for (var i = floors.Length - 1; i >= 0; --i)
            {
                var floor = new FloorView(width, height);
                liftView.AddFloor(floor);
                floors[i].SetView(floor);
            }
        }
    }
}