using System;
using System.ComponentModel.DataAnnotations;

namespace PodSquad.Models
{
    public class ReviewVM
    {
        // TODO: add validation to fields

        public int PodcastID { get; set; }
        public string PodcastName { get; set; }

        [Required(ErrorMessage = "Please rate this podcast.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Please enter your review.")]
        [StringLength(350)]
        public string ReviewText { get; set; }
    }
}
