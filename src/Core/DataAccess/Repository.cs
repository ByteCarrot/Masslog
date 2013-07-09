using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly MongoCollection<TEntity> _collection;

        protected Repository(MongoCollection<TEntity> collection)
        {
            _collection = collection;
        }

        protected IQueryable<TEntity> Queryable()
        {
            return _collection.AsQueryable();
        }

        public TEntity Get(string id)
        {
            return Queryable().SingleOrDefault(x => x.Id == id);
        }

        public void Save(TEntity entity)
        {
            _collection.Save(entity);
        }

        public void Save(IEnumerable<TEntity> entities)
        {
            entities.ForEach(x => _collection.Save(x));
        }

        public void Delete(TEntity entity)
        {
            _collection.Remove(Queryable().Where(x => x.Id == entity.Id).ToMongoQuery());
        }

        public long Count()
        {
            return _collection.Count();
        }

        public IList<TEntity> All()
        {
            return Queryable().ToList();
        }

        protected Page<TEntity> ApplySortingAndPaging(QueryBase query, IQueryable<TEntity> cursor)
        {
            return ApplySortingAndPaging(query, _collection.Find(cursor.ToMongoQuery()));
        }

        private static Page<TEntity> ApplySortingAndPaging(QueryBase query, MongoCursor<TEntity> cursor)
        {
            cursor.SetSortOrder(
                query.SortDirection == SortDirection.Ascending
                    ? SortBy.Ascending(query.SortColumn)
                    : SortBy.Descending(query.SortColumn));

            if (query.PageNumber > 1)
            {
                cursor.SetSkip((query.PageNumber - 1)*query.PageSize);
            }
            cursor.SetLimit(query.PageSize);

            return new Page<TEntity>(cursor.ToList(), cursor.Count(), query.PageSize, query.PageNumber);
        }
    }
}