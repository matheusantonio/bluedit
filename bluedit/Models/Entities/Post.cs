using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bluedit.Models.Entities
{
    public class Post : Postable
    {
        public string Title {get; set;}

        public List<string> Tags {get; set;}

        public ObjectId SubForumId {get; set;}
    }
    
}