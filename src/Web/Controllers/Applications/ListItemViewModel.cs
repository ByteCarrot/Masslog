using AutoMapper;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class ListItemViewModel : ViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        static ListItemViewModel()
        {
            Mapper.CreateMap<Application, ListItemViewModel>();
        }

        public static ListItemViewModel Create(Application application)
        {
            return Mapper.Map<ListItemViewModel>(application);
        }

        public static Pagination<ListItemViewModel> Create(Page<Application> page)
        {
            return new Pagination<ListItemViewModel>(
                page.Items.Select(Create).ToList(),
                (int) page.TotalItems,
                page.PageSize,
                page.PageNumber);
        }
    }
}