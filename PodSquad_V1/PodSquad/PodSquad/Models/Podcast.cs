﻿using System;
using System.Collections.Generic;
using PodSquad.Repositories;

namespace PodSquad.Models
{
    public class Podcast
    {
        private List<Review> reviews = new List<Review>();

        public int PodcastID { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public string Network { get; set; }
        public string HostName { get; set; }
        public string Description { get; set; }
        //public string ImageURL { get; set; }
        public List<Review> Reviews { get { return reviews; } }

        // TODO: add average rating property with logic to calculate
        // public double AvgRating { get; set; }
    }
}
