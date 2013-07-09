using System.IO;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadToEnd(this Stream s)
        {
            if (!s.CanSeek)
            {
                return new StreamReader(s).ReadToEnd();
            }

            var position = s.Position;
            s.Position = 0;
            var text = new StreamReader(s).ReadToEnd();
            s.Position = position;
            return text;
        }
    }
}
