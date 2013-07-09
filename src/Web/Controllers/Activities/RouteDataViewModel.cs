using System.Collections.Generic;
using AutoMapper;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class RouteDataViewModel : ViewModel
    {
        static RouteDataViewModel()
        {
            Mapper.CreateMap<RouteData, RouteDataViewModel>()
                .ForMember(m => m.Constraints, o => o.MapFrom(rd => rd.Constraints.ToNameValueListViewModels()))
                .ForMember(m => m.DataTokens, o => o.MapFrom(rd => rd.DataTokens.ToNameValueListViewModels()))
                .ForMember(m => m.Defaults, o => o.MapFrom(rd => rd.Defaults.ToNameValueListViewModels()))
                .ForMember(m => m.Values, o => o.MapFrom(rd => rd.Values.ToNameValueListViewModels()));
        }

        public static RouteDataViewModel Create(Activity activity)
        {
            var viewModel = new RouteDataViewModel
                                {
                                    Id = activity.Id, 
                                    RouteDataIgnored = activity.RouteDataIgnored
                                };
            Mapper.Map(activity.RouteData, viewModel);
            return viewModel;
        }

        public RouteDataViewModel()
        {
            Constraints = new List<NameValueListViewModel>();
            DataTokens = new List<NameValueListViewModel>();
            Defaults = new List<NameValueListViewModel>();
            Values = new List<NameValueListViewModel>();
        }

        public string Id { get; set; }

        public bool RouteDataIgnored { get; set; }

        public string Template { get; set; }

        public string Name { get; set; }

        public List<NameValueListViewModel> Constraints { get; set; }

        public List<NameValueListViewModel> DataTokens { get; set; }

        public List<NameValueListViewModel> Defaults { get; set; }

        public List<NameValueListViewModel> Values { get; set; }
    }
}