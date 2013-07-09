using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.Logging.Behaviors;

namespace ByteCarrot.Masslog.Core.Logging
{
    public class Monitor : IMonitor
    {
        private readonly string _applicationId;
        private readonly IDataCollector _collector;
        private readonly IActivityProcessor _processor;
        private readonly IMonitorBehaviorFactory _behaviorFactory;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private HttpApplication _application;

        public Monitor(
            IDataCollector collector,
            IActivityProcessor processor,
            ILoggingConfiguration configuration,
            IMonitorBehaviorFactory behaviorFactory)
        {
            _applicationId = configuration.ApplicationId;
            _collector = collector;
            _processor = processor;
            _behaviorFactory = behaviorFactory;
        }

        public void BeginRequest(HttpApplication application)
        {
            _application = application;
            _application.Response.Filter = new OutputFilterStream(_application.Response.Filter);
            _stopwatch.Start();
        }

        public void EndRequest()
        {
            _stopwatch.Stop();

            var behavior = _behaviorFactory.GetBehaviorFor(_application);
            if (behavior.IgnoreActivity)
            {
                return;
            }

            var activity = _collector.Collect(_application, behavior,
                new Activity
                    {
                        Duration = _stopwatch.Elapsed,
                        ApplicationId = _applicationId
                    });


            Task.Factory.StartNew(() => _processor.Process(activity));
        }
    }
}