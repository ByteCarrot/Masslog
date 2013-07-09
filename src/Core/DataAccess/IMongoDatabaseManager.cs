using ByteCarrot.Masslog.Core.DomainModel.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public interface IMongoDatabaseManager
    {
        void Drop();

        MongoCollection<TEntity> GetCollection<TEntity>(string activities);
        
        IEnumerable<string> GetCollectioNames();
    }
}