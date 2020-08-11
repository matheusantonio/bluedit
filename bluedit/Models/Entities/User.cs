using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using AspNetCore.Identity.Mongo.Model;

namespace bluedit.Models.Entities
{
    public class User : MongoUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override ObjectId Id {get; set;}

        public override string UserName {get; set;}
        
        public override string Email {get; set;}
    }
}