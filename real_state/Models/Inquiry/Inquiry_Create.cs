namespace MVC_Project.Models
{
    public class Inquiry_Create
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public string? PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
