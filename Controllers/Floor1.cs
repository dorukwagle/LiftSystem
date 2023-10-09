using LiftSystem.interfaces;

namespace LiftSystem.controllers
{
    public class Floor1 : IFloor
    {
        public override void InitializeFloor()
        {
            var callBtn = view.CallLiftBtn;
            
        }

        public override void LogRequest()
        {
            throw new System.NotImplementedException();
        }

        public override void LogArrival()
        {
            throw new System.NotImplementedException();
        }

        public override void LogDelivery()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateDisplay()
        {
            throw new System.NotImplementedException();
        }

        public override int GetFloorNumber() => 1;
    }
}