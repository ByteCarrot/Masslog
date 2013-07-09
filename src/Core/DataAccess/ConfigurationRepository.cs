using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IMongoDatabaseManager _manager;

        public ConfigurationRepository(IMongoDatabaseManager manager)
        {
            _manager = manager;
        }

        private TEntity GetConfiguration<TEntity>() where TEntity : ConfigurationEntity
        {
            return _manager.GetCollection<TEntity>("configuration").FindOneById(typeof (TEntity).Name);
        }

        public SmtpConfiguration GetSmtpConfiguration()
        {
            var c = GetConfiguration<SmtpConfiguration>();
            if (c == null)
            {
                c = SmtpConfiguration.Default;
                Save(c);
            }
            return c;
        }

        public void Save<TEntity>(TEntity entity) where TEntity : ConfigurationEntity
        {
            _manager.GetCollection<TEntity>("configuration").Save(entity);
        }
    }
}