using System.Web;

namespace ByteCarrot.Masslog.Core.Logging.Behaviors
{
    public interface IMonitorBehaviorFactory
    {
        IMonitorBehavior GetBehaviorFor(HttpApplication application);
    }
}