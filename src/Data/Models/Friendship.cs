using System.ComponentModel.DataAnnotations;

namespace Joinler.Data.Models
{
    public class Friendship
    {
        public int Id { get; set; } 

        [Required]
        public string RequesterId { get; set; }

        public virtual UserProfile Requester { get; set; }

        [Required]
        public string ResponderId { get; set; }

        public virtual UserProfile Responder { get; set; }

    }
}
