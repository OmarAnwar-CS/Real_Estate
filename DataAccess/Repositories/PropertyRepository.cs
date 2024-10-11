using application.DataAccess;
using application.DataAccess.Models;
using Application.DataAccessContracts;
using Microsoft.EntityFrameworkCore;

namespace API_Project.DataAccess.Repositories
{
    internal class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {

        public PropertyRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Property> GetAll()
        {
            return _dbSet.Include(p => p.Amenities)
                         .Include(p => p.Images)
                         .Include(p => p.City)
                         .AsEnumerable();
        }
        override public Property? GetById(int id)
        {
            return _dbSet.Include(p => p.Owner)
                         .Include(p => p.Amenities)
                         .Include(p => p.Images)
                         .Include(p => p.Inquiries)
                         .Include(p => p.City)

                         .FirstOrDefault(p => p.Id == id);
        }

        //public IEnumerable<Property> GetPropertiesByPrice(decimal minPrice, decimal maxPrice)
        //{
        //    return _dbSet.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        //}

        public IEnumerable<Property> GetPropertiesWithFilter(string? keyWord = null, string? city = null, Status? status = null,

                                                       decimal? minPrice = null, decimal? maxPrice = null,
                                                       double? minArea = null, double? maxArea = null,
                                                       int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                                       bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                                       bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                                       bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false)
        {


            var properties = _dbSet.Include(p => p.Amenities).Include(p => p.Images).Include(p => p.City).AsEnumerable();

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToLower();
                properties = properties.Where(p => p.Title.ToLower().Contains(keyWord) || (p.Description != null ? p.Description.ToLower().Contains(keyWord):false) || p.Location.ToLower().Contains(keyWord));
            }

            if (!string.IsNullOrEmpty(city))
            {
                city = city.ToLower();
                properties = properties.Where(p => p.City.Name.ToLower().Contains(city));
            }

            if (status != Status.used && status != null)
                properties = properties.Where(p => p.Status == status);

            if (minPrice.HasValue)
                properties = properties.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                properties = properties.Where(p => p.Price <= maxPrice.Value);

            if (minArea.HasValue)
                properties = properties.Where(p => p.Area >= minArea.Value);
            if (maxArea.HasValue)
                properties = properties.Where(p => p.Area <= maxArea.Value);

            if (minBaths.HasValue)
                properties = properties.Where(p => p.Bathrooms >= minBaths.Value);
            if (maxBaths.HasValue)
                properties = properties.Where(p => p.Bathrooms <= maxBaths.Value);

            if (minBed.HasValue)
                properties = properties.Where(p => p.Bedrooms >= minBed.Value);
            if (maxBed.HasValue)
                properties = properties.Where(p => p.Bedrooms <= maxBed.Value);

            if (HasGarage)
                properties = properties.Where(p => p.Amenities.HasGarage == HasGarage);
            if (Two_Stories)
                properties = properties.Where(p => p.Amenities.Two_Stories == Two_Stories);
            if (Laundry_Room)
                properties = properties.Where(p => p.Amenities.Laundry_Room == Laundry_Room);
            if (HasPool)
                properties = properties.Where(p => p.Amenities.HasPool == HasPool);
            if (HasGarden)
                properties = properties.Where(p => p.Amenities.HasGarden == HasGarden);
            if (HasElevator)
                properties = properties.Where(p => p.Amenities.HasElevator == HasElevator);
            if (HasBalcony)
                properties = properties.Where(p => p.Amenities.HasBalcony == HasBalcony);
            if (HasParking)
                properties = properties.Where(p => p.Amenities.HasParking == HasParking);
            if (HasCentralHeating)
                properties = properties.Where(p => p.Amenities.HasCentralHeating == HasCentralHeating);
            if (IsFurnished)
                properties = properties.Where(p => p.Amenities.IsFurnished == IsFurnished);


            return properties;
        }



        public IEnumerable<Property> GetPropertiesWithFilterOrderedByPrice(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true)
        {


            var properties = GetPropertiesWithFilter(keyWord, city, status, minPrice, maxPrice, minArea, maxArea, minBaths, maxBaths, minBed, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished);

            properties = ascending ? properties.OrderBy(p => p.Price) : properties.OrderByDescending(p => p.Price);

            return properties;
        }



        public IEnumerable<Property> GetPropertiesWithFilterOrderedByDateAdded(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true)
        {


            var properties = GetPropertiesWithFilter(keyWord, city, status, minPrice, maxPrice, minArea, maxArea, minBaths, maxBaths, minBed, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished);

            properties = ascending ? properties.OrderBy(p => p.DateAdded) : properties.OrderByDescending(p => p.DateAdded);

            return properties;
        }

        public IEnumerable<Property> GetPropertiesByUserId(int userId)
        {
            return _dbSet.Include(p => p.Amenities).Where(p => p.OwnerId == userId).ToList();
        }

    }
}
