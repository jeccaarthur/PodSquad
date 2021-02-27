using System;
namespace PodSquad.Models
{
    public class Reply
    {
        // TODO: add validation to fields

        public int ReplyID { get; set; }
        public AppUser Responder { get; set; }
        public DateTime Date { get; set; }
        public string ReplyText { get; set; }
    }
}
