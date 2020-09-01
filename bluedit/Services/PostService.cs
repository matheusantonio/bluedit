using bluedit.Models.Entities;
using bluedit.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System;

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
            _posts.Find(post => true).SortByDescending(post => post.Time) .ToList();

        public Post Get(string id) =>
            _posts.Find<Post>(post => post.Id == id).FirstOrDefault();

        public List<Post> GetBySubForum(string subForumId) =>
            _posts.Find<Post>(post => post.SubForumId == subForumId).SortByDescending(post => post.Time).ToList();

        public IEnumerable<string> GetTopSubForums() =>
             _posts.Aggregate()
                .Group(post => new { subForumGroup = post.SubForumId },
                       g => new {id = g.Key, count = g.Count()})
                .SortByDescending(r => r.count)
                .Project(r => new {value = r.id.subForumGroup })
                .ToEnumerable()
                .Select(x => x.value.ToString());
            

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public void Update(string id, Post postIn) =>
            _posts.ReplaceOne(post => post.Id == id, postIn);
        
        public void Remove(Post postIn) =>
            _posts.DeleteOne(post => post.Id == postIn.Id);
        
        public void Remove(string id) =>
            _posts.DeleteOne(post => post.Id == id);
    }
}