using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging.Updaters
{
    public class RequestActivityUpdater : IActivityUpdater
    {
        public void Update(DataCollectionContext context)
        {
            var application = context.Application;
            var activity = context.Activity;
            var behavior = context.Behavior;
            var request = application.Request;
            
            activity.HostName = request.Url.Host;
            activity.Url = request.Url.PathAndQuery;
            activity.UserHostAddress = request.UserHostAddress;
            activity.UserHostName = request.UserHostName;
            activity.Machine = application.Server.MachineName;
            activity.MachineFqn = GetMachineFullyQualifiedName();
            activity.StartedAt = new DenormalizedDateTime(DateTime.UtcNow);

            var ignore = behavior.IgnoreRequestBody || request.ContentLength > 1048576;
            activity.Request = new Request
            {
                Body = ignore ? null : request.InputStream.ReadToEnd(),
                BodyIgnored = ignore,
                Headers = GetHeaders(request),
                HttpMethod = request.HttpMethod,
                RawUrl = request.Url.OriginalString,
                Protocol = request.ServerVariables["SERVER_PROTOCOL"],
                ContentType = request.ContentType,
                ContentEncoding = request.ContentEncoding.WebName,
                ContentLength = request.ContentLength
            };
        }

        private static List<NameValue> GetHeaders(HttpRequest request)
        {
            return request.Headers.AllKeys
                .Select(x => new NameValue(x, request.Headers[x]))
                .ToList();
        }

        public static string GetMachineFullyQualifiedName()
        {
            try
            {
                var domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
                var hostName = Dns.GetHostName();
                return !hostName.Contains(domainName) ? hostName + "." + domainName : hostName;
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}