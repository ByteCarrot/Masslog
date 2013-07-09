using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Get(string id);

        void Save(TEntity entity);

        void Save(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        long Count();

        IList<TEntity> All();
    }
}