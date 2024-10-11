namespace MVC_Project.Models
{
    public enum Status : byte
    {
        rent = 1,
        buy = 2,
        used = 4
    }
    public class Properties_List
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; } 
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public string Image { get; set; }
    }


    public class AmenitiesFilter
    {
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
    }
    public class Filter : AmenitiesFilter
    {
        
        public string? Keyword { get; set; }
        public string? City { get; set; }
        public Status? Status { get; set; }
        public decimal? PriceRange { get; set; }
        public double? AreaSize { get; set; }
        public int? Beds { get; set; }
        public int? Baths { get; set; }
        public int? NumberOfProperties { get; set; }
    }
}
