using Mobil.Models;
using Mobil.Services;
using System;

namespace Mobil
{
    public partial class ClientsPage : ContentPage
    {
        private readonly ClientService _clientService;

        // Constructor
        public ClientsPage()
        {
            InitializeComponent();
            _clientService = new ClientService();
            LoadClients(); // Încarcă lista la inițializare
        }

        // Încărcarea listei de clienți
        private async void LoadClients()
        {
            try
            {
                var clients = await _clientService.GetClientsAsync(); // Preia clienții din API
                ClientsList.ItemsSource = clients; // Afișează lista în interfață
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca clienții: {ex.Message}", "OK");
            }
        }

        // Navigare către pagina pentru adăugarea unui nou client
        private async void OnAddClientButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientPage());
        }

        // Reîncărcarea listei de clienți la revenirea pe pagină
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadClients(); // Reîncarcă lista automat la revenirea pe pagină
        }

        // Editarea unui client
        private async void OnEditClientClicked(object sender, EventArgs e)
        {
            // Preluăm clientul selectat din CommandParameter
            var button = sender as Button;
            var selectedClient = button?.CommandParameter as Client;

            if (selectedClient != null)
            {
                // Navighează la pagina de editare cu detaliile clientului
                await Navigation.PushAsync(new EditClientPage(selectedClient));
            }
        }

        // Ștergerea unui client
        private async void OnDeleteClientClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int clientId = (int)button.CommandParameter;

            bool confirm = await DisplayAlert("Confirmare",
                                              "Sigur doriți să ștergeți acest client?",
                                              "Da", "Nu");
            if (!confirm) return;

            try
            {
                // Apelează serviciul pentru ștergerea clientului
                bool success = await _clientService.DeleteClientAsync(clientId);
                if (success)
                {
                    await DisplayAlert("Succes", "Clientul a fost șters!", "OK");
                    LoadClients(); // Reîncarcă lista după ștergere
                }
                else
                {
                    await DisplayAlert("Eroare", "Ștergerea a eșuat.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
