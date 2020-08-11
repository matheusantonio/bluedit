namespace bluedit.Models
{
    public class BlueditDatabaseSettings : IBlueditDatabaseSettings
    {
        public string PostsCollectionName { get; set; }
        public string ReplyCollectionName {get; set;}
        public string SubForumCollectionName {get; set;}
        public string UpvoteCollectionName {get; set;}
        public string UserCollectionName {get; set;}
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBlueditDatabaseSettings
    {
        string PostsCollectionName { get; set; }
        string ReplyCollectionName {get; set;}
        string SubForumCollectionName {get; set;}
        string UpvoteCollectionName {get; set;}
        string UserCollectionName {get; set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}