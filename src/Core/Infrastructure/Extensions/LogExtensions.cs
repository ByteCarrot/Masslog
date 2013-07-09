using log4net;
using System;
using System.Reflection;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public static class LogExtensions
    {
        public static IDisposable Duration(this ILog log, MethodBase method)
        {
            return new StopWatchLogger(log, method);
        }
    }
}
