using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bluedit.Models.Entities
{
    public class Upvote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId;
        
        public bool IsUpvote;
    }
}