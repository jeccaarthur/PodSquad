using System;
using System.Collections.Generic;

namespace PodSquad.Models
{
    public class Post
    {
        // TODO: add validation to fields

        private List<Reply> replies = new List<Reply>();

        public int PostID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public AppUser Poster { get; set; }
        public List<Reply> Replies { get { return replies; } }
    }
}
