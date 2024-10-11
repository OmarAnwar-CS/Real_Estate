using _Services.Contracts;
using _Services.EntityMapping;
using _Services.Models.Property;
using _Services.Models.City;
using application.DataAccess.Models;
using Application.DataAccessContracts;

namespace Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAmenitiesService _amenitiesService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly ICityService _cityService;

        public PropertyService(IUnitOfWork unitOfWork, IAmenitiesService amenitiesService, IPropertyImageService propertyImageService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _amenitiesService = amenitiesService;
            _propertyImageService = propertyImageService;
            _cityService = cityService;
        }


        public IEnumerable<Property_GetAll_Func> GetAllProperties()
        {
            try
            {
                var properties = _unitOfWork.Property.GetAll();

                return PropertyMapping.MapToPropertyGetAll(properties);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving properties.", ex);
            }
        }

        public IEnumerable<Properties_List> GetPropertyList()
        {
            try
            {
                var properties = _unitOfWork.Property.GetAll();

                return PropertyMapping.MapToPropertyList(properties);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving properties.", ex);
            }
        }

        public Property_AllInfo GetPropertyById(int Id)
        {
            try
            {
                var property = _unitOfWork.Property.GetById(Id);
                if (property == null)
                {
                    throw new KeyNotFoundException("Property not found.");
                }
                return PropertyMapping.MapToPropertyAllInfo(property);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving the property.", ex);
            }
        }

        public void CreateProperty(Property_Create _property)
        {
            try
            {
                ValidateProperty(_property);

                // تحقق من وجود المدينة
                var existingCity = _cityService.GetCityByName(_property.City);
                if (existingCity == null)
                {
                    _cityService.CreateCity(new City_Add { Name = _property.City });
                    existingCity = _cityService.GetCityByName(_property.City);
                }

                int cityId = existingCity.Id;

                // التعامل مع Amenities
                _amenitiesService.CreateAmenities(_property.Amenities);

                var allAmenities = _unitOfWork.Amenities.GetAll().ToList();
                int amenitiesId = 0;
                foreach (var amenity in allAmenities)
                {
                    if (
                        (_property.Amenities.HasPool == amenity.HasPool) && (_property.Amenities.HasBalcony == amenity.HasBalcony) &&
                        (_property.Amenities.HasParking == amenity.HasParking) && (_property.Amenities.Two_Stories == amenity.Two_Stories) &&
                        (_property.Amenities.HasGarage == amenity.HasGarage) && (_property.Amenities.HasBalcony == amenity.HasBalcony) &&
                        (_property.Amenities.HasCentralHeating == amenity.HasCentralHeating) && (_property.Amenities.HasElevator == amenity.HasElevator) &&
                        (_property.Amenities.IsFurnished == amenity.IsFurnished)
                        )
                    {
                        amenitiesId = amenity.Id;
                        break;
                    }
                }

                var property = PropertyMapping.MapToProperty(_property, amenitiesId, cityId);

                _unitOfWork.Property.Insert(property);
                _unitOfWork.Save();

                int propertyId = GetPropertyIdByTitle(_property.Title, _property.OwnerId);

                // إضافة الصور
                foreach (var image in _property.Images)
                {
                    _propertyImageService.CreatePropertyImage(propertyId, image);
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while creating the property.", ex);
            }
        }


        public void UpdateProperty(int id, Property_Update _property)
        {
            try
            {
                var property = _unitOfWork.Property.GetById(id);
                if (property == null)
                    throw new KeyNotFoundException("Property not found.");

                if (!string.IsNullOrEmpty(_property.Title))
                    property.Title = _property.Title;
                if (!string.IsNullOrEmpty(_property.Description))
                    property.Description = _property.Description;
                if (_property.Price > 0)
                    property.Price = (int)_property.Price;
                if (!string.IsNullOrEmpty(_property.Location))
                    property.Location = _property.Location;

                if (!string.IsNullOrEmpty(_property.City))
                {
                    _cityService.CreateCity(new City_Add { Name = _property.City });

                    int cityId = _cityService.GetCityByName(_property.City).Id;
                    property.CityId = cityId;
                }

                if (_property.Area > 0)
                    property.Area = (int)_property.Area;
                if (_property.Bedrooms > 0)
                    property.Bedrooms = (int)_property.Bedrooms;
                if (_property.Bathrooms > 0)
                    property.Bathrooms = (int)_property.Bathrooms;
                if (_property.YearBuilt > 0)
                    property.YearBuilt = (int)_property.YearBuilt;
                if (_property.Status != 0)
                    property.Status = _property.Status;
                if (_property.Amenities != null)
                {
                    _amenitiesService.CreateAmenities(_property.Amenities);
                    var AllAmenities = _unitOfWork.Amenities.GetAll().ToList();

                    int amenitiesId = 0;
                    foreach (var amenity in AllAmenities)
                    {
                        if (
                            (_property.Amenities.HasPool == amenity.HasPool) && (_property.Amenities.HasBalcony == amenity.HasBalcony) &&
                            (_property.Amenities.HasParking == amenity.HasParking) && (_property.Amenities.Two_Stories == amenity.Two_Stories) &&
                            (_property.Amenities.HasGarage == amenity.HasGarage) && (_property.Amenities.HasBalcony == amenity.HasBalcony) &&
                            (_property.Amenities.HasCentralHeating == amenity.HasCentralHeating) && (_property.Amenities.HasElevator == amenity.HasElevator) &&
                            (_property.Amenities.IsFurnished == amenity.IsFurnished)
                            )
                        {
                            amenitiesId = amenity.Id;
                            break;
                        }
                    }
                    property.AmenitiesId = amenitiesId;
                }

                if (_property.Images != null)
                {
                    _propertyImageService.UpdateAllPropertyImages(id, _property.Images);
                }

                _unitOfWork.Property.Update(property);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while updating the property.", ex);
            }
        }

        public void DeleteProperty(int id)
        {
            try
            {
                var property = _unitOfWork.Property.GetById(id);
                if (property == null)
                {
                    throw new KeyNotFoundException("Property not found.");
                }

                _unitOfWork.Property.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while deleting the property.", ex);
            }
        }

        public IEnumerable<Property_GetAll_Func> GetPropertiesWithFilterOrderedByPrice(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true)
        {

            try
            {
                var properties = _unitOfWork.Property.GetPropertiesWithFilterOrderedByPrice(keyWord, city, status, minPrice, maxPrice, minArea, maxArea, minBaths, maxBaths, minBed, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished, ascending);
                return PropertyMapping.MapToPropertyGetAll(properties);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving properties.", ex);
            }
        }

        public IEnumerable<Property_GetAll_Func> GetPropertiesWithFilterOrderedByDateAdded(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true)
        {

            try
            {
                var properties = _unitOfWork.Property.GetPropertiesWithFilterOrderedByDateAdded(keyWord, city, status, minPrice, maxPrice, minArea, maxArea, minBaths, maxBaths, minBed, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished, ascending);
                return PropertyMapping.MapToPropertyGetAll(properties);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving properties.", ex);
            }
        }

        public IEnumerable<Properties_List> GetPropertiesWithFilter(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? maxPrice = null, double? maxArea = null,
                                               int? maxBaths = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false)
        {

            try
            {
                var properties = _unitOfWork.Property.GetPropertiesWithFilter(keyWord, city, status, null , maxPrice, null, maxArea, null, maxBaths, null, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished);
                return PropertyMapping.MapToPropertyList(properties);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving properties.", ex);
            }
        }
        

        public int GetPropertyIdByTitle(string title, int OwnerId)
        {
            var property = _unitOfWork.Property.GetAll().FirstOrDefault(p => p.Title == title && p.OwnerId == OwnerId);
            return property.Id;
        }

        private void ValidateProperty(Property_Create property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            if (property.OwnerId <= 0)
                throw new ArgumentException("Owner ID is required.", nameof(property.OwnerId));

            if (_unitOfWork.User.GetById(property.OwnerId) == null)
                throw new KeyNotFoundException("User not found.");

            if (string.IsNullOrEmpty(property.Title))
                throw new ArgumentException("Property title is required.", nameof(property.Title));

            if (property.Price <= 0)
                throw new ArgumentException("Property price must be greater than zero.", nameof(property.Price));

            if (string.IsNullOrEmpty(property.Location))
                throw new ArgumentException("Property location is required.", nameof(property.Location));

            if (string.IsNullOrEmpty(property.City))
                throw new ArgumentException("Property city is required.", nameof(property.City));

            if (property.Area <= 0)
                throw new ArgumentException("Property area must be greater than zero.", nameof(property.Area));

            if (property.Bedrooms < 0)
                throw new ArgumentException("Number of bedrooms cannot be negative.", nameof(property.Bedrooms));

            if (property.Bathrooms < 0)
                throw new ArgumentException("Number of bathrooms cannot be negative.", nameof(property.Bathrooms));

            if (property.Images == null || !property.Images.Any())
                throw new ArgumentException("At least one image is required.", nameof(property.Images));



            // Add any additional validations as needed
        }
    }
}
