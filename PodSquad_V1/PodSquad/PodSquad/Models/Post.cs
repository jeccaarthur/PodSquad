using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PodSquad.Models
{
    public class Post
    {
        // TODO: add validation to fields

        private List<Reply> replies = new List<Reply>();

        public int PostID { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter text.")]
        public string Body { get; set; }

        public DateTime Date { get; set; }
        public AppUser Poster { get; set; }
        public List<Reply> Replies { get { return replies; } }
    }
}
