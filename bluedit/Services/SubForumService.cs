using bluedit.Models.Entities;
using bluedit.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace bluedit.Services
{
    public class SubForumService
    {
        private readonly IMongoCollection<SubForum> _subForums;

        public SubForumService(IBlueditDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _subForums = database.GetCollection<SubForum>(settings.SubForumCollectionName);
        }

        public List<SubForum> Get() =>
            _subForums.Find(subForum => true).ToList();

        public SubForum Get(string id) =>
            _subForums.Find<SubForum>(subForum => subForum.Id == id).FirstOrDefault();

        public SubForum GetByName(string name) =>
            _subForums.Find<SubForum>(SubForum => SubForum.Name == name).FirstOrDefault();
        
        public SubForum Create(SubForum subForum)
        {
            _subForums.InsertOne(subForum);
            return subForum;
        }

        public void Update(string id, SubForum subForumIn) =>
            _subForums.ReplaceOne(subForum => subForum.Id == id, subForumIn);
        
        public void Remove(SubForum subForumIn) =>
            _subForums.DeleteOne(subForum => subForum.Id == subForumIn.Id);
        
        public void Remove(string id) =>
            _subForums.DeleteOne(subForum => subForum.Id == id);
    }
}