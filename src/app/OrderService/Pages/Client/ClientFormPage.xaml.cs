using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Pages.MainMenu;
using OrderService.View.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientFormPage : ContentPage
    {
        public ClientFormPage()
        {
            InitializeComponent();
            Title = "Novo cliente";
        }

        public ClientFormPage(ClientFormViewModel clientEditViewModel)
        {
            InitializeComponent();
            this.BindingContext = clientEditViewModel;
            Title = "Editar cliente";
        }

        private async void CepTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length == 9)
            {
                var result = await DependencyService.Get<IClientService>().GetCity(e.NewTextValue);
                if (result != null && !string.IsNullOrEmpty(result.Cep))
                {
                    var clientView = (ClientFormViewModel)BindingContext;
                    clientView.State = result.Uf;
                    clientView.City = result.Localidade;

                    BindingContext = clientView;
                }
            }
        }

        private void DocumentChanged(object sender, TextChangedEventArgs e)
        {
            var clientView = (ClientFormViewModel)BindingContext;

            if (e.NewTextValue.Length <= 14)
                clientView.ClientType = Models.Client.ClientType.Fisica;
            else
                clientView.ClientType = Models.Client.ClientType.Juridica;

            BindingContext = clientView;
        }

        private async void AddClient(object sender, EventArgs e)
        {
            var clientView = (ClientFormViewModel)BindingContext;
            var payload = new ClientInsert
            {
                Name = clientView.Name,
                Document = clientView.Document,
                Cep = clientView.Cep,
                Cellphone = clientView.Cellphone,
                City = clientView.City,
                State = clientView.State,
            };

            var result = await DependencyService.Get<IClientService>().AddClientAsync(payload);

            if (result)
            {
                var previousPageIndex = Application.Current.MainPage.Navigation.NavigationStack.Count - 2;
                var previousPageType = Application.Current.MainPage.Navigation.NavigationStack[previousPageIndex];

                if (previousPageType.GetType() == typeof(MainMenuPage))
                    DependencyService.Get<IDialogService>().ShowSucessToast("Cliente adicionado");

                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async void EditClient(object sender, EventArgs e)
        {
            var clientView = (ClientFormViewModel)BindingContext;
            var payload = new ClientUpdate
            {
                Id = clientView.Id.ToGuid(),
                Name = clientView.Name,
                Document = clientView.Document,
                Cep = clientView.Cep,
                Cellphone = clientView.Cellphone,
                City = clientView.City,
                State = clientView.State,
            };

            var result = await DependencyService.Get<IClientService>().EditClientAsync(payload);

            if (result)
                DependencyService.Get<IDialogService>().ShowSucessToast("Cliente editado");
        }

        private async void DeleteClient(object sender, EventArgs e)
        {
            var confirm = await App.Current.MainPage.DisplayAlert("Mensagem", "Deseja realmente excluir este cliente?", "Sim", "Não");

            if (confirm)
            {
                var clientView = (ClientFormViewModel)BindingContext;

                var result = await DependencyService.Get<IClientService>().DeleteClientAsync(clientView.Id.ToGuid());

                if (result)
                    await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}