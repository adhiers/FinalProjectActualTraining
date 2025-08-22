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
    public class SchedulingsServices
    {
        private HttpClient _httpClient;
        public SchedulingsServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
        }

        public async Task<List<Scheduling>> GetSchedules()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Schedulings");
                if (response.IsSuccessStatusCode)
                {
                    var schedules = await response.Content.ReadAsStringAsync();
                    List<Scheduling> scheduleList = JsonConvert.DeserializeObject<List<Scheduling>>(schedules);
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

        public async Task<Scheduling> GetScheduleById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Schedulings/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Scheduling schedule = JsonConvert.DeserializeObject<Scheduling>(jsonResponse);
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
        public async Task<List<Scheduling>> GetSchedulesBySearch(string search)
        {
            try
            {
                // If search is empty, return all schedules
                if (string.IsNullOrWhiteSpace(search))
                {
                    return await GetSchedules();
                }

                var response = await _httpClient.GetAsync($"/api/Schedulings/search/{search}");
                if (response.IsSuccessStatusCode)
                {
                    var schedules = await response.Content.ReadAsStringAsync();
                    List<Scheduling> scheduleList = JsonConvert.DeserializeObject<List<Scheduling>>(schedules);
                    return scheduleList;
                }
                else
                {
                    //return empty list
                    List<Scheduling> scheduleList;
                    return scheduleList = new List<Scheduling>();
                    //throw new Exception($"Error fetching schedules by search term '{search}': {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for schedules: {ex.Message}", ex);
            }
        }
    }
}