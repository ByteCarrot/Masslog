using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public class StopWatchLogger : IDisposable
    {
        private readonly ILog _log;
        private readonly MethodBase _method;
        private readonly Stopwatch _watch = new Stopwatch();

        public StopWatchLogger(ILog log, MethodBase method)
        {
            _log = log;
            _method = method;
            _watch.Start();
        }

        public void Dispose()
        {
            _watch.Stop();
            _log.DebugFormat("{0} duration: {1}", _method.Name, _watch.Elapsed);
        }
    }
}