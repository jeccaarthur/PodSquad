using System;
namespace PodSquad.Models
{
    public class Post
    {
        // TODO: uncomment properties when other models have been built

        //private List<Reply> replies = new List<Reply>();

        public int PostID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        //public AppUser Poster { get; set; }
        //public List<Reply> Replies { get { return replies; } }
    }
}
