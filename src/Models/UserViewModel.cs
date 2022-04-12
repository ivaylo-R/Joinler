using Joinler.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Joinler.Models
{
    public class UserViewModel
    {
        public string Id { get; init; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; init; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; init; }

        [Required]
        public string Email { get; init; }

        public string CurrentCity { get; init; }

        public string Country { get; init; }

        public string Birthday { get; init; }

        public Gender Gender { get; init; }

        public string? PhoneNumber { get; init; }

        public string Occupation { get; init; }
    }
}
