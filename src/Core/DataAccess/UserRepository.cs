using System.Collections.Generic;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using MongoDB.Driver;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MongoCollection<User> collection) : base(collection)
        {
        }

        public Page<User> Search(UserQuery query)
        {
            return ApplySortingAndPaging(query, Queryable());
        }

        public User FindByCredentials(string username, string password)
        {
            password = password.Md5Hash();
            return Queryable().SingleOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }

        public User FindByUsername(string username)
        {
            return Queryable().SingleOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public IList<User> FindWithPrivilegesTo(string applicationId)
        {
            return Queryable().Where(x => x.Privileges.Any(p => p.ApplicationId == applicationId)).ToList();
        }
    }
}