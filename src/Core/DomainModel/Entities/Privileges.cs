using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Privileges
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ApplicationId { get; set; }

        public bool CanModify { get; set; }
    }
}