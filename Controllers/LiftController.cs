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
            
            ScheduleEventEmitter.Instance.AddOnScheduleUpdate(StartLift);
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
            var oneSec = new Thread(o => Thread.Sleep(1000));
            var threeSec = new Thread(o => Thread.Sleep(3000));
            
            while (!Schedule.IsEmpty)
            {
                if (LiftState.Direction == LiftDirection.Up && !IsLastFloor(LiftState.CurrentFloor))
                {
                    if (Schedule.Dequeue(LiftState.CurrentFloor))
                    {
                        LiftState.Status = LiftStatus.Paused;
                        LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, LiftDirection.Up);
                        LiftState.CurrentFloor.LogArrival();
                        oneSec.Start();
                        oneSec.Join();
                        LiftState.CurrentFloor.OpenDoor();
                        threeSec.Start();
                        threeSec.Join();
                        LiftState.CurrentFloor.CloseDoor();
                        LiftState.Status = LiftStatus.Moving;
                    }
                    MoveLiftUp();
                }

                if (LiftState.Direction == LiftDirection.Up && IsLastFloor(LiftState.CurrentFloor))
                {
                    LiftState.Direction = LiftDirection.Down;

                    if (Schedule.Dequeue(LiftState.CurrentFloor))
                    {
                        LiftState.Status = LiftStatus.Paused;
                        LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, LiftDirection.Down);
                        LiftState.CurrentFloor.LogArrival();
                        oneSec.Start();
                        oneSec.Join();
                        LiftState.CurrentFloor.OpenDoor();
                        threeSec.Start();
                        threeSec.Join();
                        LiftState.CurrentFloor.CloseDoor();
                        LiftState.Status = LiftStatus.Moving;
                    }
                    MoveLiftDown();
                }

                if (LiftState.Direction == LiftDirection.Down && !IsFirstFloor(LiftState.CurrentFloor))
                {
                    if (Schedule.Dequeue(LiftState.CurrentFloor))
                    {
                        LiftState.Status = LiftStatus.Paused;
                        LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, LiftDirection.Down);
                        LiftState.CurrentFloor.LogArrival();
                        oneSec.Start();
                        oneSec.Join();
                        LiftState.CurrentFloor.OpenDoor();
                        threeSec.Start();
                        threeSec.Join();
                        LiftState.CurrentFloor.CloseDoor();
                        LiftState.Status = LiftStatus.Moving;
                    }
                    MoveLiftDown();
                }

                if (LiftState.Direction == LiftDirection.Down && IsFirstFloor(LiftState.CurrentFloor))
                {
                    LiftState.Direction = LiftDirection.Up;
                    
                    if (Schedule.Dequeue(LiftState.CurrentFloor))
                    {
                        LiftState.Status = LiftStatus.Paused;
                        LiftEventsEmitter.EmitMotionStateChange(LiftStatus.Paused, LiftDirection.Up);
                        LiftState.CurrentFloor.LogArrival();
                        oneSec.Start();
                        oneSec.Join();
                        LiftState.CurrentFloor.OpenDoor();
                        threeSec.Start();
                        threeSec.Join();
                        LiftState.CurrentFloor.CloseDoor();
                        LiftState.Status = LiftStatus.Moving;
                    }
                    MoveLiftUp();
                }
            }
            Console.WriteLine("running main loop");
            LiftState.Status = LiftStatus.Stopped;
        }

        private void MoveLiftUp()
        {
            SimulateLiftMovement();
            LiftState.CurrentFloor = floors[LiftState.CurrentFloor.GetFloorNumber()];
            LiftEventsEmitter.EmitFloorChange(LiftState.CurrentFloor);
        }

        private void MoveLiftDown()
        {
            SimulateLiftMovement();
            LiftState.CurrentFloor = floors[LiftState.CurrentFloor.GetFloorNumber() - 2];
            LiftEventsEmitter.EmitFloorChange(LiftState.CurrentFloor);
        }

        private void SimulateLiftMovement()
        {
            Thread.Sleep(floorTravelDuration * 1000);
        }
        
        private bool IsFirstFloor(IFloor floor) => floors[0] == floor;
        private bool IsLastFloor(IFloor floor) => floors[floors.Length - 1] == floor;
    }
}