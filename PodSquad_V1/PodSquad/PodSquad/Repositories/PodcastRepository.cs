using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public class PodcastRepository : IPodcastRepository
    {
        private PodContext context;

        public PodcastRepository(PodContext c)
        {
            context = c;
        }



        #region PODCAST METHODS

        public IQueryable<Podcast> Podcasts
        {
            get
            {
                return context.Podcasts
                    .Include(pod => pod.Genre)
                    .Include(pod => pod.Reviews)
                    .ThenInclude(review => review.Reviewer);
            }
        }

        // add a new podcast
        public void AddPod(Podcast podcast)
        {
            context.Podcasts.Add(podcast);
            context.SaveChanges();
        }

        // retrieve all podcasts
        public List<Podcast> GetAllPods()
        {
            List<Podcast> podcasts = context.Podcasts
                .Include(pod => pod.Genre)
                .Include(pod => pod.Reviews)
                .ThenInclude(review => review.Reviewer)
                .ToList();

            return podcasts;
        }

        // retrieve podcast with matching id
        public Podcast GetPodByID(int podcastID)
        {
            Podcast podcast = context.Podcasts
                .Include(pod => pod.Genre)
                .Include(pod => pod.Reviews)
                .ThenInclude(review => review.Reviewer)
                .Where(pod => pod.PodcastID == podcastID).FirstOrDefault();

            return podcast;
        }

        // retrieve podcast with matching name
        public Podcast GetPodByName(string name)
        {
            Podcast podcast = Podcasts.First(pod => pod.Name == name);
            return podcast;
        }

        // update an existing podcast - used to add reviews
        public void UpdatePod(Podcast podcast)
        {
            context.Podcasts.Update(podcast);
            context.SaveChanges();
        }

        #endregion



        #region GENRE METHODS

        public IQueryable<Genre> Genres
        {
            get
            {
                return context.Genres;
            }
        }

        public void AddGenre(Genre genre)
        {
            Genre existingGenre = context.Genres.First(g => g.Name == genre.Name);

            if (existingGenre == null)
            {
                context.Genres.Add(genre);
                context.SaveChanges();
            }
            // TODO: confirm the correct genreID is being assigned to the podcast if genre already exists
            // (working in tests, confirm on live)
            else
            {
                genre.GenreID = existingGenre.GenreID;
            }
        }

        // retrieve all podcasts with specified genre name
        public List<Podcast> GetPodsByGenre(int id)
        {
            List<Podcast> pods = context.Podcasts.Where(p => p.Genre.GenreID == id).ToList();
            return pods;
        }


        #endregion





        #region REVIEW METHODS

        public IQueryable<Review> Reviews
        {
            get
            {
                return context.Reviews
                    .Include(review => review.Reviewer);
            }
        }

        // adds a new review
        public void AddReview(Review review)
        {
            // save review to db
            context.Reviews.Add(review);
            context.SaveChanges();
        }

        //public List<Review> GetReviews(int podcastID)
        //{
        //    List<Review> reviewList = context.Reviews.Where(r => r.Podcast.PodcastID == podcastID)
        //        .OrderByDescending(d => d.Date).ToList();

        //    return reviewList;
        //}


        #endregion
    }
}
