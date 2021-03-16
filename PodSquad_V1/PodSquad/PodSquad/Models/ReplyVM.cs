using System;
using System.ComponentModel.DataAnnotations;

namespace PodSquad.Models
{
    public class ReplyVM
    {
        // TODO: add validation to fields

        public Post Post { get; set; }

        [Required(ErrorMessage = "Please enter a reply.")]
        public string ReplyText { get; set; }
    }
}
