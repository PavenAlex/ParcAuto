using Mobil.Models;
using Mobil.Services;
using System.Net.Http.Json;

namespace Mobil
{
    public partial class MentenantaPage : ContentPage
    {
        private readonly VehicleService _vehicleService;
        private readonly HttpClient _httpClient;

        private List<Vehicle> _vehicles; 

        public MentenantaPage()
        {
            InitializeComponent();
            _vehicleService = new VehicleService();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/") 
            };

            LoadVehicles(); 
        }

        private async void LoadVehicles()
        {
            try
            {
                _vehicles = await _vehicleService.GetVehiclesAsync();
                VehiclePicker.ItemsSource = _vehicles;
                VehiclePicker.ItemDisplayBinding = new Binding("Marca"); 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca vehiculele: {ex.Message}", "OK");
            }
        }

        private async void OnSaveMentenantaClicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVehicle = (Vehicle)VehiclePicker.SelectedItem;

                if (selectedVehicle == null)
                {
                    await DisplayAlert("Eroare", "Selectați un vehicul!", "OK");
                    return;
                }

                var newMentenanta = new
                {
                    ID_Vehicul = selectedVehicle.ID_Vehicul,
                    Tip_Interventie = TipInterventieEntry.Text,
                    Data_Programare = DataProgramarePicker.Date,
                    Cost_Estimativ = decimal.Parse(CostEstimativEntry.Text)
                };

                var response = await _httpClient.PostAsJsonAsync("Mentenantas", newMentenanta);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Succes", "Mentenanța a fost adăugată!", "OK");
                    await Navigation.PopAsync(); 
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Eroare", $"Adăugarea a eșuat: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
