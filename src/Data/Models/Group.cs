namespace Joinler.Data.Models
{
    public class Group
    {
        public int Id { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; } = new HashSet<UserProfile>();

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        
    }
}
