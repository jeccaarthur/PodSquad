using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodSquad.Models;
using PodSquad.Repositories;

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
        public IActionResult About(Podcast podcast)
        {
            // TODO: get reviews for podcast id
            //podcast.Reviews = repo.GetReviews(podcast.PodcastID);

            return View(podcast);
        }


        [HttpGet]
        public IActionResult AddPod()
        {
            return View();
        }

        // TODO: make this async?
        [HttpPost]
        public IActionResult AddPod(Podcast podcast)
        {
            // add pod's genre to db and assign it to podcast
            repo.AddGenre(podcast.Genre);

            // add pod to db
            repo.AddPod(podcast);

            return View(podcast);
        }

        [HttpGet]
        public IActionResult Review(int podcastID)
        {
            // create an instance of reviewVM and assign the current podcast ID to it
            ReviewVM reviewVM = new ReviewVM();
            reviewVM.PodcastID = podcastID;

            return View(reviewVM);
        }

        public RedirectToActionResult Review(ReviewVM reviewVM)
        {
            // create Review object and assign values out of reviewVM
            Review review = new Review();
            // TODO: uncomment reviewer
            //review.Reviewer = userManager.GetUserAsync(User).Result;
            review.Date = DateTime.Now;
            review.Rating = reviewVM.Rating;
            review.ReviewText = reviewVM.ReviewText;

            // get the podcast this review is for
            Podcast podcast = repo.GetPodByID(reviewVM.PodcastID);

            // add review to db
            podcast.Reviews.Add(review);
            repo.AddReview(review);

            return RedirectToAction("About", reviewVM.PodcastID);
        }
    }
}
