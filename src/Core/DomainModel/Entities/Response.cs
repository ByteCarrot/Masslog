using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Response
    {
        public Response()
        {
            Headers = new List<NameValue>();
        }

        public string Body { get; set; }

        public bool BodyIgnored { get; set; }

        public List<NameValue> Headers { get; set; }

        public int StatusCode { get; set; }

        public int? SubStatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string ContentType { get; set; }

        public string ContentEncoding { get; set; }

        public long ContentLength { get; set; }
    }
}