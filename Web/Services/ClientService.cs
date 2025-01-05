using ParcAuto.Models;
using System.Net.Http.Json;

public class ClientService
{
    private readonly HttpClient _httpClient;

    public ClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Retrieve the list of clients
    public async Task<List<Client>> GetClientsAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7280/api/clients");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Client>>();
    }

    // Add a new client
    public async Task<Client> AddClientAsync(Client client)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7280/api/clients", client);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Client>();
    }

    // Retrieve a client by ID
    public async Task<Client> GetClientByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7280/api/clients/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Client>();
    }
}
