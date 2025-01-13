using Mobil.Models;
using Mobil.Services;
using System.Net.Http.Json;

namespace Mobil
{
    public partial class VehiclesPage : ContentPage
    {
        private readonly VehicleService _vehicleService;

        public VehiclesPage()
        {
            InitializeComponent();
            _vehicleService = new VehicleService();
            LoadVehicles();
        }

        private async void LoadVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetVehiclesAsync();
                VehiclesList.ItemsSource = vehicles; 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca vehiculele: {ex.Message}", "OK");
            }
        }
        private async void OnAddVehicleClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddVehiclePage()); 
        }
        private async void OnEditVehicleClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedVehicle = button?.CommandParameter as Vehicle;

            if (selectedVehicle != null)
            {
                await Navigation.PushAsync(new EditVehiclePage(selectedVehicle));
            }
        }
        private async void OnDeleteVehicleClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedVehicle = button?.CommandParameter as Vehicle;

            if (selectedVehicle != null)
            {
                bool confirm = await DisplayAlert("Confirmare", $"Ești sigur că vrei să ștergi vehiculul {selectedVehicle.Marca} {selectedVehicle.Model}?", "Da", "Nu");
                if (confirm)
                {
                    try
                    {
                        var handler = new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                        };

                        var httpClient = new HttpClient(handler)
                        {
                            BaseAddress = new Uri("https://localhost:7280/api/") 
                        };

                        var response = await httpClient.DeleteAsync($"vehicles/{selectedVehicle.ID_Vehicul}");

                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Succes", "Vehiculul a fost șters!", "OK");

                            LoadVehicles();
                        }
                        else
                        {
                            var error = await response.Content.ReadAsStringAsync();
                            await DisplayAlert("Eroare", $"Ștergerea a eșuat: {error}", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
                    }
                }
            }
        }
        private async void OnFacturiClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacturiPage());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<AddVehiclePage>(this, "RefreshVehicles", (sender) =>
            {
                LoadVehicles(); 
            });
            MessagingCenter.Subscribe<EditVehiclePage>(this, "RefreshVehicles", (sender) =>
            {
                LoadVehicles(); 
            });
        }
    }
}
