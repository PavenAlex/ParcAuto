using Mobil.Models;
using Mobil.Services;

namespace Mobil
{
    public partial class FacturiPage : ContentPage
    {
        private readonly FacturaService _facturaService;

        public FacturiPage()
        {
            InitializeComponent();
            _facturaService = new FacturaService();
            LoadFacturi(); 
        }

        private async void LoadFacturi()
        {
            try
            {
                var facturi = await _facturaService.GetFacturiAsync();
                FacturiList.ItemsSource = facturi; 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca facturile: {ex.Message}", "OK");
            }
        }
        private async void OnDeleteFacturaClicked(object sender, EventArgs e)
        {
            var button = sender as Button; 
            var ID_Factura = (int)button.CommandParameter; 

            bool confirm = await DisplayAlert(
                "Confirmare",
                "Sigur dorești să ștergi această factură?",
                "Da",
                "Nu");

            if (!confirm) return; 

            try
            {
                var success = await _facturaService.DeleteFacturaAsync(ID_Factura); 

                if (success)
                {
                    await DisplayAlert("Succes", "Factura a fost ștearsă!", "OK");
                    LoadFacturi(); 
                }
                else
                {
                    await DisplayAlert("Eroare", "Nu s-a putut șterge factura.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
            }
        }
    }
}
