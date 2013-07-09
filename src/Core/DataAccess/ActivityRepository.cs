using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using MongoDB.Driver;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(MongoCollection<Activity> collection) : base(collection)
        {
        }

        public Page<Activity> Search(ActivityQuery query)
        {
            var linq = Queryable()
                .Where(query.DateFrom != null, x => x.StartedAt.Date >= query.DateFrom.Value)
                .Where(query.DateTo != null, x => x.StartedAt.Date <= query.DateTo.Value)
                .Where(query.ApplicationId.NotEmpty(), x => x.ApplicationId == query.ApplicationId)
                .Where(query.Machine.NotEmpty(), x => x.Machine.ToLower().Contains(query.Machine.Trim()))
                .Where(query.User.NotEmpty(), x => x.User.ToLower().Contains(query.User.Trim()))
                .Where(query.HostName.NotEmpty(), x => x.HostName.ToLower().Contains(query.HostName.Trim()))
                .Where(query.Url.NotEmpty(), x => x.Url.ToLower().Contains(query.Url.Trim()))
                .Where(query.StatusCode.HasValue, x => x.StatusCode == query.StatusCode)
                .Where(query.Status.HasValue, x => x.Status == query.Status.Value)
                .Where(query.FailureDeterminedBy.HasValue, x => x.FailureDeterminedBy == query.FailureDeterminedBy);

            return ApplySortingAndPaging(query, linq);
        }
    }
}