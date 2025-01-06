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
            LoadFacturi(); // Încărcare automată
        }

        private async void LoadFacturi()
        {
            try
            {
                var facturi = await _facturaService.GetFacturiAsync();
                FacturiList.ItemsSource = facturi; // Populează lista
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Nu s-au putut încărca facturile: {ex.Message}", "OK");
            }
        }
        private async void OnDeleteFacturaClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Preia butonul apăsat
            var ID_Factura = (int)button.CommandParameter; // Preia ID-ul facturii asociate

            bool confirm = await DisplayAlert(
                "Confirmare",
                "Sigur dorești să ștergi această factură?",
                "Da",
                "Nu");

            if (!confirm) return; // Oprește dacă utilizatorul anulează

            try
            {
                var success = await _facturaService.DeleteFacturaAsync(ID_Factura); // Apelează API-ul

                if (success)
                {
                    await DisplayAlert("Succes", "Factura a fost ștearsă!", "OK");
                    LoadFacturi(); // Reîncarcă lista după ștergere
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
