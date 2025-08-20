using FinalProject.MVC.Models;

namespace FinalProject.MVC.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCarsAsync(string token = "");
        Task<Car> GetCarByIdAsync(string id);
        Task<Car> CreateCarAsync(CarInsert carInsert);
        Task<Car> UpdateCarAsync(CarUpdate carUpdate);
        Task DeleteCarAsync(string id);
    }
}
