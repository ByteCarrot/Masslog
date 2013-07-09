using AutoMapper;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class RequestViewModel : ViewModel
    {
        static RequestViewModel()
        {
            Mapper.CreateMap<Request, RequestViewModel>()
                .ForMember(m => m.Headers, o => o.MapFrom(r => ToNameValueListViewModels(r)))
                .ForMember(m => m.Body, o => o.MapFrom(r => GetBody(r)));
        }

        private static string GetBody(Request request)
        {
            if (request.BodyIgnored)
            {
                return "Body ignored";
            }

            if (request.Body.Empty())
            {
                return "No body";
            }

            try
            {
                if (request.ContentType.Contains("json", StringComparison.InvariantCultureIgnoreCase))
                {
                    return request.Body.ToFormattedJson();
                }

                if (request.ContentType.Contains("xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    return request.Body.ToFormattedXml();
                }
            }
            catch
            {
                return request.Body;
            }

            return request.Body;
        }

        private static List<NameValueListViewModel> ToNameValueListViewModels(Request request)
        {
            return request.Headers
                .OrderBy(h => h.Name)
                .Select(h => new NameValueListViewModel(h.Name, h.Value))
                .ToList();
        }

        public static RequestViewModel Create(Request request)
        {
            return Mapper.Map<RequestViewModel>(request);
        }

        public string Id { get; set; }

        public List<NameValueListViewModel> Headers { get; set; }

        public string Body { get; set; }

        public string HttpMethod { get; set; }

        public string Protocol { get; set; }

        public string RawUrl { get; set; }

        public string ContentType { get; set; }

        public string ContentEncoding { get; set; }

        public string ContentLength { get; set; }
    }
}