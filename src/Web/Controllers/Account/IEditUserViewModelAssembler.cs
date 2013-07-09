using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public interface IEditUserViewModelAssembler
    {
        EditViewModel ToViewModel(User user);

        void Update(EditViewModel model, User user);
    }
}