using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public interface IPodcastRepository
    {
        // podcast methods
        IQueryable<Podcast> Podcasts { get; }
        void AddPod(Podcast podcast);
        List<Podcast> GetAllPods();
        Podcast GetPodByID(int id);
        Podcast GetPodByName(string name);
        public Podcast GetPodBySpotifyID(string spotifyID);
        //List<Podcast> GetSavedPods(AppUser user);
        //void UpdatePod(Podcast podcast);
        //void DeletePod(int id);
        //int GetAverageRating();
        //Task<List<Podcast>> SearchSpotify(string name);

        // spotify api methods
        Task<string> GetAccessToken();
        Task<Podcast> GetSpotifyPodcast(string token, string name);





        // genre methods
        //IQueryable<Genre> Genres { get; }
        //void AddGenre(Genre genre);
        //List<Podcast> GetPodsByGenre(int id);

        // review methods
        IQueryable<Review> Reviews { get; }
        void AddReview(Review review);
        //List<Review> GetReviews(int podcastID);
        //void EditReview(Review review);
        //void DeleteReview(int id);
    }
}
