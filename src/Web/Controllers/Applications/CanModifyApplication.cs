using ByteCarrot.Masslog.Web.Infrastructure;
using ByteCarrot.Masslog.Web.Infrastructure.Security;
using StructureMap.Attributes;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class CanModifyApplication : CanAttribute
    {
        [SetterProperty]
        public ISession Session { get; set; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var state = Session.Get<ApplicationState>();
            return state != null && Security.CanModifyApplication(state.ApplicationId);
        }
    }
}