using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public interface IConfigurationRepository
    {
        SmtpConfiguration GetSmtpConfiguration();

        void Save<TEntity>(TEntity entity) where TEntity : ConfigurationEntity;
    }
}