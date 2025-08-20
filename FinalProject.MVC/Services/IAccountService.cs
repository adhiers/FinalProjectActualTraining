using FinalProject.MVC.Models;

namespace FinalProject.MVC.Services
{
    public interface IAccountService
    {
        Task<UserViewModel> Login(LoginViewModel loginViewModel);
    }
}
