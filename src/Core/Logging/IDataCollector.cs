using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.Logging.Behaviors;

namespace ByteCarrot.Masslog.Core.Logging
{
    public interface IDataCollector
    {
        Activity Collect(HttpApplication application, IMonitorBehavior behavior, Activity activity);
    }
}