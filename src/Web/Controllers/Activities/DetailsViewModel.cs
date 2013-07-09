using AutoMapper;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class DetailsViewModel : ViewModel
    {
        static DetailsViewModel()
        {
            Mapper.CreateMap<Activity, DetailsViewModel>()
                .ForMember(m => m.Status, o => o.MapFrom(a => a.GetStatusInfo()))
                .ForMember(m => m.Duration, o => o.MapFrom(a => a.Duration.ToTimeString()));
        }

        public static DetailsViewModel Create(Activity activity)
        {
            return Mapper.Map<DetailsViewModel>(activity);
        }

        public string Id { get; set; }

        public string Url { get; set; }

        public string LocalStartedAt { get; set; }

        public string LocalEndedAt { get; set; }

        public string Duration { get; set; }

        public string UserHost { get; set; }

        public string Status { get; set; }

        public string Machine { get; set; }

        public string MachineFqn { get; set; }

        public string User { get; set; }

        public string HostName { get; set; }

        public string StatusCode { get; set; }
    }
}