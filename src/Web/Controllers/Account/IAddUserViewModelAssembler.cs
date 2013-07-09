using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public interface IAddUserViewModelAssembler
    {
        User ToEntity(AddViewModel model);

        AddViewModel ToViewModel();
    }
}