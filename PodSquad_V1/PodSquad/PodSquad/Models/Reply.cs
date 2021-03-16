using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodSquad.Models
{
    public class Reply
    {
        public int ReplyID { get; set; }
        public AppUser Responder { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string ReplyText { get; set; }
        
    }
}
