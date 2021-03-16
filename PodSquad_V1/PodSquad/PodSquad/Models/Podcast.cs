using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PodSquad.Repositories;

namespace PodSquad.Models
{
    public class Podcast
    {
        private List<Review> reviews = new List<Review>();

        [Key]
        public int PodcastID { get; set; }

        [Required(ErrorMessage = "Spotify ID is required")]
        public string SpotifyID { get; set; }

        [Required(ErrorMessage = "Podcast name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Network name is required")]
        public string Network { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Image URL is required")]
        public string ImageURL { get; set; }

        public string SpotifyURL { get; set; }
        public List<Review> Reviews { get { return reviews; } }
        public int AvgRating { get; set; }
    }
}
