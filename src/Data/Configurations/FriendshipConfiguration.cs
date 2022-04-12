using Joinler.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joinler.Data.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> friendship)
        {
            friendship
                .HasOne(r=>r.Responder)
                .WithMany(r=>r.Responders)
                .HasForeignKey(r=>r.ResponderId)
                .OnDelete(DeleteBehavior.Restrict);

            friendship
                .HasOne(r => r.Requester)
                .WithMany(r => r.Requesters)
                .HasForeignKey(r => r.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
