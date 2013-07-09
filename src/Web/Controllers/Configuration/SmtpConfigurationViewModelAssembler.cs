using AutoMapper;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Configuration
{
    public class SmtpConfigurationViewModelAssembler : ISmtpConfigurationViewModelAssembler
    {
        static SmtpConfigurationViewModelAssembler()
        {
            Mapper.CreateMap<SmtpConfiguration, SmtpConfigurationViewModel>();
        }

        public SmtpConfigurationViewModel ToViewModel(SmtpConfiguration entity)
        {
            return Mapper.Map<SmtpConfigurationViewModel>(entity);
        }

        public void Update(SmtpConfigurationViewModel model, SmtpConfiguration entity)
        {
            Mapper.Map(model, entity);
        }
    }
}