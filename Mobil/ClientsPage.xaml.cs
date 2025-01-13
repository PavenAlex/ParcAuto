using Mobil.Models;
using Mobil.Services;
using System;

namespace Mobil
{
    public partial class ClientsPage : ContentPage
    {
        private readonly ClientService _clientService;

        public ClientsPage()
        {
            InitializeComponent();
            _clientService = new ClientService();
            LoadClients(); 
        }

        private async void LoadClients()
        {
            try
            {
                var clients = await _clientService.GetClientsAsync(); /
                ClientsList.ItemsSource = clients; 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca clienții: {ex.Message}", "OK");
            }
        }

        private async void OnAddClientButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadClients(); 
        }

        private async void OnEditClientClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedClient = button?.CommandParameter as Client;

            if (selectedClient != null)
            {
                await Navigation.PushAsync(new EditClientPage(selectedClient));
            }
        }

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
                bool success = await _clientService.DeleteClientAsync(clientId);
                if (success)
                {
                    await DisplayAlert("Succes", "Clientul a fost șters!", "OK");
                    LoadClients(); 
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
