using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class SearchResultViewModel : ViewModel
    {
        public IPagination<SearchResultItemViewModel> Items { get; set; }

        public GridSortOptions SortOptions { get; set; }

        public static SearchResultViewModel Create(ActivityQuery query, Page<Activity> actions)
        {
            return new SearchResultViewModel
                       {
                           SortOptions = new GridSortOptions
                                             {
                                                 Column = query.SortColumn, 
                                                 Direction = (MvcContrib.Sorting.SortDirection)query.SortDirection
                                             },
                           Items = SearchResultItemViewModel.Create(actions)
                       };
        }
    }
}