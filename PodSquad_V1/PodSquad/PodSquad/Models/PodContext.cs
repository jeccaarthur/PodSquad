using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PodSquad.Models
{
    public class PodContext : IdentityDbContext
    {
        public PodContext(DbContextOptions<PodContext> options) : base(options) { }

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // get around primary key error on IdentityUser class

            builder.Entity<Podcast>()
                .HasAlternateKey(p => p.SpotifyID);
        }
    }
}
