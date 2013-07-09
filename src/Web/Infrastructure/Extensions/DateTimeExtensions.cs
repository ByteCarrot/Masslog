using System;

namespace ByteCarrot.Masslog.Web.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToConsistentFormat(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
        }
    }
}