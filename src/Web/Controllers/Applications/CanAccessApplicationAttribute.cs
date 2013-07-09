using ByteCarrot.Masslog.Web.Controllers.Applications;
using ByteCarrot.Masslog.Web.Infrastructure;
using ByteCarrot.Masslog.Web.Infrastructure.Security;
using StructureMap.Attributes;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class CanAccessApplicationAttribute : CanAttribute
    {
        [SetterProperty]
        public ISession Session { get; set; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var id = httpContext.Request.RequestContext.RouteData.Values["id"] as string;
            if (id == null)
            {
                var state = Session.Get<ApplicationState>();
                if (state == null)
                {
                    return false;
                }
                id = state.ApplicationId;
            }
            return Security.CanAccessApplication(id);
        }
    }
}