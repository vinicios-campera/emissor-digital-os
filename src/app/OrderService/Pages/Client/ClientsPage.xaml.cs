using OrderService.Api.Client;
using OrderService.Models.Client;
using OrderService.Pages.Order;
using OrderService.View.Client;
using OrderService.View.Order;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsPage : ContentPage
    {
        private readonly bool _creatingOrder;

        private FilterClientsPopupResult _filter;

        public ClientsPage(bool creatingOrder = false)
        {
            InitializeComponent();
            _filter = new FilterClientsPopupResult();
            _creatingOrder = creatingOrder;
            Title = !_creatingOrder ? "Clientes" : "Selecione o cliente";
        }

        protected override void OnAppearing()
        {
            ((ClientsViewModel)this.BindingContext).GetClients(_filter);
            base.OnAppearing();
        }

        private async void AddClient(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ClientFormPage());
        }

        private async void ClientSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var client = (ClientResponse)e.SelectedItem;

            if (!_creatingOrder)
            {
                var payload = new ClientFormViewModel(client.Cep, (Models.Client.ClientType)client.Type, client.Document, client.Id.ToString(), client.Name, client.Cellphone, client.City, client.State);
                await Application.Current.MainPage.Navigation.PushAsync(new ClientFormPage(payload));
            }
            else
            {
                var payload = new OrderAddViewModel
                {
                    ClientId = client.Id.ToString(),
                    ClientName = client.Name
                };
                await Application.Current.MainPage.Navigation.PushAsync(new OrderAddPage(payload));
            }
        }

        private async void FilterClients(object sender, System.EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new ClientsFilterPopup(_filter)!);
            _filter = result ?? new FilterClientsPopupResult();
            ((ClientsViewModel)this.BindingContext).GetClients(_filter);
        }
    }
}