using MvcContrib.UI.Grid;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class ListViewModel : ViewModel
    {
        public string Message { get; set; }

        public Pagination<ListItemViewModel> Items { get; set; }

        public GridSortOptions SortOptions { get; set; }

        public static ListViewModel Create(ApplicationQuery query, Page<Application> applications)
        {
            return new ListViewModel
            {
                SortOptions = new GridSortOptions
                {
                    Column = query.SortColumn,
                    Direction = (MvcContrib.Sorting.SortDirection)query.SortDirection
                },
                Items = ListItemViewModel.Create(applications)
            };
        }
    }
}