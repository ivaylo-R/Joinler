using Joinler.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joinler.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> post)
        {
            post
                .HasOne(l=>l.UserProfile)
                .WithMany(p=>p.Posts)
                .HasForeignKey(r => r.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
