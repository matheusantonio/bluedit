using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bluedit.Models.Entities
{
    public class SubForum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id {get; set;}

        public ObjectId creatorId {get; set;}

        public string Name {get; set;}

        public string Descrition {get; set;}
    }
}