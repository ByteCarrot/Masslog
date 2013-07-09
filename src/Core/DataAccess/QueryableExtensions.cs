using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> q, bool include, Expression<Func<TSource, bool>> predicate)
        {
            return include ? q.Where(predicate) : q;
        }

        public static IMongoQuery ToMongoQuery<TSource>(this IQueryable<TSource> q)
        {
            return ((MongoQueryable<TSource>) q).GetMongoQuery();
        }
    }
}