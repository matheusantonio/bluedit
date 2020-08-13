using System.Collections.Generic;
using System;

namespace bluedit.Models.ViewModel.Reply{
    public class ReplyViewModel {
        public string Id {get; set;}

        public string Author {get; set;}

        public string Content {get; set;}

        public List<ReplyViewModel> Replies {get; set;}

        public DateTime Time {get; set;}

        public int Upvotes {get; set;}
    }
}