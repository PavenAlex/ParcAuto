using System;
using Mobil.Models;
using System.Net.Http.Json;

namespace Mobil;

public partial class AddClientPage : ContentPage
{
	public AddClientPage()
	{
		InitializeComponent();
	}
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            // Crează un nou client pe baza datelor introduse
            var newClient = new Client
            {
                Nume = NumeEntry.Text,
                Prenume = PrenumeEntry.Text,
                Telefon = TelefonEntry.Text,
                Email = EmailEntry.Text,
                CNP = CNPEntry.Text,
                Data_Inregistrarii = DateTime.Now
            };

            // Trimite clientul către API pentru a-l salva în baza de date
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/")
            };

            var response = await httpClient.PostAsJsonAsync("Clients", newClient);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Succes", "Clientul a fost adăugat cu succes!", "OK");
                MessagingCenter.Send(this, "RefreshClients");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Eroare", "Nu s-a putut adăuga clientul.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
        }
    }
}