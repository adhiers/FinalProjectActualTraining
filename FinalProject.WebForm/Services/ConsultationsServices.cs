using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.WebForm.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinalProject.WebForm.Services
{
    public class ConsultationsServices
    {
        private HttpClient _httpClient;
        public ConsultationsServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
        }

        public async Task<List<Consultation>> GetConsultations()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Consultations");
                if (response.IsSuccessStatusCode)
                {
                    var consultations = await response.Content.ReadAsStringAsync();
                    List<Consultation> consultationList = JsonConvert.DeserializeObject<List<Consultation>>(consultations);
                    return consultationList;
                }
                else
                {
                    throw new Exception($"Error fetching consultations: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching consultations: {ex.Message}", ex);
            }
        }
        public async Task<Consultation> GetConsultationById(string consultId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Consultations/{consultId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Consultation consultation = JsonConvert.DeserializeObject<Consultation>(jsonResponse);
                    return consultation;
                }
                else
                {
                    throw new Exception($"Error fetching consultation with ID {consultId}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching consultation: {ex.Message}", ex);
            }
        }

        public async Task<Consultation> AddConsultation(ConsultationInsert consultationInsert)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(consultationInsert);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Consultations", content);
                if (response.IsSuccessStatusCode)
                {
                    var addedConsultation = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Consultation>(addedConsultation);
                }
                else
                {
                    throw new Exception($"Error adding consultation: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding consultation: {ex.Message}", ex);
            }
        }

        public async Task UpdateConsultation(ConsultationUpdate consultationUpdate)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(consultationUpdate);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/Consultations/{consultationUpdate.ConsultId}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error updating consultation: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating consultation: {ex.Message}", ex);
            }
        }

        public async Task DeleteConsultation(string consultId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Consultations/{consultId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error deleting consultation: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting consultation: {ex.Message}", ex);
            }
        }
    }
}