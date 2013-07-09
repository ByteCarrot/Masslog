using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public abstract class ConfigurationEntity
    {
        protected ConfigurationEntity()
        {
            Id = GetType().Name;
        }

        [BsonId, BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
    }
}