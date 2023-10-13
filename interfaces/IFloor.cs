using System;
using System.Drawing.Printing;
using System.Windows;
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
            Application.Current.Dispatcher.Invoke(() => {
                view.OpenLeftDoor();
                view.OpenRightDoor();
            });
            DoorOpen = true;
        }

        public void CloseDoor()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                view.CloseLeftDoor();
                view.CloseRightDoor();
            });
            
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
                if (LiftState.CurrentFloor == this && LiftState.Status != LiftStatus.Moving)
                {
                    if (DoorOpen) return;
                    OpenDoor();
                }
                else
                {
                    Schedule.AddSchedule(this);
                    LogRequest();
                    if (LiftState.Status == LiftStatus.Stopped) ScheduleEventEmitter.EmitScheduleUpdate();
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
                    if (LiftState.Status == LiftStatus.Stopped) ScheduleEventEmitter.EmitScheduleUpdate();
                };
            }
            
            SubscribeLiftEvents();
        }
        
        private void SubscribeLiftEvents()
        {
            
            LiftEventsEmitter.AddOnFloorChange(floor => view.LabelIndicator = LiftState.CurrentFloor.GetFloorNumber().ToString());
            LiftEventsEmitter.AddOnMotionStateChange((status, direction) =>
            {
                if (status != LiftStatus.Moving)
                {
                    view.StopIndicator();
                    return;
                }
                
                if (direction == LiftDirection.Down)
                    view.PlayLiftDownIndicator();
                else view.PlayLiftUpIndicator();
                
            });
        }

        public bool IsDoorOpened => DoorOpen;
        
        private void LogRequest() => new LiftModel().Log($"Request received for floor {GetFloorNumber()}");
        public void LogArrival() => new LiftModel().Log($"Lift Arrived at floor {GetFloorNumber()}");
        public abstract int GetFloorNumber();
    }
}