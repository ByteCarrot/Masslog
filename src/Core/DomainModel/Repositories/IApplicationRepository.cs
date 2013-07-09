using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Page<Application> Search(ApplicationQuery query);

        IList<Application> In(IEnumerable<string> ids);

        Application FindByName(string name);
    }
}