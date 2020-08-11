using bluedit.Models.Entities;
using bluedit.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace bluedit.Services
{
    public class UpvoteService
    {
        private readonly IMongoCollection<Upvote> _upvotes;

        public UpvoteService(IBlueditDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _upvotes = database.GetCollection<Upvote>(settings.UpvoteCollectionName);
        }

        public List<Upvote> Get() =>
            _upvotes.Find(upvote => true).ToList();

        public Upvote Get(ObjectId userId, ObjectId postId) =>
            _upvotes.Find<Upvote>(upvote => upvote.UserId == userId
                                        && upvote.PostId == postId).FirstOrDefault();
        
        public Upvote Create(Upvote upvote)
        {
            _upvotes.InsertOne(upvote);
            return upvote;
        }

        public void Update(ObjectId userId, ObjectId postId, Upvote upvoteIn) =>
            _upvotes.ReplaceOne(upvote => upvote.UserId == userId
                                        && upvote.PostId == postId, upvoteIn);
        
        public void Remove(Upvote upvoteIn) =>
            _upvotes.DeleteOne(upvote => upvote.UserId == upvoteIn.UserId
                                && upvote.PostId == upvoteIn.PostId);
    }
}