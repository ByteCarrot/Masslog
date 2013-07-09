using System.Collections.Specialized;
using System.Web;

namespace ByteCarrot.Masslog.Core.Rules
{
    public class ActivityContext : IActivityContext
    {
        private readonly HttpContext _context;

        public ActivityContext(HttpContext context)
        {
            _context = context;
        }

        public string Url
        {
            get { return _context.Request.Url.AbsoluteUri; }
        }

        public string Machine
        {
            get { return _context.Server.MachineName; }
        }

        public int StatusCode
        {
            get { return _context.Response.StatusCode; }
        }

        public int RequestSize
        {
            get { return _context.Request.ContentLength; }
        }

        public int ResponseSize
        {
            get { return (int) _context.Response.Filter.Length; }
        }

        public NameValueCollection RequestHeaders
        {
            get { return _context.Request.Headers; }
        }

        public NameValueCollection ResponseHeaders
        {
            get { return _context.Response.Headers; }
        }
    }
}