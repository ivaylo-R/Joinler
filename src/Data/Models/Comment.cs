namespace Joinler.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }  

        public string? Content { get; set; }

        public DateTime Created { get; set; }   

        public bool IsDeleted { get; set; }

        public bool IsEdited { get; set; }

        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    }
}
