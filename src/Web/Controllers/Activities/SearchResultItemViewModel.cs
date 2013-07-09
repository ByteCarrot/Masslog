using AutoMapper;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class SearchResultItemViewModel
    {
        static SearchResultItemViewModel()
        {
            Mapper.CreateMap<Activity, SearchResultItemViewModel>()
                .ForMember(o => o.StartedAt, c => c.MapFrom(a => a.LocalStartedAt))
                .ForMember(o => o.Duration, c => c.MapFrom(a => a.Duration.ToTimeString()))
                .ForMember(o => o.Status, c => c.MapFrom(a => a.GetStatusInfo()))
                .ForMember(o => o.Failure, c => c.MapFrom(a => a.Status == ActivityStatus.Failure));
        }

        public string Id { get; set; }

        public string Machine { get; set; }

        public string StatusCode { get; set; }

        public string HostName { get; set; }

        public string Url { get; set; }

        public string StartedAt { get; set; }

        public string Duration { get; set; }

        public string Status { get; set; }

        public bool Failure { get; set; }

        public string User { get; set; }

        public static SearchResultItemViewModel Create(Activity activity)
        {
            return Mapper.Map<SearchResultItemViewModel>(activity);
        }

        public static Pagination<SearchResultItemViewModel> Create(Page<Activity> page)
        {
            return new Pagination<SearchResultItemViewModel>(
                page.Items.Select(Create).ToList(), 
                (int) page.TotalItems, 
                page.PageSize, 
                page.PageNumber);
        }
    }
}