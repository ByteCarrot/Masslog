using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Request
    {
        public Request()
        {
            Headers = new List<NameValue>();
        }

        public string Body { get; set; }

        public bool BodyIgnored { get; set; }

        public List<NameValue> Headers { get; set; }

        public string HttpMethod { get; set; }

        public string RawUrl { get; set; }

        public string Protocol { get; set; }

        public string ContentType { get; set; }

        public string ContentEncoding { get; set; }

        public int ContentLength { get; set; }
    }
}