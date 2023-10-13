using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Documents;

namespace LiftSystem
{
    public static class ScheduleEventEmitter
    {
        // events for schedule update i.e. when user clicks floor buttons. so as to stop the lift

        private static List<Action> scheduleUpdateCallbacks = new List<Action>();
        
        public static void AddOnScheduleUpdate(Action callback) => scheduleUpdateCallbacks.Add(callback);

        public static void EmitScheduleUpdate()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, args) =>
            {
                foreach (var callback in scheduleUpdateCallbacks)
                {
                    callback();
                }
            };
            worker.RunWorkerAsync();
        }
    }
}