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
    public class TestDrivesServices
    {
        private HttpClient _httpClient;
        public TestDrivesServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
        }

        public async Task<List<TestDrive>> GetTestDrives()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/TestDrives");
                if (response.IsSuccessStatusCode)
                {
                    var testDrives = await response.Content.ReadAsStringAsync();
                    List<TestDrive> testDriveList = JsonConvert.DeserializeObject<List<TestDrive>>(testDrives);
                    return testDriveList;
                }
                else
                {
                    throw new Exception($"Error fetching test drives: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching test drives: {ex.Message}", ex);
            }
        }

        public async Task<TestDrive> GetTestDriveById(string testDriveId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/TestDrives/{testDriveId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    TestDrive testDrive = JsonConvert.DeserializeObject<TestDrive>(jsonResponse);
                    return testDrive;
                }
                else
                {
                    throw new Exception($"Error fetching test drive with ID {testDriveId}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching test drive: {ex.Message}", ex);
            }
        }

        public async Task<TestDrive> AddTestDrive(TestDriveInsert testDriveInsert)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(testDriveInsert);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/TestDrives", content);
                if (response.IsSuccessStatusCode)
                {
                    var addedTestDrive = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TestDrive>(addedTestDrive);
                }
                else
                {
                    throw new Exception($"Error creating test drive: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating test drive: {ex.Message}", ex);
            }
        }

        public async Task UpdateTestDrive(TestDriveUpdate testDriveUpdate)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(testDriveUpdate);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/TestDrives/{testDriveUpdate.Tdid}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error updating consultation: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating test drive: {ex.Message}", ex);
            }
        }

        public async Task DeleteTestDrive(string testDriveId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/TestDrives/{testDriveId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error deleting test drive: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting test drive: {ex.Message}", ex);
            }
        }
    }
}