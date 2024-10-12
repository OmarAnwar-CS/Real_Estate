using _Services.Models.Property;
using application.DataAccess.Models;

namespace _Services.Contracts
{
    public interface IPropertyService
    {
        IEnumerable<Property_GetAll_Func> GetAllProperties();
        IEnumerable<Properties_List> GetPropertyList();
        Property_AllInfo GetPropertyById(int Id);
        void CreateProperty(Property_Create _property);
        void UpdateProperty(int id, Property_Update property);
        void DeleteProperty(int id);

        IEnumerable<Property_GetAll_Func> GetPropertiesWithFilterOrderedByPrice(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true);

        IEnumerable<Property_GetAll_Func> GetPropertiesWithFilterOrderedByDateAdded(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true);
        IEnumerable<Properties_List> GetPropertiesWithFilter(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? maxPrice = null, double? maxArea = null,
                                               int? minBaths = null, int? minBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false);
    }
}
