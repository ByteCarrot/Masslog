using ByteCarrot.Masslog.Web.App_Start;
using ByteCarrot.Masslog.Web.Infrastructure.Security;
using ByteCarrot.Masslog.Web.Infrastructure;
using StructureMap;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ByteCarrot.Masslog.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ObjectFactory.Configure(x => x.Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory();
                s.LookForRegistries();
                s.WithDefaultConventions();
                s.IgnoreStructureMapAttributes();
                s.Exclude(y => !y.Assembly.FullName.Contains("ByteCarrot.Masslog"));
            }));

            RegisterDependencyResolver();
            RegisterFilterProvider();

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterDependencyResolver()
        {
            DependencyResolver.SetResolver(new IocDependencyResolver(ObjectFactory.Container));
        }

        private static void RegisterFilterProvider()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider));
            FilterProviders.Providers.Add(new IocFilterProvider(ObjectFactory.Container));
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<ISecurityService>().AuthenticateRequest(Context);
        }
    }
}