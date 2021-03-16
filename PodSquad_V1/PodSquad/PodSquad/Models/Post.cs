using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodSquad.Models
{
    public class Post
    {
        private List<Reply> replies = new List<Reply>();

        public int PostID { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter text.")]
        public string Body { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("PosterId")]
        public AppUser Poster { get; set; }
        public List<Reply> Replies { get { return replies; } }
    }
}
