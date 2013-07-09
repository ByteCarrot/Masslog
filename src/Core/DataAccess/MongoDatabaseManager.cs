using MongoDB.Driver;
using System.Collections.Generic;
using ByteCarrot.Masslog.Core.Infrastructure.Configuration;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class MongoDatabaseManager : IMongoDatabaseManager
    {
        private readonly MongoClient _client;
        private readonly MongoServer _server;
        private readonly MongoDatabase _database;

        public MongoDatabaseManager(IMasslogConfiguration configuration)
        {
            MongoDriverMonitor.Enabled = true;

            _client = new MongoClient(configuration.ConnectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase(configuration.DatabaseName);
        }

        public void Drop()
        {
            _database.Drop();
        }

        public MongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
        {
            return _database.GetCollection<TEntity>(collectionName);
        }

        public IEnumerable<string> GetCollectioNames()
        {
            return _database.GetCollectionNames();
        }
    }
}