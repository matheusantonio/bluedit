using bluedit.Models.ViewModel.Posts;
using System.Collections.Generic;

namespace bluedit.Models.ViewModel.SubForum
{
    public class SubForumHomeViewModel
    {
        public string Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}

        public List<PostPreviewViewModel> posts {get; set;}
    }
}