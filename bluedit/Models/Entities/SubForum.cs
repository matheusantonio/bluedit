using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bluedit.Models.Entities
{
    public class SubForum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}

        [BsonRepresentation(BsonType.ObjectId)]
        public string creatorId {get; set;}

        public string Name {get; set;}

        public string Descrition {get; set;}
    }
}