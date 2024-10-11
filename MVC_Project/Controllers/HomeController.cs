using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC_Project.API_Services;
using MVC_Project.Models;
using MVC_Project.ViewModel;
using System.Diagnostics;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBase_API_Call _base_API_Call;

        public HomeController(ILogger<HomeController> logger,IBase_API_Call base_API_Call)
        {
            _logger = logger;
            _base_API_Call = base_API_Call;
        }

        public async Task<IActionResult> Index()
        {
            var cityList = await _base_API_Call.GetAllCity();
            return View(cityList);
        }

        public async Task<IActionResult> Properties()
        {
            var cityList = await _base_API_Call.GetAllCity();

            var properyList = await _base_API_Call.GetPropertyList();
            var viewModel = new PropertyViewModel
            {
                Properties = properyList,
                Cites = cityList
            };

            int max = (int)properyList.Max(x => x.Price);
            ViewData["MaxPrice"] = max;
            int min = (int)properyList.Min(x => x.Price);
            ViewData["MinPrice"] = min;
            max = (int)properyList.Max(x => x.Area);
            ViewData["MaxArea"] = max;
            min = (int)properyList.Min(x => x.Area);
            ViewData["MinArea"] = min;

            
            return View(viewModel);

        }
        //public async Task<IActionResult> PropertiesFiltered(string? keyWord = null, string? city = null, int? status = null,

        //                                       decimal? maxPrice = null, double? maxArea = null,
        //                                       int? maxBaths = null, int? maxBed = null,

        //                                       bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
        //                                       bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
        //                                       bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false)
        //{
        //    Filter filter = new Filter
        //    {

        //        Status = status == 0 ? null : (status == 1 ? Status.rent : (status == 2 ? Status.buy : null)),
        //        City = city != "All" ? city : null,
        //        Keyword = keyWord.IsNullOrEmpty() ? null : keyWord,
        //        PriceRange = maxPrice != 0 ? maxPrice : null,
        //        AreaSize = maxArea != 0 ? maxArea : null,
        //        Beds = maxBed != 0 ? maxBed : null,
        //        Baths = maxBaths != 0 ? maxBaths : null,
        //        HasGarage = HasGarage,
        //        Two_Stories = Two_Stories,
        //        Laundry_Room = Laundry_Room,
        //        HasPool = HasPool,
        //        HasGarden = HasGarden,
        //        HasElevator = HasElevator,
        //        HasBalcony = HasBalcony,
        //        HasParking = HasParking,
        //        HasCentralHeating = HasCentralHeating,
        //        IsFurnished = IsFurnished

        //    };


        //    var properties = await _base_API_Call.GetFilteredProperties(filter);
        //    var cityList = await _base_API_Call.GetAllCity();

        //    var viewModel = new PropertyViewModel
        //    {
        //        Properties = properties,
        //        Cites = cityList
        //    };

        //    return View(viewModel);
        //}

        public async Task<IActionResult> PropertyiesPartial(string? keyWord = null, string? city = null, int? status = null,

                                               decimal? maxPrice = null, double? maxArea = null,
                                               int? maxBaths = null, int? maxBed = null,

                                               bool HasGarage = false, bool Two_Stories = false, bool Laundry_Room = false,
                                               bool HasPool = false, bool HasGarden = false, bool HasElevator = false,
                                               bool HasBalcony = false, bool HasParking = false, bool HasCentralHeating = false, bool IsFurnished = false)
        {
            Filter filter = new Filter
            {

                Status = status == 0 ? null : (status == 1 ? Status.rent : (status == 2 ? Status.buy : null)),
                City = city != "All" ? city : null,
                Keyword = keyWord.IsNullOrEmpty() ? null : keyWord,
                PriceRange = maxPrice != 0 ? maxPrice : null,
                AreaSize = maxArea != 0 ? maxArea : null,
                Beds = maxBed != 0 ? maxBed : null,
                Baths = maxBaths != 0 ? maxBaths : null,
                HasGarage = HasGarage,
                Two_Stories = Two_Stories,
                Laundry_Room = Laundry_Room,
                HasPool = HasPool,
                HasGarden = HasGarden,
                HasElevator = HasElevator,
                HasBalcony = HasBalcony,
                HasParking = HasParking,
                HasCentralHeating = HasCentralHeating,
                IsFurnished = IsFurnished

            };


            var properties = await _base_API_Call.GetFilteredProperties(filter);

            return PartialView("/Views/Partial_Views/_propertyListPartial.cshtml", properties);
        }

        public async Task<IActionResult> SortedProperties()
        {
            var properyList = await _base_API_Call.GetSortedPropertyPriceList();
            return PartialView("/Views/Partial_Views/_propertyListPartial.cshtml",properyList);
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _base_API_Call.GetUserInfo(1);
            return View(user);
        }
        public IActionResult ProfilePartial()
        {
            //var user = await _base_API_Call.GetUserInfo(1);
            return PartialView("/Views/Partial_Views/_myProfilePartial.cshtml");
        }
        public IActionResult MyPropertiesPartial()
        {
            //var user = await _base_API_Call.GetUserInfo(1);
            return PartialView("/Views/Partial_Views/_myPropertiesPartial.cshtml");
        }

        public IActionResult FavoritedPropertiesPartial()
        {
            //var user = await _base_API_Call.GetUserInfo(1);
            return PartialView("/Views/Partial_Views/_favoritedPropertiesPartial.cshtml");
        }

        public IActionResult MassagesPartial()
        {
            //var user = await _base_API_Call.GetUserInfo(1);
            return PartialView("/Views/Partial_Views/_massagesPartial.cshtml");
        }
        public IActionResult SubmitPropertyPartial()
        {
            //var user = await _base_API_Call.GetUserInfo(1);
            return PartialView("/Views/Partial_Views/_submitPropertyPartial.cshtml");
        }
        public IActionResult ChangePasswordPartial()
        {
            //var user = await _base_API_Call.GetUserInfo(1);
            return PartialView("/Views/Partial_Views/_changePasswordPartial.cshtml");
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
