using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bluedit.Models.Entities
{
    public class Upvote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId PostId;
        
        public bool IsUpvote;
    }
}