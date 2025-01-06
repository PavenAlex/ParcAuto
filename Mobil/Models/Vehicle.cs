using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobil.Models
{
    public class Vehicle
    {
        public int ID_Vehicul { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public int An_Fabricatie { get; set; }
        public string Tip_Combustibil { get; set; }
        public string Stare { get; set; }
        public int Kilometraj { get; set; }
        public ICommand ReserveCommand { get; }

        public Vehicle()
        {
            ReserveCommand = new Command(OnReserve);
        }

        private async void OnReserve()
        {
            try
            {
                // Trimite cererea către API pentru a rezerva vehiculul
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7280/api/Rezervares")
                };

                var response = await httpClient.PostAsJsonAsync("Rezervares", new
                {
                    ID_Vehicul = this.ID_Vehicul,
                    ID_Client = 1,
                    Data_Start = DateTime.Now,
                    Data_Sfarsit = DateTime.Now.AddDays(7),
                    Status = "Rezervat"
                });

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Succes", $"Vehiculul {Marca} {Model} a fost rezervat!", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Eroare", $"Rezervarea a eșuat.{ID_Vehicul}", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
