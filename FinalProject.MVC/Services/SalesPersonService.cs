using FinalProject.MVC.Models;
using System.Text.Json;
using NuGet.Common;

namespace FinalProject.MVC.Services
{
    public class SalesPersonService : ISalesPersonService
    {
        private readonly HttpClient _httpClient;
        public SalesPersonService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7291/");
        }

        public async Task<SalesPerson> CreateSalesPersonAsync(SalesPersonInsert salesPersonInsert)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(salesPersonInsert);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/SalesPeople", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<SalesPerson>(data);
                    if (result == null)
                    {
                        throw new ArgumentException("Sales person creation failed, no data returned.");
                    }
                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Error creating sales person: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteSalesPersonAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SalesPeople/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error deleting sales person: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SalesPerson> GetSalesPersonByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/SalesPeople/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var salesPerson = JsonSerializer.Deserialize<SalesPerson>(data);
                if (salesPerson == null)
                {
                    throw new ArgumentException($"Sales Person id: {id} not found");
                }
                return salesPerson;
            }
            else
            {
                throw new HttpRequestException($"Error fetching sales person: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<SalesPerson>> GetSalesPersonsAsync(string token = "")
        {
            _httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/SalesPeople");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var salesPersons = JsonSerializer.Deserialize<IEnumerable<SalesPerson>>(data);
                return salesPersons ?? Enumerable.Empty<SalesPerson>();
            }
            else
            {
                throw new HttpRequestException($"Error fetching sales people: {response.ReasonPhrase}");
            }
        }

        public async Task<SalesPerson> UpdateSalesPersonAsync(SalesPersonUpdate salesPersonUpdate)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(salesPersonUpdate);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/SalesPeople/{salesPersonUpdate.SPId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<SalesPerson>(data);
                    if (result == null)
                    {
                        throw new ArgumentException("Sales person update failed, no data returned.");
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
