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
        //List<Podcast> GetPodsByGenre(string genre);
        //List<Podcast> GetSavedPods(AppUser user);
        //void DeletePod(int id);

        // review methods
        IQueryable<Review> Reviews { get; }
        //void AddReview(Review review);
        //List<Review> GetReviewsByPodID(int id);
        //void EditReview(Review review);
        //void DeleteReview(int id);
    }
}
