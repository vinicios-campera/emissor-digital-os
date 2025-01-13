using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Order;
using OrderService.Pages.Client;
using OrderService.View.Order;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private FilterOrdersPopupResult _filter;

        public OrdersPage()
        {
            InitializeComponent();
            _filter = new FilterOrdersPopupResult();
        }

        protected override void OnAppearing()
        {
            ((OrdersViewModel)this.BindingContext).GetOrders(_filter);
            base.OnAppearing();
        }

        private async void AddOrder(object sender, EventArgs e)
        {
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
        }

        private async void FilterOrders(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new OrdersFilterPopup(_filter)!);
            _filter = result ?? new FilterOrdersPopupResult();
            ((OrdersViewModel)this.BindingContext).GetOrders(_filter);
        }

        private void ListOfOrdersCheckChanged(object sender, CheckedChangedEventArgs e)
        {
            ((OrdersViewModel)this.BindingContext).SomeItemIsChanged();
        }

        private async void OrderSelected(object sender, ItemTappedEventArgs e)
        {
            var item = (OrderResponseCustom)e.Item;
            var os = await DependencyService.Get<IOrderService>().GetOrderAsync(item.Id);
            var result = await Navigation.ShowPopupAsync(new OrderPopup(os!)!);

            if (result != null && (bool)result)
                ((OrdersViewModel)this.BindingContext).GetOrders(_filter);
        }

        private async void RemoveMultipleOrders(object sender, EventArgs e)
        {
            if (((OrdersViewModel)BindingContext).IsAnyChecked)
            {
                var result = await App.Current.MainPage.DisplayAlert("Mensagem", "Deseja realmente excluir as O.S. selecionadas?", "Sim", "Não");
                if (result)
                {
                    var ids = ((OrdersViewModel)BindingContext).Items.Where(x => x.IsChecked).ToList();
                    await DependencyService.Get<IOrderService>().DeleteOrdersAsync(ids.Select(x => x.Id.ToString()).ToList());
                    ((OrdersViewModel)this.BindingContext).GetOrders(_filter);
                    ((OrdersViewModel)this.BindingContext).SomeItemIsChanged();
                }
            }
            else
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Selecione pelo menos uma O.S.");
            }
        }

        private async void SetPayMultipleOrders(object sender, EventArgs e)
        {
            if (((OrdersViewModel)BindingContext).IsAnyChecked)
            {
                var result = await App.Current.MainPage.DisplayAlert("Mensagem", "Deseja realmente atualizar as O.S. selecionadas para pagas?", "Sim", "Não");
                if (result)
                {
                    var ids = ((OrdersViewModel)BindingContext).Items.Where(x => x.IsChecked).ToList();
                    await DependencyService.Get<IOrderService>().UpdatePayOrders(ids.Select(x => x.Id.ToString()).ToList(), true);
                    ((OrdersViewModel)this.BindingContext).GetOrders(_filter);
                    ((OrdersViewModel)this.BindingContext).SomeItemIsChanged();
                }
            }
            else
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Selecione pelo menos uma O.S.");
            }
        }

        private async void ShareMultipleOrders(object sender, EventArgs e)
        {
            if (((OrdersViewModel)BindingContext).IsAnyChecked)
            {
                var ids = ((OrdersViewModel)BindingContext).Items.Where(x => x.IsChecked).ToList();
                var result = await DependencyService.Get<IOrderService>().GetOrdersBase64Async(ids.Select(x => x.Id.ToString()).ToList());
                result.Item1!.OpenPdfAsync(result.Item2!);
            }
            else
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Selecione pelo menos uma O.S.");
            }
        }
    }
}