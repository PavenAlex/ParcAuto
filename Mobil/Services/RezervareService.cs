using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobil.Models;
using System.Net.Http.Json;

namespace Mobil.Services
{
    public class RezervareService
    {
        private readonly HttpClient _httpClient;

        public RezervareService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/")
            };
        }

        // Adaugă o rezervare
        public async Task<bool> AddRezervareAsync(Rezervare rezervare)
        {
            var response = await _httpClient.PostAsJsonAsync("rezervares", rezervare);
            return response.IsSuccessStatusCode;
        }
    }
}
