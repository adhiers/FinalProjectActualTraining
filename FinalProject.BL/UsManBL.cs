using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;
using FinalProject.BL.Helpers;
using FinalProject.DAL;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace FinalProject.BL
{
    public class UsManBL : IUsManBL
    {
        private readonly IUsMan _usManDAL;
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public UsManBL(IUsMan usManDAL, IOptions<AppSettings> appSettings, IConfiguration configuration) 
        {
            _usManDAL = usManDAL;
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }

        public async Task<bool> AddUserToRoleAsync(string email, string roleName)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException(nameof(email), "Email cannot be null or empty");
                }
                if (string.IsNullOrEmpty(roleName))
                {
                    throw new ArgumentNullException(nameof(roleName), "Role name cannot be null or empty");
                }
                var result = await _usManDAL.AddUserToRoleAsync(email, roleName);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateRoleAsync(RoleCreateDTO roleCreateDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(roleCreateDTO.RoleName))
                {
                    throw new ArgumentNullException(nameof(roleCreateDTO), "Role name cannot be null or empty");
                }
                var result = await _usManDAL.CreateRoleAsync(roleCreateDTO.RoleName);
                return result;
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
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException(nameof(email), "Email cannot be null or empty");
                }
                var roles = await _usManDAL.GetRolesByUserAsync(email);
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserWithTokenDTO> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                {
                    throw new ArgumentNullException(nameof(loginDTO), "Login data cannot be null");
                }
                var user = await _usManDAL.LoginAsync(loginDTO.Email, loginDTO.Password);
                if (user == null)
                {
                    return null; // User not found or invalid password
                }

                //add claim
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                var roles = await _usManDAL.GetRolesByUserAsync(user.Email);
                if (roles != null && roles.Count > 0)
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                // Replace this line:
                // var secretKey = IConfiguration.GetValue<string>("AppSettings:Secret");

                // With this line:
                //var secretKeuy = "gnjgjhgjhgjgjhgjhghkgjgjghhjgjhgeqjfkohgjhgjhgjhghgkgjhgjhgjgjhgkjgjhgjgjhgjhg";
                //var secretKey = _configuration.GetValue<string>("AppSettings:Secret");
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                        Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);


                var result = new UserWithTokenDTO
                {
                    Email = user.Email,
                    Token = tokenHandler.WriteToken(token)
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterAsync(RegistrationDTO registrationDTO)
        {
            try
            {
                if (registrationDTO == null)
                {
                    throw new ArgumentNullException(nameof(registrationDTO), "Registration data cannot be null");
                }
                var result = await _usManDAL.RegisterAsync(registrationDTO.Email, registrationDTO.Password);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
