using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(MongoCollection<Application> collection) : base(collection)
        {
        }

        public Page<Application> Search(ApplicationQuery query)
        {
            return ApplySortingAndPaging(query, Queryable());
        }

        public IList<Application> In(IEnumerable<string> ids)
        {
            return Queryable().Where(x => ids.Contains(x.Id)).ToList();
        }

        public Application FindByName(string name)
        {
            return Queryable().SingleOrDefault(x => x.Name == name);
        }
    }
}