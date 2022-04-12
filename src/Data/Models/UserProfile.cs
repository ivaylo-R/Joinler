using Joinler.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Joinler.Data.Models
{
    public class UserProfile : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public ICollection<Friendship> Requesters { get; set; } = new HashSet<Friendship>();

        public ICollection<Friendship> Responders { get; set; } = new HashSet<Friendship>();

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    }
}
