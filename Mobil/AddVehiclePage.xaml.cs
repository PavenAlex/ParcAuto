using System.Formats.Tar;
using System.Net.Http.Json;

namespace Mobil
{
    public partial class AddVehiclePage : ContentPage
    {
        public AddVehiclePage()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var newVehicle = new
                {
                    Marca = MarcaEntry.Text,
                    Model = ModelEntry.Text,
                    An_Fabricatie = int.Parse(AnFabricatieEntry.Text),
                    Tip_Combustibil = TipCombustibilEntry.Text,
                    Stare = StareEntry.Text,
                    Kilometraj = int.Parse(KilometrajEntry.Text)
                };

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7280/api/") 
                };

                var response = await httpClient.PostAsJsonAsync("vehicles", newVehicle);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Succes", "Vehiculul a fost adăugat!", "OK");
                    MessagingCenter.Send(this, "RefreshVehicles");

                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync(); 
                    await DisplayAlert("Eroare", $"Adăugarea a eșuat: {error}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
