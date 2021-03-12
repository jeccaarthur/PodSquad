using System;
namespace PodSquad.Models
{
    public class ReviewVM
    {
        // TODO: add validation to fields

        public int PodcastID { get; set; }
        public string PodcastName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
