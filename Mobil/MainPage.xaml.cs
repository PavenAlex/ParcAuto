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
        private async void OnClientsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientsPage());
        }
        private async void OnRezervariButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RezervariPage());
        }
        private async void OnMentenantaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MentenantaPage());
        }
    }
}
