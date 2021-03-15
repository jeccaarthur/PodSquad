using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PodSquad.Models;

using FluentSpotifyApi;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;
using FluentSpotifyApi.Model.Shows;
using System.Text.Json;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;

namespace PodSquad.Repositories
{
    public class PodcastRepository : IPodcastRepository
    {
        private PodContext context;
        private readonly IFluentSpotifyClient fluentSpotifyClient;

        public PodcastRepository(PodContext c, IFluentSpotifyClient f)
        {
            context = c;
            fluentSpotifyClient = f;
        }


        #region SPOTIFY PODCAST METHODS

        // spotify authorization
        public async Task<string> GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();
            string postString = string.Format("grant_type=client_credentials");

            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            string url = "https://accounts.spotify.com/api/token";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic ZGJhMGQ3OGJmNzQxNDQwY2FiN2NmNDAzOTAxODViYWI6YWZkOTllOWY5YjE3NGNiZDgzYzgxYjI0MTIzNmU0ZDk="); request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                        }
                    }
                }
            }

            return token.access_token;
        }

        // retrieve spotify data
        public async Task<Podcast> GetSpotifyPodcast(string token, string name)
        {
            // example call: "https://api.spotify.com/v1/search?q=My%20Favorite%20Murder%20with%20Karen%20Kilgariff%20and%20Georgia%20Hardstark&type=show&limit=10&offset=0"

            // construct url
            string query = System.Web.HttpUtility.UrlPathEncode(name);
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type=show&market=US&limit=10&offset=0", query);

            Podcast podcast = new Podcast();

            try
            {
                // call spotify
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json; charset=utf-8";

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseFromServer = reader.ReadToEnd();

                            dynamic data = JObject.Parse(responseFromServer);

                            if (data != null)
                            {
                                // TODO: get podcast spotify url too - external_urls:spotify:{url}
                                podcast.SpotifyID = data.shows.items[0].id;
                                podcast.Name = data.shows.items[0].name;
                                podcast.Network = data.shows.items[0].publisher;
                                podcast.Description = data.shows.items[0].description;
                                podcast.ImageURL = data.shows.items[0].images[0].url;
                                podcast.SpotifyURL = data.shows.items[0].external_urls.spotify;
                            }
                        }
                    }
                }
            }
            catch
            {
                podcast = null;
            }

            return podcast;
        }


        #endregion SPOTIFY PODCAST METHODS



        #region LOCAL PODCAST METHODS

        public IQueryable<Podcast> Podcasts
        {
            get
            {
                return context.Podcasts
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
                .Include(pod => pod.Reviews)
                .ThenInclude(review => review.Reviewer)
                .ToList();

            return podcasts;
        }

        // retrieve podcast with matching id
        public Podcast GetPodByID(int podcastID)
        {
            Podcast podcast = context.Podcasts
                .Include(pod => pod.Reviews)
                .ThenInclude(review => review.Reviewer)
                .Where(pod => pod.PodcastID == podcastID).FirstOrDefault();

            return podcast;
        }

        // see if podcast with matching spotify id exists
        public Podcast GetPodBySpotifyID(string spotifyID)
        {
            Podcast podcast = context.Podcasts.Where(pod => pod.SpotifyID == spotifyID).FirstOrDefault();

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


        /*
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
        */





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
