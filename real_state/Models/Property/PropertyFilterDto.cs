namespace MVC_Project.Models
{
    public class PropertyFilterDto
    {
        
        public string OrderBy { get; set; }
        public string Order { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        string? keyWord { get; set; }
        string? city { get; set; }
        int status { get; set; }
        decimal? minPrice { get; set; }
        decimal? maxPrice { get; set; }
        double? minArea { get; set; }
        double? maxArea { get; set; }
        int? minBaths { get; set; }
        int? maxBaths { get; set; }
        int? minBed { get; set; }
        int? maxBed { get; set; }
        bool HasGarage { get; set; }
        bool Two_Stories { get; set; }
        bool Laundry_Room { get; set; }
        bool HasPool { get; set; }
        bool HasGarden { get; set; }
        bool HasElevator { get; set; }
        bool HasBalcony { get; set; }

        bool HasParking { get; set; }
        bool HasCentralHeating { get; set; }
        bool IsFurnished { get; set; }
        bool ascending { get; set; }
    }

}
