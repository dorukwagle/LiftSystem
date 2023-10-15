using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LiftSystem
{
    public static class LogsEventEmitter
    {
        private static List<Action<int, string, string>> callbacks = new List<Action<int, string, string>>();

        public static void AddOnLog(Action<int, string, string> fun) => callbacks.Add(fun);

        public static void EmitLog(int id, string message, string created)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, args) =>
                {
                    foreach (var callback in callbacks)
                    {
                        callback(id, message, created);
                    }
                };
            worker.RunWorkerAsync();
        }
    }
}