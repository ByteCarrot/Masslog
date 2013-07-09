
using System.Globalization;

namespace ByteCarrot.Masslog.Web.Infrastructure.Extensions
{
    public static class DateTimeFormatInfoExtensions
    {
        public static string TojQueryDateFormat(this DateTimeFormatInfo dtfi)
        {
            return dtfi.ShortDatePattern
                .Replace("yyyy", "yy")
                .Replace("MM", "mm");
        }

        public static string TojQueryTimeFormat(this DateTimeFormatInfo dtfi)
        {
            return dtfi.ShortTimePattern
                .Replace("H", "h");
        }
    }
}