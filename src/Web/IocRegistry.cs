using ByteCarrot.Masslog.Web.Infrastructure;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web
{
    public class IocRegistry : Registry
    {
        public IocRegistry()
        {
            For<IControllerActivator>().Use<IocControllerActivator>();
            For<IDependencyResolver>().Use<IocDependencyResolver>();
        }
    }
}
