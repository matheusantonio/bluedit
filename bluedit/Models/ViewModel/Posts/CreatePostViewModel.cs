using System.Collections.Generic;

namespace bluedit.Models.ViewModel.Posts
{
    public class CreatePostViewModel
    {
        public string Title {get; set;}

        public string Content {get; set;}

        public string SubForum {get; set;}

        public List<string> Tags {get; set;}
    }
}