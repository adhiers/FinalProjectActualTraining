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
        public Task<Dealer> CreateDealerAsync(DealerInsert dealerInsert)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDealerAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Dealer> GetDealerByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dealer>> GetDealersAsync(string token = "")
        {
            throw new NotImplementedException();
        }

        public Task<Dealer> UpdateDealerAsync(DealerUpdate dealerUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
