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
    // TODO: restrict to authorized users
    public class ForumController : Controller
    {
        IForumRepository repo;
        UserManager<AppUser> userManager;

        public ForumController(IForumRepository r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Post> posts = repo.GetAllPosts();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Post(Post post)
        {
            // assign user to post
            post.Poster = userManager.GetUserAsync(User).Result;

            // set date
            post.Date = DateTime.Now;

            // save post to db
            repo.AddPost(post);

            // store current postID for easy access to send back to About page
            int id = post.PostID;

            // redirect to thread view
            return RedirectToAction("Thread", new { postID = id });
        }

        [HttpGet]
        public IActionResult Reply(int postID)
        {
            // retrieve post with id
            Post post = repo.GetPostByID(postID);

            // create an instance of replyVM and assign the current post to it
            ReplyVM replyVM = new ReplyVM();
            replyVM.Post = post;

            return View(replyVM);
        }


        [HttpPost]
        public RedirectToActionResult Reply(ReplyVM replyVM)
        {
            if (ModelState.IsValid)
            {
                // create reply object and assign values out of replyVM
                Reply reply = new Reply();

                // TODO: uncomment responder
                //reply.Responder = userManager.GetUserAsync(User).Result;
                reply.Date = DateTime.Now;
                reply.ReplyText = replyVM.ReplyText;

                // get the post this reply is on
                Post post = repo.GetPostByID(replyVM.Post.PostID);

                // add reply to db
                post.Replies.Add(reply);
                repo.AddReply(reply);
            }

            // store current postID for easy access to send back to Thread page
            int id = replyVM.Post.PostID;

            return RedirectToAction("Thread", new { postID = id });
        }


        [HttpGet]
        public IActionResult Thread(int postID)
        {
            // get post with id and send to view
            Post post = repo.GetPostByID(postID);

            return View(post);
        }
    }
}
