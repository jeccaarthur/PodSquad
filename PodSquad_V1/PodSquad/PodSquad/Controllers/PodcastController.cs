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

        public IActionResult About(int podcastID)
        {
            var podcast = repo.GetPodByID(podcastID);

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
            // retrieve podcast with id
            Podcast podcast = repo.GetPodByID(podcastID);

            // create an instance of reviewVM and assign the current podcast ID to it
            var reviewVM = new ReviewVM
            {
                PodcastID = podcastID,
                PodcastName = podcast.Name
            };
            //reviewVM.PodcastID = id;
            //reviewVM.PodcastName = podcast.Name;

            return View(reviewVM);
        }

        [HttpPost]
        public RedirectToActionResult Review(ReviewVM reviewVM)
        {
            if (ModelState.IsValid)
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
            }

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
    }
}
