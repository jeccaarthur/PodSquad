using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PodSquad.Models;

namespace PodSquad.Controllers
{
    public class PodcastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
