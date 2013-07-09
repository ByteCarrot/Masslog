namespace ByteCarrot.Masslog.Core.Logging.Behaviors
{
    public interface IMonitorBehavior
    {
        bool IgnoreActivity { get; }

        bool IgnoreRequestBody { get; }

        bool IgnoreResponseBody { get; }

        bool IgnoreServerVariables { get; }

        bool IgnoreRouteData { get; }
    }
}