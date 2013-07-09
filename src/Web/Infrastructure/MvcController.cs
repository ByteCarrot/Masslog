using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure.Security;
using StructureMap;
using StructureMap.Attributes;
using System;
using System.Web;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class MvcController : Controller
    {
        [SetterProperty]
        public IDomainContext Context { get; set; }

        [SetterProperty]
        public ISecurityContext Security { get; set; }

        [SetterProperty]
        public new ISession Session { get; set; }

        [SetterProperty]
        public IContainer Container { get; set; }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var cache = filterContext.HttpContext.Response.Cache;
            cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            cache.SetValidUntilExpires(false);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }

        protected RedirectToRouteResult RedirectWithMessage(string actionName, string message)
        {
            TempData.Add("RedirectedMessage", message);
            return RedirectToAction(actionName);
        }

        protected string GetRedirectedMessage()
        {
            if (!TempData.ContainsKey("RedirectedMessage"))
            {
                return null;
            }
            return TempData["RedirectedMessage"] as string;
        }
    }
}