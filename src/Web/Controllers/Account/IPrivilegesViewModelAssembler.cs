using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public interface IPrivilegesViewModelAssembler
    {
        List<PrivilegesViewModel> ToViewModels(List<Privileges> entities);

        List<Privileges> ToEntities(List<PrivilegesViewModel> models);
        
        List<PrivilegesViewModel> ToViewModels();
    }
}