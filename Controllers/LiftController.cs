using System.ComponentModel;
using System.Threading;
using LiftSystem.Enums;
using LiftSystem.interfaces;
using LiftSystem.Model;
using LiftSystem.views;

namespace LiftSystem.controllers
{
    public class LiftController
    {
        private LiftModel model = new LiftModel();
        private IFloor[] floors;
        private int floorTravelDuration = 3; //sec
        private int liftWaitDuration = 5000; // 5 sec
        
        public LiftController(IFloor[] floors, LiftView liftView, int width, int height)
        {
            this.floors = floors;
            
            for (var i = floors.Length - 1; i >= 0; --i)
            {
                var floor = new FloorView(width, height);
                liftView.AddFloor(floor);
                floors[i].SetView(floor);
                floors[i].InitializeFloor();
            }
            
            InitializeLift();
            
            ScheduleEventEmitter.AddOnScheduleUpdate(StartLift);
        }
        
        private void InitializeLift()
        {
            model.Log("Lift Positioned at floor 1");
            LiftState.CurrentFloor = floors[0];
            LiftState.Status = LiftStatus.Stopped;
            LiftState.Direction = LiftDirection.Up;
        }

        private void StartLift()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, args) => StartMainLoop();
            
            worker.RunWorkerAsync();
        }

        private void StartMainLoop()
        {
            const int  shortInterval = 1000;
            
            while (!Schedule.IsEmpty)
            {
                PrepareForMovement();
                if (LiftState.Direction == LiftDirection.Up)
                {
                   if (Schedule.HasHigherFloors(LiftState.CurrentFloor)) MoveLiftUp();
                   else MoveLiftDown();
                }
                else
                {
                    if (Schedule.HasLowerFloors(LiftState.CurrentFloor)) MoveLiftDown();
                    else MoveLiftUp();
                }
                
                if (!Schedule.Dequeue(LiftState.CurrentFloor)) continue;
                
                LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, LiftState.Direction);
                LiftState.Status = LiftStatus.Paused;
                LiftState.CurrentFloor.LogArrival();
                Thread.Sleep(shortInterval);
                LiftState.CurrentFloor.OpenDoor();
                WaitForFurtherCommands();
                LiftState.CurrentFloor.CloseDoor();
                WaitForDoorClose();
            }
            LiftState.Status = LiftStatus.Stopped;
        }
    
        private void MoveLiftUp()
        {
            PrepareForMovement();
            LiftState.Direction = LiftDirection.Up;
            LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Moving, LiftDirection.Up);
            SimulateLiftMovement();
            LiftState.CurrentFloor = floors[LiftState.CurrentFloor.GetFloorNumber()];
            LiftState.Direction = IsLastFloor(LiftState.CurrentFloor) ? LiftDirection.Down : LiftDirection.Up; 
            if (IsLastFloor(LiftState.CurrentFloor)) LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, 0);
            LiftEventsEmitter.EmitFloorChange(LiftState.CurrentFloor);
        }

        private void MoveLiftDown()
        {
            PrepareForMovement();
            LiftState.Direction = LiftDirection.Down;
            LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Moving, LiftDirection.Down);
            SimulateLiftMovement();
            LiftState.CurrentFloor = floors[LiftState.CurrentFloor.GetFloorNumber() - 2];
            LiftState.Direction = IsFirstFloor(LiftState.CurrentFloor) ? LiftDirection.Up : LiftDirection.Down;
            if(IsFirstFloor(LiftState.CurrentFloor)) LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, 0);
            LiftEventsEmitter.EmitFloorChange(LiftState.CurrentFloor);
        }

        private void SimulateLiftMovement()
        {
            Thread.Sleep(floorTravelDuration * 1000);
        }

        private void PrepareForMovement()
        {
            LiftState.Status = LiftStatus.Moving;
            if (!LiftState.CurrentFloor.IsDoorOpened) return;
            
            LiftState.CurrentFloor.CloseDoor();
            WaitForDoorClose();
        }

        private void WaitForDoorClose() => Thread.Sleep(Constants.DoorAnimationDuration * 1000);
        private void WaitForFurtherCommands() => Thread.Sleep(liftWaitDuration);

        private bool IsFirstFloor(IFloor floor) => floors[0] == floor;
        private bool IsLastFloor(IFloor floor) => floors[floors.Length - 1] == floor;
    }
}