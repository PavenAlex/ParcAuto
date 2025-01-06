using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Mobil.Models;

namespace Mobil.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/") // Modifică dacă e altă adresă API
            };
        }

        // Metoda pentru a obține lista clienților
        public async Task<List<Client>> GetClientsAsync()
        {
            var response = await _httpClient.GetAsync("clients");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Client>>();
            }
            else
            {
                throw new Exception("Nu s-au putut încărca clienții.");
            }
        }
        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"clients/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EditClientAsync(Client client)
        {
            var response = await _httpClient.PutAsJsonAsync($"clients/{client.ID_Client}", client);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("clients", client);
            return response.IsSuccessStatusCode;
        }
    }
}
