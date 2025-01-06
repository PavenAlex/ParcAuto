using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobil.Models
{
    public class Factura
    {
        public int ID_Factura { get; set; }
        public int ID_Rezervare { get; set; }
        public DateTime Data_Emitere { get; set; }
        public decimal Suma_Totala { get; set; }
        public string Status_Plata { get; set; }
        public ICommand DeleteCommand { get; }

        public Factura()
        {
            DeleteCommand = new Command(async () => await OnDelete());
        }

        private async Task OnDelete()
        {
            bool confirm = await App.Current.MainPage.DisplayAlert(
                "Confirmare",
                $"Sigur dorești să ștergi factura {ID_Factura}?",
                "Da",
                "Nu");

            if (!confirm) return;

            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7280/api/")
                };

                var response = await httpClient.DeleteAsync($"Facturi/{ID_Factura}");

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Succes", "Factura a fost ștearsă!", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Eroare", "Nu s-a putut șterge factura.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
