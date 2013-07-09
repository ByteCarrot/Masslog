using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging.Updaters
{
    public class ResponseActivityUpdater : IActivityUpdater
    {
        public void Update(DataCollectionContext context)
        {
            var application = context.Application;
            var activity = context.Activity;
            var behavior = context.Behavior;
            var response = application.Response;

            activity.EndedAt = new DenormalizedDateTime(DateTime.UtcNow);
            activity.StatusCode = response.StatusCode;

            var ignore = behavior.IgnoreResponseBody || response.Filter.Length > 1048576;
            activity.Response = new Response
            {
                Body = ignore ? null : response.Filter.ToString(),
                BodyIgnored = ignore,
                Headers = GetHeaders(response),
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription,
                SubStatusCode = GetSubStatusCode(response),
                ContentType = response.ContentType,
                ContentEncoding = response.ContentEncoding.WebName,
                ContentLength = response.Filter.Length
            };

            if (activity.Status == ActivityStatus.Failure)
            {
                return;
            }

            if (application.Response.StatusCode < 400)
            {
                activity.SetStatusToSuccess();
                return;
            }
                
            activity.SetStatusToFailure(FailureDeterminedBy.HttpCode);
        }

        private static List<NameValue> GetHeaders(HttpResponse response)
        {
            try
            {
                return response.Headers.AllKeys
                    .Select(x => new NameValue(x, response.Headers[x]))
                    .ToList();
            }
            catch (PlatformNotSupportedException)
            {
                return new List<NameValue>();
            }
        }

        private static int? GetSubStatusCode(HttpResponse response)
        {
            try
            {
                return response.SubStatusCode;
            }
            catch (PlatformNotSupportedException)
            {
                return null;
            }
        }
    }
}