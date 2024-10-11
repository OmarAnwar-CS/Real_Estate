using MVC_Project.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace MVC_Project.API_Services
{
    public interface IBase_API_Call
    {
        // Any common functionality for API calls can be placed here
        Task<IEnumerable<City_Get>> GetAllCity();
        Task<IEnumerable<Property_GetAll_Func>> GetAllProperties();
        //Task<IEnumerable<Property_GetAll_Func>> GetFilteredProperties(PropertyFilterDto filter);
        Task<User_Basic> GetUserInfo(int id);

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

        public async Task<IEnumerable<Property_GetAll_Func>> GetAllProperties()
        {
            try
            {
                var response = await _httpClient.GetAsync("Property");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Property_GetAll_Func>>();
                return result ?? Enumerable.Empty<Property_GetAll_Func>();  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }

        //public async Task<IEnumerable<Property_GetAll_Func>> GetFilteredProperties(PropertyFilterDto filter)
        //{


        //https://localhost:7197/api/Property/OrderedByPrice?HasGarage=false&Two_Stories=false&Laundry_Room=false&HasPool=false&HasGarden=false&HasElevator=false&HasBalcony=false&HasParking=false&HasCentralHeating=false&IsFurnished=false&ascending=true


        //    try
        //    {
        //        var response = await _httpClient.GetAsync($"https://localhost:7197/api/Property/OrderedByPrice?HasGarage={filter.HasGarden}&Two_Stories={filter.Two_Stories}&Laundry_Room={filter.Two_Stories}&HasPool={filter.Two_Stories}&HasGarden={filter.Two_Stories}&HasElevator={filter.Two_Stories}&HasBalcony={filter.Two_Stories}&HasParking={filter.Two_Stories}&HasCentralHeating={filter.Two_Stories}&IsFurnished={filter.Two_Stories}&ascending=true");
        //        response.EnsureSuccessStatusCode();  // Ensure the response is successful
        //        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Property_GetAll_Func>>();
        //        return result ?? Enumerable.Empty<Property_GetAll_Func>();  // Return empty collection if null
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception here
        //        throw new ApplicationException("An error occurred while fetching city data.", ex);
        //    }
        //}




        //https://localhost:7197/api/User?id=1
        public async Task<User_Basic> GetUserInfo(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"User?id={id}");
                response.EnsureSuccessStatusCode();  // Ensure the response is successful
                var result = await response.Content.ReadFromJsonAsync<User_Basic>();
                return result ?? new User_Basic();  // Return empty collection if null
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching city data.", ex);
            }
        }
    }
}

    