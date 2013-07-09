using ByteCarrot.Masslog.Core.Infrastructure.Configuration;
using ByteCarrot.Masslog.Core.Logging.Behaviors;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;

namespace ByteCarrot.Masslog.Core.Logging
{
    public static class LoggingModuleIoC
    {
        private static readonly object LockObject = new Object();
        private volatile static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                    lock (LockObject)
                        if (_container == null)
                        {
                            _container = ConfigureNewContainer();
                        }
                return _container;
            }
        }

        private static Container ConfigureNewContainer()
        {
            return new Container(registry =>
            {
                registry.Scan(s =>
                {
                    s.AssemblyContainingType<LoggingModule>();
                    s.LookForRegistries();
                    s.WithDefaultConventions();
                });
                ConfigureModuleSpecificDependencies(registry);
            });
        }

        private static void ConfigureModuleSpecificDependencies(Registry registry)
        {
            registry.For<IMasslogConfiguration>().Singleton().Use<LoggingConfiguration>();
            registry.For<ILoggingConfiguration>().Singleton().Use<LoggingConfiguration>();
            registry.For<IMonitorBehaviorFactory>().Singleton().Use<MonitorBehaviorFactory>();
            registry.For<IMonitor>().HttpContextScoped().Use<Monitor>();
        }
    }
}