using Joinler.Data.Configurations;
using Joinler.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Joinler.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
         {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FriendshipConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}