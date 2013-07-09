using ByteCarrot.Masslog.Web.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace ByteCarrot.Masslog.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(Routes.Default, "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = (string)null });
        }
    }
}