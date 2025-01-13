using OrderService.Interfaces;
using OrderService.Models.MainMenu;
using OrderService.Pages.About;
using OrderService.Pages.Client;
using OrderService.Pages.Item;
using OrderService.Pages.Notification;
using OrderService.Pages.Order;
using OrderService.View.MainMenu;
using OrderService.View.Order;
using Xamarin.Forms;

namespace OrderService.Pages.MainMenu
{
    public partial class MainMenuPage : ContentPage
    {
        public MainMenuPage()
        {
            InitializeComponent();

            DependencyService.Get<IUserService>().GetUserAsync().
                ContinueWith(t =>
                {
                    var context = (MainMenuViewModel)this.BindingContext;
                    context.NewMessagesCount = t.Result!.Notifications.Count(x => x.IsUnRead);
                    context.HasNewMessages = context.NewMessagesCount > 0;
                },
                TaskContinuationOptions.NotOnFaulted);
        }

        private async void OnMenuItemTapped(object sender, System.EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            ItemType type = (ItemType)tapGesture.CommandParameter;
            switch (type)
            {
                case ItemType.Clients:
                    await Application.Current.MainPage.Navigation.PushAsync(new ClientsPage());
                    break;

                case ItemType.Products:
                    await Application.Current.MainPage.Navigation.PushAsync(new ItemsPage());
                    break;

                case ItemType.Orders:
                    await Application.Current.MainPage.Navigation.PushAsync(new OrdersPage());
                    break;

                case ItemType.About:
                    await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
                    break;

                case ItemType.NewOs:
                    var result = await App.Current.MainPage.DisplayActionSheet("Selecione o tipo de O.S.", null, null, "Padrão", "Rápida (sem cliente)");
                    if (result == "Padrão")
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new ClientsPage(true));
                    }
                    else if (result == "Rápida (sem cliente)")
                    {
                        var payload = new OrderAddViewModel();
                        await Application.Current.MainPage.Navigation.PushAsync(new OrderAddPage(payload));
                    }
                    break;

                case ItemType.NewClient:
                    await Application.Current.MainPage.Navigation.PushAsync(new ClientFormPage());
                    break;

                case ItemType.Logout:
                    var resultLogout = await Application.Current.MainPage.DisplayAlert("Mensagem", "Deseja realmente sair?", "Sim", "Não");

                    if (resultLogout)
                        Application.Current.MainPage = new NavigationPage(new MainPage(true));

                    break;

                default:
                    break;
            }
        }

        private async void OnMessagedTapped(object sender, System.EventArgs e)
        {
            var context = (MainMenuViewModel)this.BindingContext;
            await Application.Current.MainPage.Navigation.PushAsync(new NotificationsPage());
            context.NewMessagesCount = 0;
            context.HasNewMessages = context.NewMessagesCount > 0;
        }

        private async void OnImageTapped(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
        }
    }
}