using AutoMapper;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class UserViewModel : ViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public bool IsAdministrator { get; set; }

        static UserViewModel()
        {
            Mapper.CreateMap<User, UserViewModel>();
        }

        public static UserViewModel Create(User user)
        {
            return Mapper.Map<UserViewModel>(user);
        }

        public static Pagination<UserViewModel> Create(Page<User> page)
        {
            return new Pagination<UserViewModel>(
                page.Items.Select(Create).ToList(), 
                (int) page.TotalItems, 
                page.PageSize, 
                page.PageNumber);
        }
    }
}