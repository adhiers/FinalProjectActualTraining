using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.DAL
{
    public class UsManDAL : IUsMan
    {
        private readonly UserManager<IdentityUser> _usermManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsManDAL(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _usermManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> AddUserToRoleAsync(string email, string roleName)
        {
            try
            {
                var user = await _usermManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return false; // User not found
                }
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    return false; // Role does not exist
                }
                var result = await _usermManager.AddToRoleAsync(user, roleName);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    var result = await _roleManager.CreateAsync(role);
                    return result.Succeeded;
                }
                return true; // Role already exists
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<string>> GetRolesByUserAsync(string email)
        {
            try
            {
                var user = await _usermManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new List<string>(); // User not found
                }
                var roles = await _usermManager.GetRolesAsync(user);
                return roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IdentityUser> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _usermManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return null; // User not found
                }
                var result = await _usermManager.CheckPasswordAsync(user, password);
                if (result)
                {
                    return user; // Login successful
                }
                else
                {
                    return null; // Invalid password
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            try
            {
                var user = new IdentityUser { UserName = email, Email = email };
                var result = await _usermManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
