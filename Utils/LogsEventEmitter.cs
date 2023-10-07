using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LiftSystem
{
    public class LogsEventEmitter
    {
        private List<Action<int, string>> callbacks = new List<Action<int, string>>();
        private BackgroundWorker worker = new BackgroundWorker();

        private static LogsEventEmitter instance;

        private LogsEventEmitter()
        {}

        public static LogsEventEmitter Instance
        {
            get
            {
                if (instance == null)
                    instance = new LogsEventEmitter();
                return instance;
            }
        }

        public void AddOnLog(Action<int, string> fun) => callbacks.Add(fun);

        public void EmitLog(int id, string message)
        {
            worker.DoWork += (sender, args) =>
                {
                    foreach (var callback in callbacks)
                    {
                        callback(id, message);
                    }
                };
            worker.RunWorkerAsync();
        }
    }
}