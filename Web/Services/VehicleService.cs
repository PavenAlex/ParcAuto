using ParcAuto.Models;

public class VehicleService
{
    private readonly HttpClient _httpClient;

    public VehicleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7280/api/vehicles");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Vehicle>>();
    }

    public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7280/api/vehicles", vehicle);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Vehicle>();
    }

    public async Task<Vehicle> GetVehicleByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7280/api/vehicles/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Vehicle>();
    }
}
