using System;
using System.Windows.Controls;
using LiftSystem.Enums;
using LiftSystem.Model;
using LiftSystem.views;

namespace LiftSystem.interfaces
{
    public abstract class IFloor
    {
        protected FloorView view;
        protected bool DoorOpen = false;

        public void OpenDoor()
        {
            view.OpenLeftDoor();
            view.OpenRightDoor();
            DoorOpen = true;
        }

        public void CloseDoor()
        {
            view.CloseLeftDoor();
            view.OpenRightDoor();
            DoorOpen = false;
        }

        public void SetView(FloorView view)
        {
            this.view = view;
        }

        public void InitializeFloor()
        {
            var callBtn = view.CallLiftBtn;
            var numpad = view.NumPad;
            
            callBtn.Click += (sender, args) =>
            {
                if (LiftState.CurrentFloor == this)
                {
                    if (DoorOpen) return;
                    OpenDoor();
                }
                else
                {
                    Schedule.AddSchedule(this);
                    LogRequest();
                    if (LiftState.Status == LiftStatus.Stopped) ScheduleEventEmitter.Instance.EmitScheduleUpdate();
                }
            };
            
            foreach (var button in numpad)
            {
                button.Click += (sender, args) =>
                {
                    var btnText = (args.Source as Button).Content.ToString();
                    var floorNumber = Int32.Parse(btnText);

                    if (floorNumber == GetFloorNumber()) return;

                    var targetFloor = Constants.Floors[floorNumber - 1];
                    Schedule.AddSchedule(targetFloor);
                    LogRequest();
                    if (LiftState.Status == LiftStatus.Stopped) ScheduleEventEmitter.Instance.EmitScheduleUpdate();
                };
            }
        }

        public abstract void SubscribeLiftEvents();
        public abstract void LogRequest();
        public abstract void LogArrival();
        public abstract int GetFloorNumber();
    }
}