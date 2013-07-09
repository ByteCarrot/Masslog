namespace ByteCarrot.Masslog.Core.Logging.Behaviors
{
    public class MonitorBehavior : IMonitorBehavior
    {
        public bool IgnoreActivity { get; set; }

        public bool IgnoreRequestBody { get; set; }

        public bool IgnoreResponseBody { get; set; }

        public bool IgnoreServerVariables { get; set; }

        public bool IgnoreRouteData { get; set; }
    }
}