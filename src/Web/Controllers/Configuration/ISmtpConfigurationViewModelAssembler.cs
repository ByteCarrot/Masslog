using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Configuration
{
    public interface ISmtpConfigurationViewModelAssembler
    {
        SmtpConfigurationViewModel ToViewModel(SmtpConfiguration entity);

        void Update(SmtpConfigurationViewModel model, SmtpConfiguration entity);
    }
}