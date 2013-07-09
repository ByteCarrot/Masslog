using System;
using System.Collections.Generic;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class EditUserViewModelAssembler : IEditUserViewModelAssembler
    {
        private readonly IPrivilegesViewModelAssembler _privilegesAssembler;

        public EditUserViewModelAssembler(IPrivilegesViewModelAssembler privilegesAssembler)
        {
            _privilegesAssembler = privilegesAssembler;
        }

        public EditViewModel ToViewModel(User user)
        {
            return new EditViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsAdministrator = user.IsAdministrator,
                Privileges = _privilegesAssembler.ToViewModels(user.Privileges)
            };
        }

        public void Update(EditViewModel model, User user)
        {
            if (model.Id != user.Id)
            {
                throw new InvalidOperationException("Entity cannot be updated because of Id mismatch");
            }

            user.Username = model.Username;
            user.Password = model.Password.NotEmpty() 
                ? model.Password.Md5Hash() 
                : user.Password;
            user.IsAdministrator = model.IsAdministrator;
            user.Email = model.Email;
            user.Privileges = model.IsAdministrator 
                ? new List<Privileges>() 
                : _privilegesAssembler.ToEntities(model.Privileges);
        }
    }
}