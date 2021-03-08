using System;
using System.Collections.Generic;
using System.Linq;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public interface IPodcastRepository
    {
        // podcast methods
        IQueryable<Podcast> Podcasts { get; }
        void AddPod(Podcast podcast);
        Podcast GetPodByID(int id);
        Podcast GetPodByName(string name);
        //List<Podcast> GetSavedPods(AppUser user);
        //void DeletePod(int id);

        // genre methods
        IQueryable<Genre> Genres { get; }
        void AddGenre(Genre genre);
        List<Podcast> GetPodsByGenre(int id);

        // review methods
        IQueryable<Review> Reviews { get; }
        //void AddReview(Review review);
        //List<Review> GetReviewsByPodID(int id);
        //void EditReview(Review review);
        //void DeleteReview(int id);
    }
}
