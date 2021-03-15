using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodSquad.Models;
using PodSquad.Repositories;


// TODO: check if model states are valid

namespace PodSquad.Controllers
{
    public class PodcastController : Controller
    {
        IPodcastRepository repo;
        UserManager<AppUser> userManager;

        public PodcastController(IPodcastRepository r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }

        [HttpGet]
        public IActionResult About(int podcastID)
        {
            var podcast = repo.GetPodByID(podcastID);

            return View(podcast);
        }

        [Authorize(Roles = "Member, Admin")]
        [HttpGet]
        public IActionResult Review(int podcastID)
        {
            // retrieve podcast with id
            Podcast podcast = repo.GetPodByID(podcastID);

            // create an instance of reviewVM and assign the current podcast ID to it
            var reviewVM = new ReviewVM
            {
                PodcastID = podcastID,
                PodcastName = podcast.Name
            };

            return View(reviewVM);
        }

        [Authorize(Roles = "Member, Admin")]
        [HttpPost]
        public RedirectToActionResult Review(ReviewVM reviewVM)
        {
            if (ModelState.IsValid)
            {
                // create Review object and assign values out of reviewVM
                Review review = new Review();

                review.Reviewer = userManager.GetUserAsync(User).Result;
                review.Date = DateTime.Now;

                // TODO: store rating properly
                review.Rating = reviewVM.Rating;
                review.ReviewText = reviewVM.ReviewText;

                // get the podcast this review is for
                Podcast podcast = repo.GetPodByID(reviewVM.PodcastID);

                // add review to db
                podcast.Reviews.Add(review);
                repo.AddReview(review);

                // update average rating
                podcast.AvgRating = repo.CalculateAvgRating(podcast);
                repo.UpdatePod(podcast);
            }

            // store current podcastID for easy access to send back to About page
            int id = reviewVM.PodcastID;

            return RedirectToAction("About", new { podcastID = id });
        }

        [HttpGet]
        public IActionResult Browse()
        {
            List<Podcast> podcasts = repo.GetAllPods();

            return View(podcasts);
        }

        [HttpPost]
        public RedirectToActionResult Browse(int podcastID)
        {
            return RedirectToAction("About");
        }

        // takes podcast name input from user
        [Authorize(Roles = "Member, Admin")]
        [HttpGet]
        public IActionResult Search()
        {
            // create shell podcastVM and send to view
            PodcastVM podcastVM = new PodcastVM();

            // set success property to true
            podcastVM.Success = true;

            return View(podcastVM);
        }


        [HttpPost]
        public IActionResult Search(PodcastVM podcastVM)
        {
            string name = podcastVM.SearchQuery;

            // get spotify token
            string token = repo.GetAccessToken().Result;

            // send query to spotify and redirect results to confirm
            //Task<Podcast> podcast = repo.GetSpotifyPodcast(token, name);
            Podcast podcast = repo.GetSpotifyPodcast(token, name).Result;

            if (podcast != null)
            {
                // assign podcast result to podcastVM
                podcastVM.Podcast = new Podcast
                {
                    SpotifyID = podcast.SpotifyID,
                    Name = podcast.Name,
                    Network = podcast.Network,
                    Description = podcast.Description,
                    ImageURL = podcast.ImageURL,
                    SpotifyURL = podcast.SpotifyURL
                };

                // set success to true
                podcastVM.Success = true;
            }
            else
            {
                // if search didn't return any results, set success to false
                podcastVM.Success = false;
            }

            return View(podcastVM);
        }


        [Authorize(Roles = "Member, Admin")]
        [HttpPost]
        public IActionResult AddPod(Podcast podcast)
        {
            // see if podcast is already in db
            Podcast p = repo.GetPodBySpotifyID(podcast.SpotifyID);
            int id;

            // if podcast already exists return about view with its id
            if (p != null)
            {
                id = p.PodcastID;
            }
            // otherwise save to db and return about view with new id
            else
            {
                repo.AddPod(podcast);
                id = podcast.PodcastID;
            }

            // redirect to about page
            return RedirectToAction("About", new { podcastID = id });
        }
    }
}
