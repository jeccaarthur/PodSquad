using System;
namespace PodSquad.Models
{
    public class Reply
    {
        public int ReplyID { get; set; }
        public AppUser Responder { get; set; }
        public DateTime Date { get; set; }
        public string ReplyText { get; set; }
    }
}
