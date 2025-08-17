using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using FinalProject.WebForm.Models;
using Newtonsoft.Json;

namespace FinalProject.WebForm.Services
{
    public class GuestsServices
    {
        private HttpClient _httpClient;
        public GuestsServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");

            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); // Replace with actual token if needed
        }

        public async Task<List<Guest>> GetGuests()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Guests");
                if (response.IsSuccessStatusCode)
                {
                    var guests = await response.Content.ReadAsStringAsync();
                    List<Guest> guestList = JsonConvert.DeserializeObject<List<Guest>>(guests);
                    return guestList;
                }
                else
                {
                    throw new Exception($"Error fetching guests: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching guests: {ex.Message}", ex);
            }
        }

        public async Task<Guest> GetGuestById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Guests/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Guest guest = JsonConvert.DeserializeObject<Guest>(jsonResponse);
                    return guest;
                }
                else
                {
                    throw new Exception($"Error fetching guest with ID {id}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching guest with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task DeleteGuest(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Guests/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error deleting guest with ID {id}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting guest with ID {id}: {ex.Message}", ex);
            }
        }
    }
}