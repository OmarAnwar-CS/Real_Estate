namespace MVC_Project.Models
{
    public enum PropType : byte
    {
        apartment = 1,
        villa = 2,
        office = 3,
        land = 4,
        shop = 5,
        warehouse = 6,
        building = 7,
        other = 8
    }
    public class Property_AllInfo
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public double Area { get; set; }
        public PropType PropertyType { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int? YearBuilt { get; set; }
        public DateTime DateAdded { get; set; }
        public Status Status { get; set; }
        public bool HasGarage { get; set; }
        public bool Two_Stories { get; set; }
        public bool Laundry_Room { get; set; }
        public bool HasPool { get; set; }
        public bool HasGarden { get; set; }
        public bool HasElevator { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParking { get; set; }
        public bool HasCentralHeating { get; set; }
        public bool IsFurnished { get; set; }
        public IEnumerable<String> Image { get; set; }

    }
}
