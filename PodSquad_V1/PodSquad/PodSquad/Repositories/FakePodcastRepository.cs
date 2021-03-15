using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public class FakePodcastRepository : IPodcastRepository
    {
        private List<Podcast> podcasts = new List<Podcast>();
        private List<Genre> genres = new List<Genre>();
        private List<Review> reviews = new List<Review>();


        #region PODCAST METHODS

        public IQueryable<Podcast> Podcasts
        {
            get
            {
                return podcasts.AsQueryable<Podcast>();
            }
        }

        // add a new podcast if it doesn't already exist
        public void AddPod(Podcast podcast)
        {
            Podcast existingPod = podcasts.Find(p => p.Name == podcast.Name);

            if (existingPod == null)
            {
                // simulate auto-incremented primary key and add podcast to list
                podcast.PodcastID = podcasts.Count;
                podcasts.Add(podcast);
            }
            else
            {
                throw new Exception("Podcast already exists");
            }
        }

        public List<Podcast> GetAllPods()
        {
            podcasts = Podcasts.ToList();

            return podcasts;
        }

        // retrieve podcast with matching id
        public Podcast GetPodByID(int id)
        {
            Podcast podcast = podcasts.Find(pod => pod.PodcastID == id);
            return podcast;
        }

        // retrieve podcast with matching name
        public Podcast GetPodByName(string name)
        {
            Podcast podcast = podcasts.Find(pod => pod.Name == name);
            return podcast;
        }

        //// update an existing podcast - used to add reviews
        //public void UpdatePod(Podcast podcast)
        //{
        //    // retrieve podcast from list
        //    Podcast pod = podcasts.Find(p => p.PodcastID == podcast.PodcastID);

        //    // update its properties
        //    //pod.Name = podcast.Name;
        //    //pod.Network = podcast.Network;
        //    //pod.HostName = podcast.HostName;
        //    //pod.Description = podcast.Description;

        //    // pull most recent review out of podcast and add it to pod reviews
        //    // TODO: might need to sort reviews by date to identify most recent
        //    Review review = podcast.Reviews.Last();
        //    pod.Reviews.Add(review);
        //}

        #endregion



        #region REVIEW METHODS

        public IQueryable<Review> Reviews
        {
            get
            {
                return reviews.AsQueryable<Review>();
            }
        }

        // adds a review to reviews
        public void AddReview(Review review)
        {
            reviews.Add(review);
        }

        public Task<List<Podcast>> SearchSpotify(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAccessToken()
        {
            throw new NotImplementedException();
        }

        public Task<Podcast> GetSpotifyPodcast(string token, string name)
        {
            throw new NotImplementedException();
        }

        public Podcast GetPodBySpotifyID(string spotifyID)
        {
            throw new NotImplementedException();
        }

        public int CalculateAvgRating(Podcast podcast)
        {
            throw new NotImplementedException();
        }

        public void UpdatePod(Podcast podcast)
        {
            throw new NotImplementedException();
        }

        //public List<Review> GetReviews(int podcastID)
        //{
        //    List<Review> reviewList = Reviews.Where(r => r. == podcastID)
        //        //.OrderByDescending(d => d.Date).ToList();

        //    return reviewList;
        //}

        #endregion

    }
}
