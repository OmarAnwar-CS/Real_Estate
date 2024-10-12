using _Services.Contracts;
using _Services.Models.Property;
using application.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // Get all properties
        [HttpGet]
        public IActionResult GetAllProperties()
        {
            var properties = _propertyService.GetAllProperties();
            return Ok(properties);
        }

        [HttpGet("GetPropertyList")]
        public IActionResult GetPropertyList()
        {
            var properties = _propertyService.GetPropertyList();
            return Ok(properties);
        }

        // Get property by ID
        [HttpGet("{id}")]
        public IActionResult GetPropertyById(int id)
        {
            var property = _propertyService.GetPropertyById(id);
            if (property == null)
                return NotFound("Property not found.");

            return Ok(property);
        }

        // Create a new property
        [HttpPost]
        public IActionResult CreateProperty([FromBody] Property_Create _property)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _propertyService.CreateProperty(_property);
            return Ok("Property created successfully.");
        }

        // Update an existing property
        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, [FromBody] Property_Update _property)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _propertyService.UpdateProperty(id, _property);
            return Ok("Property updated successfully.");
        }

        // Delete a property by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteProperty(int id)
        {
            _propertyService.DeleteProperty(id);
            return Ok("Property deleted successfully.");
        }

        // Get properties by Filtering with optional parameters
        [HttpGet("OrderedByPrice")]
        public IActionResult GetPropertiesWithFilterOrderedByPrice(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true)
        {

            var properties = _propertyService.GetPropertiesWithFilterOrderedByPrice(keyWord, city, status, minPrice, maxPrice, minArea, maxArea, minBaths, maxBaths, minBed, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished, ascending);
            return Ok(properties);
        }

        [HttpGet("OrderedByDateAdded")]
        public IActionResult GetPropertiesWithFilterOrderedByDateAdded(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? minPrice = null, decimal? maxPrice = null,
                                               double? minArea = null, double? maxArea = null,
                                               int? minBaths = null, int? maxBaths = null, int? minBed = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false,

                                               bool ascending = true)
        {

            var properties = _propertyService.GetPropertiesWithFilterOrderedByDateAdded(keyWord, city, status, minPrice, maxPrice, minArea, maxArea, minBaths, maxBaths, minBed, maxBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished, ascending);
            return Ok(properties);
        }

        [HttpGet("GetPropertiesWithFilter")]
        public IActionResult GetPropertiesWithFilter(string? keyWord = null, string? city = null, Status? status = null,

                                               decimal? maxPrice = null, double? maxArea = null,
                                               int? minBaths = null, int? minBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false)
        {

            var properties = _propertyService.GetPropertiesWithFilter(keyWord, city, status, maxPrice, maxArea, minBaths, minBed, HasGarage, Two_Stories, Laundry_Room, HasPool, HasGarden, HasElevator, HasBalcony, HasParking, HasCentralHeating, IsFurnished);
            return Ok(properties);
        }



        //// Search for properties by term
        //[HttpGet("search")]
        //public IActionResult SearchProperties([FromQuery] string searchTerm)
        //{
        //    var properties = _propertyService.SearchProperties(searchTerm);
        //    return Ok(properties);
        //}

        //// Filter properties
        //[HttpGet("filter")]
        //public IActionResult FilterProperties([FromQuery] string location, [FromQuery] string propertyType,
        //                                      [FromQuery] int? minBedrooms, [FromQuery] int? maxBedrooms,
        //                                      [FromQuery] int? minBathrooms, [FromQuery] int? maxBathrooms)
        //{
        //    var properties = _propertyService.FilterProperties(location, propertyType, minBedrooms, maxBedrooms, minBathrooms, maxBathrooms);
        //    return Ok(properties);
        //}

        //// Get properties ordered by price
        //[HttpGet("order/price")]
        //public IActionResult GetPropertiesOrderedByPrice([FromQuery] bool ascending = true)
        //{
        //    var properties = _propertyService.GetPropertiesOrderedByPrice(ascending);
        //    return Ok(properties);
        //}

        //// Get properties ordered by date added
        //[HttpGet("order/date")]
        //public IActionResult GetPropertiesOrderedByDateAdded([FromQuery] bool ascending = true)
        //{
        //    var properties = _propertyService.GetPropertiesOrderedByDateAdded(ascending);
        //    return Ok(properties);
        //}

        //// Get properties by user ID
        //[HttpGet("user/{userId}")]
        //public IActionResult GetPropertiesByUserId(int userId)
        //{
        //    var properties = _propertyService.GetPropertiesByUserId(userId);
        //    return Ok(properties);
        //}
    }
}
