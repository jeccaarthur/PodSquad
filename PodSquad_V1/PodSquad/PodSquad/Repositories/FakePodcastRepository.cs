using System;
using System.Collections.Generic;
using System.Linq;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public class FakePodcastRepository : IPodcastRepository
    {
        private List<Podcast> podcasts = new List<Podcast>();
        private List<Review> reviews = new List<Review>();






        #region PODCAST METHODS

        public IQueryable<Podcast> Podcasts
        {
            get
            {
                return podcasts.AsQueryable<Podcast>();
            }
        }

        // add a new podcast
        public void AddPod(Podcast podcast)
        {
            // simulate auto-incremented primary key and add podcast to list
            podcast.PodcastID = podcasts.Count;
            podcasts.Add(podcast);
        }

        // retrieve podcast with matching id
        public Podcast GetPodByID(int id)
        {
            Podcast podcast = podcasts.Find(pod => pod.PodcastID == id);
            return podcast;
        }

        #endregion







        #region REVIEW METHODS

        public IQueryable<Review> Reviews
        {
            get
            {
                return reviews.AsQueryable<Review>();
            }
        }


        #endregion

    }
}
