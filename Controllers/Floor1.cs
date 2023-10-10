using System;
using System.Windows.Controls;
using LiftSystem.Enums;
using LiftSystem.interfaces;
using LiftSystem.Model;

namespace LiftSystem.controllers
{
    public class Floor1 : IFloor
    {
        public override void SubscribeLiftEvents()
        {
            LiftEventsEmitter.AddOnFloorChange(floor => view.WallPanelLabel = LiftState.CurrentFloor.GetFloorNumber().ToString());
            LiftEventsEmitter.AddOnMotionStateChange((status, direction) => {});
        }

        public override void LogRequest()
        {
           new LiftModel().Log("Request received for floor 1");
        }

        public override void LogArrival()
        {
            new LiftModel().Log("Lift Arrived at floor 1");
        }
        
        public override int GetFloorNumber() => 1;
    }
}