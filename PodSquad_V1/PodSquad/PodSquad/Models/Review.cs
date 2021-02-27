using System;
using System.Collections.Generic;

namespace PodSquad.Models
{
    public class Review
    {
        private List<Comment> comments = new List<Comment>();

        public int ReviewID { get; set; }
        public Podcast Podcast { get; set; }
        public AppUser Reviewer { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public List<Comment> Comments { get { return comments; } }
    }
}
