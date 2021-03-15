﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PodSquad.Models
{
    public class SeedData
    {
        public static void Seed(PodContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            // TODO: update seeded data to reflect updated podcast properties
            // (removed genre and hostname)
            
            // if podcast table is empty, seed data
            if (!context.Podcasts.Any())
            {
                // create member and admin roles
                var result = roleManager.CreateAsync(new IdentityRole("Member")).Result;
                result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;

                // seed a default administrator
                AppUser siteadmin = new AppUser
                {
                    UserName = "SiteAdmin",
                    FirstName = "Site",
                    LastName = "Admin"
                };

                userManager.CreateAsync(siteadmin, "Secret-123").Wait();
                IdentityRole adminRole = roleManager.FindByNameAsync("Admin").Result;
                userManager.AddToRoleAsync(siteadmin, adminRole.Name);

                // save changes to db
                context.SaveChanges();

                // save changes to db
                context.SaveChanges();


                // seed fake podcasts to test formatting
                var pod1 = new Podcast
                {
                    Name = "Podcast 1",
                    Network = "Network",
                    Description = "This is a podcast description. I hope you enjoy.",
                };
                context.Podcasts.Add(pod1);

                var pod2 = new Podcast
                {
                    Name = "Podcast 2",
                    Network = "Network",
                    Description = "This is a podcast description. I hope you enjoy.",
                };
                context.Podcasts.Add(pod2);

                var pod3 = new Podcast
                {
                    Name = "Podcast 3",
                    Network = "Network",
                    Description = "This is a podcast description. I hope you enjoy.",
                };
                context.Podcasts.Add(pod3);

                var pod4 = new Podcast
                {
                    Name = "Podcast 4",
                    Network = "Network",
                    Description = "This is a podcast description. I hope you enjoy.",
                };
                context.Podcasts.Add(pod4);

                var pod5 = new Podcast
                {
                    Name = "Podcast 5",
                    Network = "Network",
                    Description = "This is a podcast description. I hope you enjoy.",
                };
                context.Podcasts.Add(pod5);

                // save changes to db
                context.SaveChanges();


                // seed fake posts to test formatting
                var post1 = new Post
                {
                    Title = "First post",
                    Body = "This is the first post in the forum. Enjoy it.",
                    Date = DateTime.Now,
                    Poster = new AppUser { FirstName = "Jecca", LastName = "Arthur" }
                };
                context.Posts.Add(post1);

                var post2 = new Post
                {
                    Title = "Second post",
                    Body = "This is the second post in the forum. Enjoy it.",
                    Date = DateTime.Now,
                    Poster = new AppUser { FirstName = "Misha", LastName = "Danger" }
                };
                context.Posts.Add(post2);

                // save changes to db
                context.SaveChanges();
            }
        }
    }
}
