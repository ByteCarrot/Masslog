using AutoMapper;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions.Json;
using System;
using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class ResponseViewModel : ViewModel
    {
        static ResponseViewModel()
        {
            Mapper.CreateMap<Response, ResponseViewModel>()
                .ForMember(m => m.Headers, o => o.MapFrom(r => r.Headers.ToNameValueListViewModels()))
                .ForMember(m => m.Body, o => o.MapFrom(r => GetBody(r)));
        }

        private static string GetBody(Response response)
        {
            if (response.BodyIgnored)
            {
                return "Body ignored";
            }

            if (response.Body.Empty())
            {
                return "No body";
            }

            try
            {
                if (response.ContentType.Contains("json", StringComparison.InvariantCultureIgnoreCase))
                {
                    return response.Body.ToFormattedJson();
                }

                if (response.ContentType.Contains("xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    return response.Body.ToFormattedXml();
                }
            }
            catch
            {
                return response.Body;
            }

            return response.Body;
        }

        public static ResponseViewModel Create(Response response)
        {
            return Mapper.Map<ResponseViewModel>(response);
        }

        public string Id { get; set; }

        public List<NameValueListViewModel> Headers { get; set; }

        public string Body { get; set; }

        public string StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string ContentType { get; set; }

        public string ContentEncoding { get; set; }

        public string ContentLength { get; set; }
    }
}