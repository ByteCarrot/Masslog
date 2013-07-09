using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Page<Activity> Search(ActivityQuery query);
    }
}