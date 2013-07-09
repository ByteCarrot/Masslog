using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using RouteData = ByteCarrot.Masslog.Core.DomainModel.Entities.RouteData;

namespace ByteCarrot.Masslog.Core.Logging.Updaters
{
    public class RouteDataActivityUpdater : IActivityUpdater
    {
        public void Update(DataCollectionContext context)
        {
            var requestRouteData = context.Application.Context.Request.RequestContext.RouteData;
            if (requestRouteData == null || requestRouteData.Route == null)
            {
                return;
            }

            if (context.Behavior.IgnoreRouteData)
            {
                context.Activity.RouteDataIgnored = true;
                return;
            }

            var routeData = new DomainModel.Entities.RouteData();
            var route = requestRouteData.Route as Route;
            if (route == null)
            {
                return;
            }
            
            routeData.Name = Name(route);
            routeData.Template = route.Url;
            routeData.Constraints = ToList(route.Constraints);
            routeData.DataTokens = ToList(route.DataTokens);
            routeData.Defaults = ToList(route.Defaults);
            routeData.Values = ToList(requestRouteData.Values);
            context.Activity.RouteData = routeData;
        }

        private static string Name(RouteBase original)
        {
            if (RouteTable.Routes.Contains(original))
            {
                var field = typeof(RouteCollection).GetField("_namedMap", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field == null)
                {
                    return null;
                }

                var value = field.GetValue(RouteTable.Routes) as Dictionary<string, RouteBase>;
                if (value == null)
                {
                    return null;
                }

                var query = from pair in value where pair.Value == original select pair.Key;
                return query.SingleOrDefault();
            }

            return string.Empty;
        }

        private static List<NameValue> ToList(IEnumerable<KeyValuePair<string, object>> dict)
        {
            return dict == null ? new List<NameValue>() : dict.Select(kvp => new NameValue(kvp.Key, kvp.Value as string ?? "<null>")).ToList();
        }
    }
}