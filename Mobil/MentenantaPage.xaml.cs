﻿using Mobil.Models;
using Mobil.Services;
using System.Net.Http.Json;

namespace Mobil
{
    public partial class MentenantaPage : ContentPage
    {
        private readonly VehicleService _vehicleService;
        private readonly HttpClient _httpClient;

        private List<Vehicle> _vehicles; // Lista de vehicule

        public MentenantaPage()
        {
            InitializeComponent();
            _vehicleService = new VehicleService();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7280/api/") // Înlocuiește cu URL-ul tău
            };

            LoadVehicles(); // Încarcă vehiculele
        }

        // Încarcă lista de vehicule
        private async void LoadVehicles()
        {
            try
            {
                _vehicles = await _vehicleService.GetVehiclesAsync();
                VehiclePicker.ItemsSource = _vehicles;
                VehiclePicker.ItemDisplayBinding = new Binding("Marca"); // Afișează marca
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca vehiculele: {ex.Message}", "OK");
            }
        }

        // Salvarea mentenanței
        private async void OnSaveMentenantaClicked(object sender, EventArgs e)
        {
            try
            {
                // Verifică selecția vehiculului
                var selectedVehicle = (Vehicle)VehiclePicker.SelectedItem;

                if (selectedVehicle == null)
                {
                    await DisplayAlert("Eroare", "Selectați un vehicul!", "OK");
                    return;
                }

                // Creează obiectul mentenanță
                var newMentenanta = new
                {
                    ID_Vehicul = selectedVehicle.ID_Vehicul,
                    Tip_Interventie = TipInterventieEntry.Text,
                    Data_Programare = DataProgramarePicker.Date,
                    Cost_Estimativ = decimal.Parse(CostEstimativEntry.Text)
                };

                // Trimite cererea POST către API
                var response = await _httpClient.PostAsJsonAsync("Mentenantas", newMentenanta);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Succes", "Mentenanța a fost adăugată!", "OK");
                    await Navigation.PopAsync(); // Revine la pagina anterioară
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
