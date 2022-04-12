using Joinler.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joinler.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment
                .HasOne(c=>c.Post)
                .WithMany(c=>c.Comments)
                .HasForeignKey(c=>c.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            
            comment
                .HasOne(c=>c.UserProfile)
                .WithMany(u=>u.Comments)
                .HasForeignKey(c=>c.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
