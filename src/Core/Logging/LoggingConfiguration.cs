using ByteCarrot.Masslog.Core.Infrastructure.Configuration;

namespace ByteCarrot.Masslog.Core.Logging
{
    public sealed class LoggingConfiguration : MasslogConfiguration, ILoggingConfiguration
    {
        public LoggingConfiguration()
        {
            ApplicationId = GetStringSetting("ApplicationId");
            InternalLogLevel = GetStringSetting("InternalLogLevel", "Debug");
            InternalLogLocation = GetStringSetting("InternalLogLocation", @"App_Data\masslog.log");
        }

        public string ApplicationId { get; private set; }

        public string InternalLogLevel { get; private set; }

        public string InternalLogLocation { get; private set; }
    }
}