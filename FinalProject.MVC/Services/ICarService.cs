using FinalProject.MVC.Models;

namespace FinalProject.MVC.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Dealer>> GetDealersAsync(string token = "");
        Task<Dealer> GetDealerByIdAsync(string id);
        Task<Dealer> CreateDealerAsync(DealerInsert dealerInsert);
        Task<Dealer> UpdateDealerAsync(DealerUpdate dealerUpdate);
        Task DeleteDealerAsync(string id);
    }
}
