using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;

[assembly: XmlConfigurator(Watch = false)]

namespace ByteCarrot.Masslog.Core.Logging
{
    public class Log4NetManager
    {
        private static readonly object LockObject = new object();
        private volatile static bool _configured;
        public const string RepositoryName = "LoggingModule";
        
        private readonly ILoggingConfiguration _configuration;

        public Log4NetManager(ILoggingConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure()
        {
            if (_configured) return;

            lock (LockObject)
            {
                if (_configured) return;

                var logger = (Hierarchy) LogManager.CreateRepository(RepositoryName);
                logger.Root.RemoveAllAppenders();
                logger.Root.Level = GetLevel();

                var layout = new log4net.Layout.PatternLayout
                {
                    ConversionPattern =
                        "%date [%thread] %-5level %logger [%property] - %message%newline"
                };
                layout.ActivateOptions();

                var fileAppender = new FileAppender
                {
                    AppendToFile = true,
                    LockingModel = new FileAppender.MinimalLock(),
                    File = _configuration.InternalLogLocation,
                    Layout = layout,
                    ImmediateFlush = true
                };
                fileAppender.ActivateOptions();

                BasicConfigurator.Configure(logger, fileAppender);

                _configured = true;
            }
        }

        private Level GetLevel()
        {
            switch (_configuration.InternalLogLevel.Trim().ToLower())
            {
                case "debug":
                    return Level.Debug;
                case "warn":
                    return Level.Warn;
                case "error":
                    return Level.Error;
                default:
                    return Level.All;
            }
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(RepositoryName, type);
        }
    }
}
