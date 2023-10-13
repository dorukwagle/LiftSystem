using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LiftSystem
{
    public static class LogsEventEmitter
    {
        private static List<Action<int, string>> callbacks = new List<Action<int, string>>();

        public static void AddOnLog(Action<int, string> fun) => callbacks.Add(fun);

        public static void EmitLog(int id, string message)
        {
            var worker = new BackgroundWorker();
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