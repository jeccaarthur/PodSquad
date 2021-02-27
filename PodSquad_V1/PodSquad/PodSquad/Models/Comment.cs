using System;
namespace PodSquad.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public AppUser Commenter { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
    }
}
