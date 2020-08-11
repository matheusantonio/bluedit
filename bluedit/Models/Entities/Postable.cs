using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System.Collections.Generic;
using System;

namespace bluedit.Models.Entities{
    public abstract class Postable {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}

        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId {get; set;}

        public string Content {get; set;}

        public List<string> Replies {get; set;}

        public DateTime Time {get; set;}

        public int Upvotes {get; set;}
    }
}