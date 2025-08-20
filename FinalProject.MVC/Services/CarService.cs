using FinalProject.MVC.Models;
using NuGet.Common;
using System.Text.Json;

namespace FinalProject.MVC.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7291/");
        }
        public async Task<Car> CreateCarAsync(CarInsert carInsert)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(carInsert);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Cars", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Car>(data);
                    if (result == null)
                    {
                        throw new ArgumentException("Dealer creation failed, no data returned.");
                    }
                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Error creating dealer: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCarAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Cars/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error deleting dealer: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Car> GetCarByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/Cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var car = JsonSerializer.Deserialize<Car>(data);
                if (car == null)
                {
                    throw new ArgumentException($"Dealer id: {id} not found");
                }
                return car;
            }
            else
            {
                throw new HttpRequestException($"Error fetching dealer: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Car>> GetCarsAsync(string token = "")
        {
            _httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/Cars");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var dealers = JsonSerializer.Deserialize<IEnumerable<Car>>(data);
                return dealers ?? Enumerable.Empty<Car>();
            }
            else
            {
                throw new HttpRequestException($"Error fetching dealers: {response.ReasonPhrase}");
            }
        }

        public async Task<Car> UpdateCarAsync(CarUpdate carUpdate)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(carUpdate);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Cars/{carUpdate.CarId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Car>(data);
                    if (result == null)
                    {
                        throw new ArgumentException("Dealer update failed, no data returned.");
                    }
                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Error updating dealer: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
