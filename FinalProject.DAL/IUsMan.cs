using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.DAL
{
    public interface IUsMan
    {
        //registration asp identity
        Task<bool> RegisterAsync(string email, string password);
        //login asp identity
        Task<IdentityUser> LoginAsync(string email, string password);
        //create role
        Task<bool> CreateRoleAsync(string roleName);
        //add user to role
        Task<bool> AddUserToRoleAsync(string email, string roleName);
        Task<List<string>> GetRolesByUserAsync(string email);
    }
}
