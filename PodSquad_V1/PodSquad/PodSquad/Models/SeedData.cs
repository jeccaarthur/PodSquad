using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PodSquad.Repositories;

namespace PodSquad.Models
{
    public class SeedData
    {
        public static void Seed(PodContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IPodcastRepository repo)
        {
            // if podcast table is empty, seed data
            if (!context.Podcasts.Any())
            {
                // create member and admin roles
                var result = roleManager.CreateAsync(new IdentityRole("Member")).Result;
                result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;

                // seed a default administrator
                AppUser admin = new AppUser
                {
                    UserName = "admin",
                    FirstName = "Site",
                    LastName = "Admin"
                };

                userManager.CreateAsync(admin, "Secret-123").Wait();    // creates user in db
                IdentityRole adminRole = roleManager.FindByNameAsync("Admin").Result;   
                userManager.AddToRoleAsync(admin, adminRole.Name);      // adds user to admin role

                context.SaveChanges();


                // seed first forum post
                var firstPost = new Post
                {
                    Title = "Welcome to the PodSquad",
                    Body = "Hello there! I hope you enjoy your time here finding new podcasts to enjoy and discussing them with fellow squad members. Drop me a line if you run into issues!",
                    Date = DateTime.Now,
                    Poster = new AppUser { UserName = "siteadmin" }
                };

                context.Posts.Add(firstPost);
                context.SaveChanges();


                // seed spotify podcasts
                string token = repo.GetAccessToken().Result;

                // my favorite murder 
                Podcast myFavMurder = repo.GetSpotifyPodcast(token, "my favorite murder").Result;
                context.Podcasts.Add(myFavMurder);
                context.SaveChanges();

                // higher learning
                Podcast higherLearning = repo.GetSpotifyPodcast(token, "higher learning").Result;
                context.Podcasts.Add(higherLearning);
                context.SaveChanges();

                // your own backyard
                Podcast yourOwnBackyard = repo.GetSpotifyPodcast(token, "your own backyard").Result;
                context.Podcasts.Add(yourOwnBackyard);
                context.SaveChanges();

                // hidden brain
                Podcast hiddenBrain = repo.GetSpotifyPodcast(token, "hidden brain").Result;
                context.Podcasts.Add(hiddenBrain);
                context.SaveChanges();
            }
        }
    }
}
