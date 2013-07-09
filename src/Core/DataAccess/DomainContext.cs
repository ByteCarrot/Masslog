using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class DomainContext : IDomainContext
    {
        private readonly IMongoDatabaseManager _manager;

        public DomainContext(IMongoDatabaseManager manager)
        {
            _manager = manager;
        }

        public void Reset()
        {
            _manager.Drop();
        }

        public IActivityRepository Activities
        {
            get { return new ActivityRepository(_manager.GetCollection<Activity>("activities")); }
        }

        public IUserRepository Users
        {
            get { return new UserRepository(_manager.GetCollection<User>("users")); }
        }

        public IApplicationRepository Applications
        {
            get { return new ApplicationRepository(_manager.GetCollection<Application>("applications")); }
        }

        public IConfigurationRepository Configuration 
        { 
            get { return new ConfigurationRepository(_manager); }
        }
    }
}
