using System.Web;

namespace ByteCarrot.Masslog.Core.Logging
{
    public interface IMonitor
    {
        void BeginRequest(HttpApplication application);

        void EndRequest();
    }
}