using ByteCarrot.Masslog.Core.Infrastructure.Configuration;

namespace ByteCarrot.Masslog.Core.Logging
{
    public interface ILoggingConfiguration : IMasslogConfiguration
    {
        string ApplicationId { get; }

        string InternalLogLevel { get; }

        string InternalLogLocation { get; }
    }
}
