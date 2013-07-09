using System;
using System.Text;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToTimeString(this TimeSpan? ts)
        {
            return ts == null ? null : ToTimeString((TimeSpan)ts);
        }

        public static string ToTimeString(this TimeSpan ts)
        {
            var sb = new StringBuilder();

            if (ts.Days > 0)
            {
                sb.AppendFormat("{0}d ", ts.Days);
            }

            if (ts.Hours > 0 || sb.Length > 0)
            {
                sb.AppendFormat("{0}h ", ts.Hours);
            }

            if (ts.Minutes > 0 || sb.Length > 0)
            {
                sb.AppendFormat("{0}m ", ts.Minutes);
            }

            if (ts.Seconds > 0 || sb.Length > 0)
            {
                sb.AppendFormat("{0}s ", ts.Seconds);
            }

            if (ts.Milliseconds > 0 || sb.Length > 0)
            {
                sb.AppendFormat("{0}ms", ts.Milliseconds);
            }

            return sb.ToString();
        }
    }
}