using MVC_Project.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Text;

namespace MVC_Project.API_Services
{
    public interface IBase_API_Call
    {
        // Any common functionality for API calls can be placed here
        Task<IEnumerable<City_Get>> GetAllCity();
        Task<Property_AllInfo> GetPropertyById(int Id);
        Task<IEnumerable<Properties_List>> GetPropertyList();
        Task<IEnumerable<Properties_List>> GetPropertyList(IEnumerable<int> Id);
        Task<IEnumerable<Properties_List>> GetFilteredProperties(Filter filter);
        Task<User_Info> GetUserInfo(int id);



    }



    internal class Base_API_Call : IBase_API_Call
    {
        protected readonly HttpClient _httpClient;  // Use `protected` to allow derived classes to access it

        public Base_API_Call(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7197/api/"); // Set the base address
        }

        public async Task<IEnumerable<City_Get>> GetAllCity()
        {
            try
            {
                var response = await _httpClient.GetAsync("City");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<City_Get>>();
                return result ?? Enumerable.Empty<City_Get>();  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }

        public async Task<Property_AllInfo> GetPropertyById(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Property/{Id}");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<Property_AllInfo>();
                return result ?? new Property_AllInfo();  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }
        public async Task<IEnumerable<Properties_List>> GetPropertyList()
        {
            try
            {
                var response = await _httpClient.GetAsync("Property/GetPropertyList");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Properties_List>>();
                result = result ?? Enumerable.Empty<Properties_List>();
                result = result.OrderByDescending(U => U.DateAdded)
                                       .ThenBy(U => U.Price);
                return result;  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }



        public async Task<IEnumerable<Properties_List>> GetFilteredProperties(Filter filter)
        {
            var url = $"Property/GetPropertiesWithFilter?keyword={filter.Keyword}&city={filter.City}&status={filter.Status}&maxPrice={filter.PriceRange}&maxArea={filter.AreaSize}&maxBaths={filter.Baths}&maxBed={filter.Beds}&HasGarage={filter.HasGarage}&Two_Stories={filter.Two_Stories}&Laundry_Room={filter.Laundry_Room}&HasPool={filter.HasPool}&HasGarden={filter.HasGarden}&HasElevator={filter.HasElevator}&HasBalcony={filter.HasBalcony}&HasParking={filter.HasParking}&HasCentralHeating={filter.HasCentralHeating}&IsFurnished={filter.IsFurnished}";

            try
            {
                // بناء رابط الاستدعاء باستخدام المعايير المدخلة
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();  // تأكد من نجاح الاستجابة

                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Properties_List>>();
                result = result ?? Enumerable.Empty<Properties_List>();
                result = result.OrderByDescending(U => U.DateAdded)
                                       .ThenBy(U => U.Price);
                return result;  // إرجاع مجموعة فارغة في حالة وجود null
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error URL: " + url);  // Log the exact URL
                Console.WriteLine("Error Message: " + ex.Message);  // Log the error message
                throw new ApplicationException("An error occurred while fetching filtered properties.", ex);
            }
        }



        public async Task CreateUserAsync(User_Create user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null");
            }

            try
            {
                // تحويل الكائن المدخل إلى JSON
                var jsonContent = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // إرسال الطلب إلى الـ API
                var response = await _httpClient.PostAsync("User", content);

                // التحقق من نجاح الاستجابة
                response.EnsureSuccessStatusCode();

                // تسجيل معلومات في حالة نجاح الطلب
                Console.WriteLine($"User {user.Email} created successfully.");
            }
            catch (HttpRequestException httpEx)
            {
                // في حالة وجود خطأ HTTP
                Console.WriteLine($"HTTP Request Error: {httpEx.Message}");
                throw new ApplicationException("An error occurred while creating the user.", httpEx);
            }
            catch (Exception ex)
            {
                // معالجة الأخطاء الأخرى
                Console.WriteLine($"General Error: {ex.Message}");
                throw new ApplicationException("An error occurred while processing the request.", ex);
            }
        }
        public async Task<HttpResponseMessage> UpdateUserInfo(User_Info userInfo)
        {
            // تأكد من ملء خصائص المستخدم
            User_Update user = new User_Update
            {
                F_Name = userInfo.F_Name,
                L_Name = userInfo.L_Name,
                PhoneNumber = userInfo.PhoneNumber,
                Email = userInfo.Email,
                Address = userInfo.Address,
                ProfilePicture = "" // تأكد من إضافة هذه الخاصية إذا كانت موجودة في User_Update
            };

            // تحويل الكائن إلى JSON
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // استخدام الـ id لتحديث المستخدم، و تأكد من أن URL متوافق
            return await _httpClient.PutAsync($"User/UpdateById{userInfo.Id}", content);
        }



        public async Task<User_Info> GetUserInfo(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"User/GetUserByEmail?email={email}");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<User_Info>();
                return result ?? new User_Info();  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }

        public async Task<IEnumerable<Inquity_List>> GetMassagesToUser(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Inquiry/user/{Id}");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Inquity_List>>();
                result = result ?? Enumerable.Empty<Inquity_List>();
                result = result.OrderByDescending(U => U.DateSent);
                return result;  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }

        public async Task<IEnumerable<Inquity_List>> GetMassagesToProperty(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Inquiry/property/{Id}");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Inquity_List>>();
                result = result ?? Enumerable.Empty<Inquity_List>();
                result = result.OrderByDescending(U => U.DateSent);
                return result;  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }

    }
}

    