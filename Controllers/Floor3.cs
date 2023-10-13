using LiftSystem.interfaces;
using LiftSystem.Model;

namespace LiftSystem.controllers
{
    public class Floor3 : IFloor
    {
        public override void LogRequest()
        {
            new LiftModel().Log("Request received for floor 3");
        }

        public override void LogArrival()
        {
            new LiftModel().Log("Lift Arrived at floor 3");
        }
        
        public override int GetFloorNumber() => 3;
    }
}