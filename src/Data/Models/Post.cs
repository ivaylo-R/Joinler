namespace Joinler.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public string MediaLocation { get; set; }   

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    }
}
