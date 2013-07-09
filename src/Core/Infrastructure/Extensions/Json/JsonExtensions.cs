namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions.Json
{
    public static class JsonExtensions
    {
        public static string ToFormattedJson(this string s)
        {
            return new JsonFormatter(s).Format().Trim();
        }
    }
}
