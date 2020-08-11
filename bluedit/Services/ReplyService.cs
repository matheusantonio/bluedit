using bluedit.Models.Entities;
using bluedit.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace bluedit.Services
{
    public class ReplyService
    {
        private readonly IMongoCollection<Reply> _replies;

        public ReplyService(IBlueditDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _replies = database.GetCollection<Reply>(settings.ReplyCollectionName);
        }

        public List<Reply> Get() =>
            _replies.Find(reply => true).ToList();
        
        public Reply Get(ObjectId id) =>
            _replies.Find<Reply>(reply => reply.Id == id).FirstOrDefault();

        public Reply Create(Reply reply)
        {
            _replies.InsertOne(reply);
            return reply;
        }

        public void Update(ObjectId id, Reply replyIn) =>
            _replies.ReplaceOne(reply => reply.Id == id, replyIn);
        
        public void Remove(Reply replyIn) =>
            _replies.DeleteOne(reply => reply.Id == replyIn.Id);
        
        public void Remove(ObjectId id) =>
            _replies.DeleteOne(reply => reply.Id == id);
        

    }
}