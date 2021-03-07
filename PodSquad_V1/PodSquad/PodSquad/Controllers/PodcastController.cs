using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PodSquad.Models;
using PodSquad.Repositories;

namespace PodSquad.Controllers
{
    public class PodcastController : Controller
    {
        IPodcastRepository repo;

        public PodcastController(IPodcastRepository r)
        {
            repo = r;
        }

        [HttpGet]
        public IActionResult About(Podcast podcast)
        {
            return View(podcast);
        }

        [HttpGet]
        public IActionResult AddPod()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPod(Podcast podcast)
        {
            // add pod's genre to db and assign it to podcast
            repo.AddGenre(podcast.Genre);

            // add pod to db
            repo.AddPod(podcast);

            return View(podcast);
        }
    }
}
