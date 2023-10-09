using System;
using System.Collections.Generic;
using System.ComponentModel;
using LiftSystem.Enums;
using LiftSystem.interfaces;

namespace LiftSystem
{
    public static class LiftEventsEmitter
    {
        private static BackgroundWorker worker2 = new BackgroundWorker();

        private static List<Action<LiftStatus, LiftDirection>> motionStateChangeCallbacks =
            new List<Action<LiftStatus, LiftDirection>>();

        private static List<Action<IFloor>> floorChangeCallbacks = new List<Action<IFloor>>();

        public static void AddOnMotionStateChange(Action<LiftStatus, LiftDirection> callback) =>
            motionStateChangeCallbacks.Add(callback);

        public static void AddOnFloorChange(Action<IFloor> callback) => floorChangeCallbacks.Add(callback);

        public static void EmitMotionStateChange(LiftStatus status, LiftDirection direction)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, args) =>
            {
                foreach (var callback in motionStateChangeCallbacks)
                    callback(status, direction);
            };
            worker.RunWorkerAsync();
        }
        
        public static void EmitFloorChange(IFloor currentFloor)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, args) =>
            {
                foreach (var callback in floorChangeCallbacks)
                    callback(currentFloor);
            };
            worker.RunWorkerAsync();
        }
    }
}