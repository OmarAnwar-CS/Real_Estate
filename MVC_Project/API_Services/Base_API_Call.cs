using MVC_Project.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Security.Policy;

namespace MVC_Project.API_Services
{
    public interface IBase_API_Call
    {
        // Any common functionality for API calls can be placed here
        Task<IEnumerable<City_Get>> GetAllCity();
        Task<IEnumerable<Properties_List>> GetPropertyList();
        Task<IEnumerable<Properties_List>> GetFilteredProperties(Filter filter);
        Task<User_Info> GetUserInfo(int id);
        Task<IEnumerable<Properties_List>> GetSortedPropertyPriceList();

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

        public async Task<IEnumerable<Properties_List>> GetPropertyList()
        {
            try
            {
                var response = await _httpClient.GetAsync("Property/GetPropertyList");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Properties_List>>();
                return result ?? Enumerable.Empty<Properties_List>();  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }

        public async Task<IEnumerable<Properties_List>> GetSortedPropertyPriceList()
        {
            try
            {
                var response = await _httpClient.GetAsync("Property/GetPropertyList");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Properties_List>>();
                return result.OrderBy(p=>p.Price) ?? Enumerable.Empty<Properties_List>();  // Return empty collection if null
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
                return result ?? Enumerable.Empty<Properties_List>();  // إرجاع مجموعة فارغة في حالة وجود null
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error URL: " + url);  // Log the exact URL
                Console.WriteLine("Error Message: " + ex.Message);  // Log the error message
                throw new ApplicationException("An error occurred while fetching filtered properties.", ex);
            }
        }





        //https://localhost:7197/api/User?id=1
        public async Task<User_Info> GetUserInfo(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"User?id={id}");
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
    }
}

    