using bluedit.Models.Entities;
using bluedit.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace bluedit.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService(IBlueditDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _posts = database.GetCollection<Post>(settings.PostsCollectionName);
        }

        public List<Post> Get() =>
            _posts.Find(post => true).ToList();

        public Post Get(ObjectId id) =>
            _posts.Find<Post>(post => post.Id == id).FirstOrDefault();
        
        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public void Update(ObjectId id, Post postIn) =>
            _posts.ReplaceOne(post => post.Id == id, postIn);
        
        public void Remove(Post postIn) =>
            _posts.DeleteOne(post => post.Id == postIn.Id);
        
        public void Remove(ObjectId id) =>
            _posts.DeleteOne(post => post.Id == id);
    }
}