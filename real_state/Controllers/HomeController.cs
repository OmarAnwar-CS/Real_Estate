using Microsoft.AspNetCore.Mvc;
using MVC_Project.API_Services;
using MVC_Project.Models;
using real_state.Models;
using System.Diagnostics;

namespace real_state.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBase_API_Call _base_API_Call;

        public HomeController(IBase_API_Call base_API_Call)
        {
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
            var properyList = await _base_API_Call.GetAllProperties();
            int max = (int) properyList.Max(x => x.Price);
            ViewData["MaxPrice"] = max;
            int min = (int)properyList.Min(x => x.Price);
            ViewData["MinPrice"] = min;
            max = (int)properyList.Max(x => x.Area);
            ViewData["MaxArea"] = max;
            min = (int)properyList.Min(x => x.Area);
            ViewData["MinArea"] = min;
            return View(cityList);
        }
        //public async Task<IActionResult> Properties(string city = null, string status = null, int? minPrice = null, int? maxPrice = null, int? minArea = null, int? maxArea = null, int beds = 0, int baths = 0, string orderBy = "property_date", string order = "ASC", int page = 1, int pageSize = 12)
        //{
        //    var cityList = await _base_API_Call.GetAllCity();
        //    ViewData["Cities"] = cityList;

        //    var filter = new PropertyFilterDto
        //    {
        //        City = city,
        //        Status = status,
        //        MinPrice = minPrice,
        //        MaxPrice = maxPrice,
        //        MinArea = minArea,
        //        MaxArea = maxArea,
        //        Beds = beds,
        //        Baths = baths,
        //        OrderBy = orderBy,
        //        Order = order,
        //        Page = page,
        //        PageSize = pageSize
        //    };

        //    var properties = await _base_API_Call.GetFilteredProperties(filter);
        //    return View(properties);
        //}


        public async Task<IActionResult> Profile()
        {
            var user = await _base_API_Call.GetUserInfo(1);
            return View(user);
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
