using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media.TextFormatting;
using LiftSystem.Enums;
using LiftSystem.interfaces;
using LiftSystem.Model;
using LiftSystem.views;
using LiftSystem.views;

namespace LiftSystem.controllers
{
    public class LiftController
    {
        private LiftModel model = new LiftModel();
        private IFloor[] floors;
        private int floorTravelDuration = 3; //sec
        
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
            const int longInterval = 5000;
            
            while (!Schedule.IsEmpty)
            {
                if (LiftState.Direction == LiftDirection.Up)
                {
                    PrepareForMovement();
                    MoveLiftUp();
                }
                else
                {
                    PrepareForMovement();
                    MoveLiftDown();
                }
                
                if (!Schedule.Dequeue(LiftState.CurrentFloor)) continue;
                
                LiftState.Status = LiftStatus.Paused;
                LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, LiftState.Direction);
                LiftState.CurrentFloor.LogArrival();
                Thread.Sleep(shortInterval);
                LiftState.CurrentFloor.OpenDoor();
                Thread.Sleep(longInterval);
                LiftState.CurrentFloor.CloseDoor();
                WaitForDoorClose();
            }
            LiftState.Status = LiftStatus.Stopped;
        }

        private void MoveLiftUp()
        {
            PrepareForMovement();
            LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Moving, LiftDirection.Up);
            SimulateLiftMovement();
            LiftState.CurrentFloor = floors[LiftState.CurrentFloor.GetFloorNumber()];
            LiftState.Direction = IsLastFloor(LiftState.CurrentFloor) ? LiftDirection.Down : LiftDirection.Up; 
            LiftEventsEmitter.EmitFloorChange(LiftState.CurrentFloor);
        }

        private void MoveLiftDown()
        {
            PrepareForMovement();
            LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Moving, LiftDirection.Down);
            SimulateLiftMovement();
            LiftState.CurrentFloor = floors[LiftState.CurrentFloor.GetFloorNumber() - 2];
            LiftState.Direction = IsFirstFloor(LiftState.CurrentFloor) ? LiftDirection.Up : LiftDirection.Down;
            LiftEventsEmitter.EmitFloorChange(LiftState.CurrentFloor);
        }

        private void SimulateLiftMovement()
        {
            Thread.Sleep(floorTravelDuration * 1000);
        }

        private void PrepareForMovement()
        {
            LiftState.Status = LiftStatus.Moving;
            if (LiftState.CurrentFloor.IsDoorOpened)
            {
                LiftState.CurrentFloor.CloseDoor();
                WaitForDoorClose();
            }
        }

        private void WaitForDoorClose() => Thread.Sleep(Constants.DoorAnimationDuration * 1000);
        
        private bool IsFirstFloor(IFloor floor) => floors[0] == floor;
        private bool IsLastFloor(IFloor floor) => floors[floors.Length - 1] == floor;
    }
}