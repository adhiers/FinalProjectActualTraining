using FinalProject.MVC.Models;
using System.Text.Json;
using NuGet.Common;

namespace FinalProject.MVC.Services
{
    public class DealerCarService : IDealerCarService
    {
        private readonly HttpClient _httpClient;
        public DealerCarService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7291/");
        }
        public async Task<DealerCar> AddDealerCarAsync(DealerCarInsert dealerCarInsert)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(dealerCarInsert);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/DealerCars", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<DealerCar>(data);
                    if (result == null)
                    {
                        throw new ArgumentException("Dealer car creation failed, no data returned.");
                    }
                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Error creating dealer car: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteDealerCarAsync(string dealerCarId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/DealerCars/{dealerCarId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error deleting dealer car: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DealerCar>> GetAllDealerCarsAsync(string token = "")
        {
            _httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/DealerCars");
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var dealerCars = JsonSerializer.Deserialize<IEnumerable<DealerCar>>(data);
                if (dealerCars == null)
                {
                    throw new ArgumentException("No dealer cars found.");
                }
                return dealerCars ?? Enumerable.Empty<DealerCar>();
            }
            else
            {
                throw new HttpRequestException($"Error fetching dealer cars: {response.ReasonPhrase}");
            }
        }

        public async Task<DealerCar> GetDealerCarByIdAsync(string Id)
        {
            var response = await _httpClient.GetAsync($"api/DealerCars/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var dealerCar = JsonSerializer.Deserialize<DealerCar>(data);
                if (dealerCar == null)
                {
                    throw new ArgumentException($"Dealer Car id: {Id} not found");
                }
                return dealerCar;
            }
            else
            {
                throw new HttpRequestException($"Error fetching sales person: {response.ReasonPhrase}");
            }
        }

        public async Task<DealerCar> UpdateDealerCarAsync(DealerCarUpdate dealerCarUpdate)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(dealerCarUpdate);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/DealerCars/{dealerCarUpdate.DealerCarId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<DealerCar>(data);
                    if (result == null)
                    {
                        throw new ArgumentException("Dealer car update failed, no data returned.");
                    }
                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Error updating sales person: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
