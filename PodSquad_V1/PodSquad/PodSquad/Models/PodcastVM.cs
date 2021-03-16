using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PodSquad.Models
{
    public class PodcastVM
    {
        private List<Podcast> results = new List<Podcast>();

        public bool Success { get; set; }

        [Required(ErrorMessage = "Please enter the name of the podcast you're looking for.")]
        [StringLength(150)]
        public string SearchQuery { get; set; }

        public Podcast Podcast { get; set; }
    }
}
