using FinalProject.MVC.Models;
using NuGet.Common;
using System.Text.Json;

namespace FinalProject.MVC.Services
{
    public class DealerService : IDealerService
    {
        private readonly HttpClient _httpClient;
        public DealerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7291/");
        }
        public async Task<Dealer> CreateDealerAsync(DealerInsert dealerInsert)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(dealerInsert);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Dealers", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Dealer>(data);
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

        public async Task DeleteDealerAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Dealers/{id}");
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

        public async Task<Dealer> GetDealerByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/Dealers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var dealer = JsonSerializer.Deserialize<Dealer>(data);
                if (dealer == null)
                {
                    throw new ArgumentException($"Dealer id: {id} not found");
                }
                return dealer;
            }
            else
            {
                throw new HttpRequestException($"Error fetching dealer: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Dealer>> GetDealersAsync(string token = "")
        {
            _httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/Dealers");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var dealers = JsonSerializer.Deserialize<IEnumerable<Dealer>>(data);
                return dealers ?? Enumerable.Empty<Dealer>();
            }
            else
            {
                throw new HttpRequestException($"Error fetching dealers: {response.ReasonPhrase}");
            }
        }

        public async Task<Dealer> UpdateDealerAsync(DealerUpdate dealerUpdate)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(dealerUpdate);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Dealers/{dealerUpdate.DealerId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Dealer>(data);
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
