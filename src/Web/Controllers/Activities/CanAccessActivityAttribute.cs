using ByteCarrot.Masslog.Web.Controllers.Activities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;
using ByteCarrot.Masslog.Web.Infrastructure.Security;
using StructureMap.Attributes;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class CanAccessActivityAttribute : CanAttribute
    {
        [SetterProperty]
        public ISession Session { get; set; }

        [SetterProperty]
        public IDomainContext Context { get; set; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var id = httpContext.Request.RequestContext.RouteData.Values["id"] as string;
            if (id == null)
            {
                var state = Session.Get<ActivityState>();
                return state != null && Security.CanAccessApplication(state.Activity.ApplicationId);
            }

            var activity = Context.Activities.Get(id);
            return activity != null && Security.CanAccessApplication(activity.ApplicationId);
        }
    }
}