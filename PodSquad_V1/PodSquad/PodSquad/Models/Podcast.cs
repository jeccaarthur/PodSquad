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
        public string SpotifyID { get; set; }
        public string Name { get; set; }
        public string Network { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string SpotifyURL { get; set; }
        public List<Review> Reviews { get { return reviews; } }
        public int AvgRating { get; set; }
    }
}
