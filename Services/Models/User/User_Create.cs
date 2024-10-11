using System.ComponentModel.DataAnnotations;

namespace _Services.Models.User
{
    public class User_Create
    {
        [Required]
        [StringLength(20)]
        public string F_Name { get; set; }
        [Required]
        [StringLength(20)]
        public string L_Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
