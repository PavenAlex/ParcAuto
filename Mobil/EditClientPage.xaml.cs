using Mobil.Models;
using Mobil.Services;

namespace Mobil
{
    public partial class EditClientPage : ContentPage
    {
        private Client _client;
        private readonly ClientService _clientService;
        public EditClientPage(Client selectedClient)
        {
            InitializeComponent();
            _client = selectedClient; // Setează clientul primit
            _clientService = new ClientService(); // Inițializează serviciul
            BindingContext = _client; // Leagă datele clientului la interfață
            LoadClientDetails(); // Încarcă detaliile
        }
        private void LoadClientDetails()
        {
            NumeEntry.Text = _client.Nume;
            PrenumeEntry.Text = _client.Prenume;
            TelefonEntry.Text = _client.Telefon;
            EmailEntry.Text = _client.Email;
            CNPEntry.Text = _client.CNP;
        }

        // Încărcăm detaliile clientului selectat
        private async void LoadClientDetails(int clientId)
        {
            try
            {
                var client = await _clientService.GetClientsAsync();
                var selectedClient = client.FirstOrDefault(c => c.ID_Client == clientId);

                if (selectedClient != null)
                {
                    NumeEntry.Text = selectedClient.Nume;
                    PrenumeEntry.Text = selectedClient.Prenume;
                    TelefonEntry.Text = selectedClient.Telefon;
                    EmailEntry.Text = selectedClient.Email;
                    CNPEntry.Text = selectedClient.CNP;
                }
                else
                {
                    await DisplayAlert("Eroare", "Clientul nu a fost găsit!", "OK");
                    await Shell.Current.GoToAsync(".."); // Ne întoarcem la pagina anterioară
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca detaliile: {ex.Message}", "OK");
            }
        }

        // Salvăm modificările
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Actualizează datele clientului cu cele introduse în UI
                _client.Nume = NumeEntry.Text;
                _client.Prenume = PrenumeEntry.Text;
                _client.Telefon = TelefonEntry.Text;
                _client.Email = EmailEntry.Text;
                _client.CNP = CNPEntry.Text;

                bool success = await _clientService.EditClientAsync(_client);

                if (success)
                {
                    await DisplayAlert("Succes", "Clientul a fost actualizat!", "OK");
                    await Navigation.PopAsync(); // Întoarce-te la lista de clienți
                }
                else
                {
                    await DisplayAlert("Eroare", "Actualizarea a eșuat!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
