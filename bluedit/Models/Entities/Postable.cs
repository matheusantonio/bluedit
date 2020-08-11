using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System.Collections.Generic;
using System;

namespace bluedit.Models.Entities{
    public abstract class Postable {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id {get; set;}

        public ObjectId AuthorId {get; set;}

        public string Content {get; set;}

        public List<ObjectId> Replies {get; set;}

        public DateTime Time {get; set;}

        public int Upvotes {get; set;}
    }
}