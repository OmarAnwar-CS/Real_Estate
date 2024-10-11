using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MVC_Project.Models;
using System.Text;

namespace MVC_Project.Controllers
{
    public class PropertyController : Controller
    {
        private readonly HttpClient _httpClient;
        private int propertyId;
        public PropertyController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7197/api/");
        }

        // GET: PropertyController
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Property");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Property_GetAll_Func>>();
                return View(result);
            }
            else
            {
                // عرض صفحة خطأ أو رسالة في حال فشل الطلب
                ViewBag.ErrorMessage = "Failed to retrieve data from the API.";
                return View("Error");
            }
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property_Create propertyCreateModel)
        {
            if (ModelState.IsValid)
            {
                // تحويل الكائن المدخل إلى JSON
                var jsonContent = JsonConvert.SerializeObject(propertyCreateModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // إرسال الطلب إلى الـ API
                var response = await _httpClient.PostAsync("Property", content);

                if (response.IsSuccessStatusCode)
                {
                    // إعادة التوجيه إلى الصفحة الرئيسية بعد الإنشاء الناجح
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // عرض رسالة خطأ في حالة حدوث مشكلة
                    ModelState.AddModelError(string.Empty, "Error creating property");
                }
            }

            // إعادة عرض النموذج في حالة وجود خطأ في التحقق من صحة البيانات
            return View(propertyCreateModel);
        }


        // GET: Property/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            propertyId = id;
            ViewData["P_ID"]= id;
            
            var response = await _httpClient.GetAsync($"Property/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var property = JsonConvert.DeserializeObject<Property_AllInfo>(content);
                return View(property);
            }
            return NotFound();
        }

        // POST: Property/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Property_Update property)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"Property/{propertyId}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(property);
        }

        // GET: PropertyController/Delete/5
        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Property/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error deleting property");
                    return View("Error"); // or return some error view
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View("Error"); // or return some error view
            }
        }


        //// POST: PropertyController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"Property/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Error deleting property");
        //        return View();
        //    }
        //}
    }
}
