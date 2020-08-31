using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bluedit.Models.Entities
{
    public class Upvote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId {get; set;}

        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId {get; set;}
        
        public bool IsUpvote {get; set;}
    }
}