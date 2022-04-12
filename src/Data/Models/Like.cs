using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joinler.Data.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public string UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }    

    }
}
