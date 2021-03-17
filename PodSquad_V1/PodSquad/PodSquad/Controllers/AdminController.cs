using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using PodSquad.Models;
using PodSquad.Repositories;

namespace PodSquad.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IForumRepository forumRepo;
        private IPodcastRepository podRepo;

        public AdminController(UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr,
            IForumRepository forum, IPodcastRepository pod)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            forumRepo = forum;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }

            AdminVM model = new AdminVM
            {
                Users = users,
                Roles = roleManager.Roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result = null;
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                // check to see if the user has sent or received a post or reply
                if (0 == (from m in forumRepo.Posts
                          where m.Poster.UserName == user.UserName
                          select m).Count<Post>()
                          ||
                          (0 == (from m in forumRepo.Replies
                                 where m.Responder.UserName == user.UserName
                                 select m).Count<Reply>())
                                 ||
                                 (0 == (from m in podRepo.Reviews
                                        where m.Reviewer.UserName == user.UserName
                                        select m).Count<Review>()))
                {

                    result = await userManager.DeleteAsync(user);
                }
                else
                {
                    result = IdentityResult.Failed(new IdentityError()
                    { Description = "User's posts, replies, and/or reviews must be deleted first" });
                }

                if (!result.Succeeded)
                {
                    // if failed 
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += errorMessage != "" ? " | " : "";   // put a separator between messages
                        errorMessage += error.Description;
                    }
                    TempData["message"] = errorMessage;
                }
                else
                {
                    TempData["message"] = "";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");

            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. "
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded) { }

            return RedirectToAction("Index");
        }

        // role management methods

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateAccountVM model)
        {

            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
