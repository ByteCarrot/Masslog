using System.Xml.Linq;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public static class XmlExtensions
    {
        public static string ToFormattedXml(this string s)
        {
            return XElement.Parse(s).ToString().Trim();
        }
    }
}