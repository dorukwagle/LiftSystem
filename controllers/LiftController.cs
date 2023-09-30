using System.Windows.Media.TextFormatting;
using LiftSystem.interfaces;
using LiftSystem.views;

namespace LiftSystem.controllers
{
    public class LiftController
    {
        // public LiftController(Floor[] floors, LiftView liftView, int widht, int height) 
        public LiftController(LiftView liftView, int width, int height)
        {
            var floor1 = new FloorView(width, height);
            var floor2 = new FloorView(width, height);

            liftView.AddFloor(floor1);
            liftView.AddFloor(floor2);

            // floors[0].setView(floor1);
            // floors[1].setView(floor2);
        }
    }
}