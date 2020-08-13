using System.Collections.Generic;
using System;

namespace bluedit.Models.Entities
{
    public class PostPreviewViewModel
    {
        public string Id {get; set;}

        public string Title {get; set;}

        public List<string> Tags {get; set;}

        public string Author {get; set;}

        public DateTime Time {get; set;}

        public int Upvotes {get; set;}
    }
    
}