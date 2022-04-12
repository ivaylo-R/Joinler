using Joinler.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joinler.Data.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> like)
        {
            like
                .HasOne(l => l.Post)
                .WithMany(l=>l.Likes)
                .HasForeignKey(l=>l.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            like
                .HasOne(u=>u.UserProfile)
                .WithMany(l=>l.Likes)
                .HasForeignKey(l => l.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            like
                .HasOne(p => p.Comment)
                .WithMany(l=>l.Likes)
                .HasForeignKey(l => l.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
