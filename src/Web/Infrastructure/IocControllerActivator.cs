using StructureMap;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class IocControllerActivator : IControllerActivator
    {
        private readonly IContainer _container;

        public IocControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController) _container.GetInstance(controllerType);
        }
    }
}