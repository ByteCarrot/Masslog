using System.Collections.Generic;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class AddUserViewModelAssembler : IAddUserViewModelAssembler
    {
        private readonly IPrivilegesViewModelAssembler _privilegesAssembler;

        public AddUserViewModelAssembler(IPrivilegesViewModelAssembler privilegesAssembler)
        {
            _privilegesAssembler = privilegesAssembler;
        }

        public User ToEntity(AddViewModel model)
        {
            return new User
            {
                Username = model.Username,
                Password = model.Password.Md5Hash(),
                Email = model.Email,
                IsAdministrator = model.IsAdministrator,
                Privileges = model.IsAdministrator 
                    ? new List<Privileges>() 
                    : _privilegesAssembler.ToEntities(model.Privileges)
            };
        }

        public AddViewModel ToViewModel()
        {
            return new AddViewModel { Privileges = _privilegesAssembler.ToViewModels() };
        }
    }
}