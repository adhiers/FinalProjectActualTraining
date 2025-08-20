using FinalProject.MVC.Models;

namespace FinalProject.MVC.Services
{
    public interface ISalesPersonService
    {
        Task<IEnumerable<SalesPerson>> GetSalesPersonsAsync(string token = "");
        Task<SalesPerson> GetSalesPersonByIdAsync(string id);
        Task<SalesPerson> CreateSalesPersonAsync(SalesPersonInsert salesPersonInsert);
        Task<SalesPerson> UpdateSalesPersonAsync(SalesPersonUpdate salesPersonUpdate);
        Task DeleteSalesPersonAsync(string id);
    }
}
