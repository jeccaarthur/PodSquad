using System;
using System.Collections.Generic;

namespace PodSquad.Models
{
    public class PodcastVM
    {
        private List<Podcast> results = new List<Podcast>();

        public bool Success { get; set; }
        public string SearchQuery { get; set; }
        public Podcast Podcast { get; set; }
        //public List<Podcast> Results { get; set; }
    }
}
