namespace Mobil
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Navigare către pagina Vehicule
        private async void OnVehiclesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VehiclesPage());
        }

        // Navigare către pagina Facturi
        private async void OnFacturiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacturiPage());
        }
    }
}
