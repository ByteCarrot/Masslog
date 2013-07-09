using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.Logging.Behaviors;

namespace ByteCarrot.Masslog.Core.Logging
{
    public class DataCollectionContext
    {
        public DataCollectionContext(Activity activity, IMonitorBehavior behavior, HttpApplication application)
        {
            Activity = activity;
            Behavior = behavior;
            Application = application;
        }

        public Activity Activity { get; private set; }

        public IMonitorBehavior Behavior { get; private set; }

        public HttpApplication Application { get; private set; }
    }
}