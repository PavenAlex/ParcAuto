using Mobil.Models;
using Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mobil
{
    public partial class RezervariPage : ContentPage
    {
        private readonly VehicleService _vehicleService;
        private readonly ClientService _clientService;
        private readonly RezervareService _rezervareService;
        private readonly HttpClient _httpClient;
        private List<Vehicle> _vehicles;
        private List<Client> _clients;

        public RezervariPage()
        {
            InitializeComponent();
            _vehicleService = new VehicleService();
            _clientService = new ClientService();
            _rezervareService = new RezervareService();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/") // Asigură-te că URL-ul este corect!
            };

            LoadVehicles(); // Încarcă lista de vehicule
        }

        // Încarcă vehiculele din baza de date
        private async void LoadVehicles()
        {
            try
            {
                _vehicles = await _vehicleService.GetVehiclesAsync(); // Preia vehiculele
                VehiclePicker.ItemsSource = _vehicles; // Adaugă vehiculele în Picker
                VehiclePicker.ItemDisplayBinding = new Binding("Marca"); // Afișează doar marca

                _clients = await _clientService.GetClientsAsync();
                ClientPicker.ItemsSource = _clients;
                ClientPicker.ItemDisplayBinding = new Binding("Nume");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca vehiculele: {ex.Message}", "OK");
            }
        }

        // Salvează rezervarea
        private async void OnSaveReservationClicked(object sender, EventArgs e)
        {
            try
            {
                // Verifică dacă s-a selectat un vehicul
                var selectedVehicle = (Vehicle)VehiclePicker.SelectedItem;
                var selectedClient = (Client)ClientPicker.SelectedItem;
                if (selectedVehicle == null || selectedClient == null)
                {
                    await DisplayAlert("Eroare", "Selectați un vehicul și un client!", "OK");
                    return;
                }

                // Creează obiectul pentru rezervare
                var newReservation = new
                {
                    ID_Vehicul = selectedVehicle.ID_Vehicul,
                    ID_Client = selectedClient.ID_Client,
                    Data_Start = StartDatePicker.Date,
                    Data_Sfarsit = EndDatePicker.Date,
                    Status = selectedVehicle.Stare
                };

                // Trimite cererea POST către API
                var response = await _httpClient.PostAsJsonAsync("Rezervares", newReservation);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Succes", "Rezervarea a fost salvată!", "OK");
                    await Navigation.PopAsync(); // Revine la pagina anterioară
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Eroare", $"Rezervarea a eșuat: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
