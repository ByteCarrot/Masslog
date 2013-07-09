using System.Web.Mvc;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure.Security;
using StructureMap.Attributes;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class AppendLayoutViewModel : ActionFilterAttribute
    {
        [SetterProperty]
        public ISecurityContext Security { get; set; }

        [SetterProperty]
        public IDomainContext Context { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var applications = Context.Applications.In(Security.ReadAccess);
            filterContext.Controller.ViewData["layout"] = LayoutViewModel.Create(applications, Security);
        }
    }
}