using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using FinalProject.WebForm.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace FinalProject.WebForm.Services
{
    public class SchedulesServices
    {
        private HttpClient _httpClient;
        public SchedulesServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
        }

        public async Task<List<Schedule>> GetSchedules()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Schedules");
                if (response.IsSuccessStatusCode)
                {
                    var schedules = await response.Content.ReadAsStringAsync();
                    List<Schedule> scheduleList = JsonConvert.DeserializeObject<List<Schedule>>(schedules);
                    return scheduleList;
                }
                else
                {
                    throw new Exception($"Error fetching schedules: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching schedules: {ex.Message}", ex);
            }
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Schedules/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonResponse);
                    return schedule;
                }
                else
                {
                    throw new Exception($"Error fetching schedule with ID {id}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching schedule: {ex.Message}", ex);
            }
        }
        public async Task<List<Schedule>> GetSchedulesBySearch(string search)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Schedules/search/{search}");
                if (response.IsSuccessStatusCode)
                {
                    var schedules = await response.Content.ReadAsStringAsync();
                    List<Schedule> scheduleList = JsonConvert.DeserializeObject<List<Schedule>>(schedules);
                    return scheduleList;
                }
                else
                {
                    throw new Exception($"Error fetching schedules by search term '{search}': {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for schedules: {ex.Message}", ex);
            }
        }
    }
}