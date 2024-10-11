namespace MVC_Project.Models
{
    public class User_GetAll
    {
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string ProfilePicture { get; set; }

        public int? Num_of_Properties { get; set; }
        public int? Num_of_Inquiries { get; set; }
    }
}
