using System;
using System.Collections.Generic;
using System.Linq;
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

        #endregion






        #region GENRE METHODS

        public IQueryable<Genre> Genres
        {
            get
            {
                return genres.AsQueryable<Genre>();
            }
        }

        public void AddGenre(Genre genre)
        {
            Genre existingGenre = genres.Find(g => g.Name == genre.Name);

            if (existingGenre == null)
            {
                // simulate auto-incremented primary key and add genre to list
                genre.GenreID = genres.Count;
                genres.Add(genre);
            }
            else
            {
                Console.WriteLine("Genre already exists");
                genre.GenreID = existingGenre.GenreID;
            }
        }

        // retrieve all podcasts with specified genre name
        public List<Podcast> GetPodsByGenre(int id)
        {
            List<Podcast> pods = podcasts.Where(p => p.Genre.GenreID == id).ToList();
            return pods;
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
