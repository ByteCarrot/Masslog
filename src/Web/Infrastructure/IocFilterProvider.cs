using StructureMap;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class IocFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IContainer _container;

        public IocFilterProvider(IContainer container)
        {
            _container = container;
        }

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    _container.BuildUp(filter.Instance);
                }
            }
            
            return filters;
        }
    }
}