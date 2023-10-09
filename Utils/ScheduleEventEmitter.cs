using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Documents;

namespace LiftSystem
{
    public class ScheduleEventEmitter
    {
        // events for schedule update i.e. when user clicks floor buttons. so as to stop the lift
        
        private static ScheduleEventEmitter instance;

        private List<Action> scheduleUpdateCallbacks = new List<Action>();
        
        private ScheduleEventEmitter() {}

        public static ScheduleEventEmitter Instance {
            get
            {
                if (instance == null)
                    instance = new ScheduleEventEmitter();
                return instance;
            }
        }

        public void AddOnScheduleUpdate(Action callback) => scheduleUpdateCallbacks.Add(callback);

        public void EmitScheduleUpdate()
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