using FinalProject.MVC.Models;

namespace FinalProject.MVC.Services
{
    public interface IDealerCarService
    {
        Task<IEnumerable<DealerCar>> GetAllDealerCarsAsync(string token = "");
        Task<DealerCar> GetDealerCarByIdAsync(string dealerCarId);
        Task<DealerCar> AddDealerCarAsync(DealerCarInsert dealerCarInsert);
        Task<DealerCar> UpdateDealerCarAsync(DealerCarUpdate dealerCarUpdate);
        Task DeleteDealerCarAsync(string dealerCarId);
    }
}
