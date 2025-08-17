using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using FinalProject.WebForm.Models;

namespace FinalProject.WebForm.Services
{
    public class AccountServices
    {
        private HttpClient _httpClient;
        private string token;
        public AccountServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
        }

        public async Task<UserViewModel> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(loginViewModel);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Account/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var userViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserViewModel>(responseContent);
                    token = userViewModel.Token; // Store the token for future requests
                    return userViewModel;
                }
                else
                {
                    throw new Exception("Login failed: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                throw new Exception("An error occurred while logging in: " + ex.Message);
            }
        }

    }
}