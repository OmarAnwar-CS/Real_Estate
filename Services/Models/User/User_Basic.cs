using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _Services.Models.User
{
    public class User_Basic
    {

        public int Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string ProfilePicture { get; set; }

        public IEnumerable<int> PropertiesId { get; set; } 
        public IEnumerable<int> InquiriesId { get; set; } 
        public IEnumerable<int> FavoriteId { get; set; } 
    }
}
