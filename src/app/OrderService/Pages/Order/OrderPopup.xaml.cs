using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.View.Order;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPopup : Popup
    {
        private readonly OrderResponse _orderResponse;
        private bool _hasEdited;

        public OrderPopup(OrderResponse os)
        {
            _orderResponse = os;

            InitializeComponent();

            this.BindingContext = new OrderViewModel
            {
                Id = _orderResponse.Id.ToString(),
                ClientName = _orderResponse.Client.Name,
                Identifier = _orderResponse.Identifier,
                Pay = _orderResponse.State == OrderState.Pay,
                Note = _orderResponse.Note
            };
        }

        protected override void LightDismiss()
        {
            Dismiss(_hasEdited);
        }

        private async void DeleteOrder(object sender, System.EventArgs e)
        {
            var result = await App.Current.MainPage.DisplayAlert("Alerta", "Deseja realmente excluir esta O.S.?", "Sim", "Não");
            if (result)
            {
                var context = (OrderViewModel)this.BindingContext;
                await DependencyService.Get<IOrderService>().DeleteOrderAsync(context.Id!.ToGuid());

                Dismiss(true);
            }
        }

        private async void GetPdf(object sender, System.EventArgs e)
        {
            var context = (OrderViewModel)this.BindingContext;
            var result = await DependencyService.Get<IOrderService>().GetOrdersBase64Async(new List<string> { context.Id! });
            result.Item1!.OpenPdfAsync(result.Item2!);
        }

        private async void SwitchPay(object sender, ToggledEventArgs e)
        {
            await DependencyService.Get<IOrderService>().UpdatePayOrders(new List<string> { _orderResponse.Id.ToString() }, e.Value);
            _orderResponse.State = e.Value ? OrderState.Pay : OrderState.None;
            _hasEdited = true;
        }
    }
}