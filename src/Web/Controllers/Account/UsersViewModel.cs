using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class UsersViewModel : ViewModel
    {
        public string CurrentUsername { get; set; }

        public IPagination<UserViewModel> Items { get; set; }

        public GridSortOptions SortOptions { get; set; }

        public string Message { get; set; }

        public static UsersViewModel Create(UserQuery query, Page<User> users)
        {
            return new UsersViewModel
            {
                SortOptions = new GridSortOptions
                {
                    Column = query.SortColumn,
                    Direction = (MvcContrib.Sorting.SortDirection)query.SortDirection
                },
                Items = UserViewModel.Create(users)
            };
        }
    }
}