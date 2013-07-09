using System.Web;
using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.Logging.Behaviors;
using ByteCarrot.Masslog.Core.Logging.Updaters;

namespace ByteCarrot.Masslog.Core.Logging
{
    public class DataCollector : IDataCollector
    {
        private static readonly List<IActivityUpdater> RequestUpdaters =
            new List<IActivityUpdater>
                {
                    new ServerVariablesActivityUpdater(),
                    new RequestActivityUpdater(),
                    new RouteDataActivityUpdater()
                };

        private static readonly List<IActivityUpdater> ResponseUpdaters =
            new List<IActivityUpdater>
                {
                    new ResponseActivityUpdater()
                };

        private static void CollectRequestData(DataCollectionContext context)
        {
            RequestUpdaters.ForEach(x => x.Update(context));
        }

        private static void CollectAuthenticationData(DataCollectionContext context)
        {
            if (context.Application.User != null)
            {
                context.Activity.User = context.Application.User.Identity.Name;
            }
        }

        private static void CollectResponseData(DataCollectionContext context)
        {
            ResponseUpdaters.ForEach(x => x.Update(context));
        }

        private static void CollectExceptionData(DataCollectionContext context)
        {
            var exception = context.Application.Server.GetLastError();
            if (exception == null)
            {
                return;
            }

            var activity = context.Activity;

            activity.SetStatusToFailure(FailureDeterminedBy.Exception);

            var entity = ToEntity(exception);
            activity.Exceptions.Add(entity);
            do
            {
                exception = exception.InnerException;
                if (exception == null)
                {
                    continue;
                }

                var parent = entity;
                entity = ToEntity(exception);
                parent.InnerException = entity;
            } while (exception != null);
        }

        private static Exception ToEntity(System.Exception exception)
        {
            var type = exception.GetType();
            return new Exception
            {
                Name = type.Name,
                Type = type.AssemblyQualifiedName,
                Message = exception.Message,
                Source = exception.Source,
                StackTrace = exception.StackTrace
            };
        }

        public Activity Collect(HttpApplication application, IMonitorBehavior behavior, Activity activity)
        {
            var context = new DataCollectionContext(activity, behavior, application);
            CollectRequestData(context);
            CollectAuthenticationData(context);
            CollectExceptionData(context);
            CollectResponseData(context);
            return activity;
        }
    }
}