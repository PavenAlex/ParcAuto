using System.Formats.Tar;
using System.Net.Http.Json;
using Mobil.Models;

namespace Mobil
{
    public partial class EditVehiclePage : ContentPage
    {
        private Vehicle _vehicle; 

        public EditVehiclePage(Vehicle vehicle)
        {
            InitializeComponent();
            _vehicle = vehicle;

            MarcaEntry.Text = _vehicle.Marca;
            ModelEntry.Text = _vehicle.Model;
            AnFabricatieEntry.Text = _vehicle.An_Fabricatie.ToString();
            TipCombustibilEntry.Text = _vehicle.Tip_Combustibil;
            StareEntry.Text = _vehicle.Stare;
            KilometrajEntry.Text = _vehicle.Kilometraj.ToString();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _vehicle.Marca = MarcaEntry.Text;
                _vehicle.Model = ModelEntry.Text;
                _vehicle.An_Fabricatie = int.Parse(AnFabricatieEntry.Text);
                _vehicle.Tip_Combustibil = TipCombustibilEntry.Text;
                _vehicle.Stare = StareEntry.Text;
                _vehicle.Kilometraj = int.Parse(KilometrajEntry.Text);

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7280/api/") 
                };

                var response = await httpClient.PutAsJsonAsync($"vehicles/{_vehicle.ID_Vehicul}", _vehicle);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Succes", "Vehiculul a fost actualizat!", "OK");
                    await Shell.Current.GoToAsync(".."); 
                    MessagingCenter.Send(this, "RefreshVehicles"); 
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Eroare", $"Actualizarea a eșuat: {error}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
