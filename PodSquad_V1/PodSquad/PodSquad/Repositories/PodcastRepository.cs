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

        // retrieve podcast with matching id
        public Podcast GetPodByID(int id)
        {
            Podcast podcast = context.Podcasts.First(pod => pod.PodcastID == id);
            return podcast;
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
        }

        // retrieve all podcasts with specified genre id
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
                    .Include(review => review.Podcast)
                    .Include(review => review.Reviewer);
            }
        }


        #endregion
    }
}
