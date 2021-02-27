using System;
namespace PodSquad.Models
{
    public class Podcast
    {
        // TODO: uncomment properties when other models have been built

        // private List<Review> reviews = new List<Review>();

        public int PodcastID { get; set; }
        public string Name { get; set; }
        // public Genre Genre { get; set; }
        public string Network { get; set; }
        public string HostName { get; set; }

        // public List<Review> Reviews { get { return reviews; } }
    }
}
