using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Entity
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}