using System.Net.Http.Json;
using Mobil.Models;

namespace Mobil.Services
{
    public class FacturaService
    {
        private readonly HttpClient _httpClient;

        public FacturaService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7280/api/") 
            };
        }

        // Obține lista tuturor facturilor
        public async Task<List<Factura>> GetFacturiAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Factura>>("facturas");
        }

        // Adaugă o factură nouă
        public async Task<bool> AddFacturaAsync(Factura factura)
        {
            var response = await _httpClient.PostAsJsonAsync("facturas", factura);
            return response.IsSuccessStatusCode;
        }

        // Șterge o factură
        public async Task<bool> DeleteFacturaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Facturas/{id}");
            return response.IsSuccessStatusCode; // Returnează true sau false
        }
    }
}
