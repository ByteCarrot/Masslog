using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Page<User> Search(UserQuery query);

        User FindByCredentials(string username, string password);

        User FindByUsername(string username);

        IList<User> FindWithPrivilegesTo(string applicationId);
    }
}