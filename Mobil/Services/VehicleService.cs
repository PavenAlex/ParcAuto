using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Mobil.Models;

namespace Mobil.Services
{
    public class VehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/vehicles")
            };
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Vehicle>>("vehicles");
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _httpClient.PostAsJsonAsync("vehicles", vehicle);
        }

        public async Task DeleteVehicleAsync(int id)
        {
            await _httpClient.DeleteAsync($"vehicles/{id}");
        }
    }
}
