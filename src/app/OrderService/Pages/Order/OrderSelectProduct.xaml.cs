using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Item;
using OrderService.Models.Order;
using OrderService.View.Order;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSelectProduct : Popup<OrderSelectProductResult>
    {
        public OrderSelectProduct(List<ItemResponseCustom> products)
        {
            InitializeComponent();
            ((OrderSelectProductView)this.BindingContext).Products = products;
        }

        private void AddProductToListOfProducts(object sender, System.EventArgs e)
        {
            OrderSelectProductResult result;
            var context = (OrderSelectProductView)this.BindingContext;

            if (string.IsNullOrEmpty(context.Description))
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Digite a descrição do produto");
                return;
            }

            if (context.Amount < 0.01)
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Quantidade deve ser maior que 0");
                return;
            }

            if (context.UnitaryValue < 0.01)
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Valor unitário deve ser maior que 0");
                return;
            }

            if (context.SelectedProduct != null &&
                context.SelectedProduct.Description == context.Description &&
                context.SelectedProduct.Measure == context.MeasureTypeSelected &&
                context.SelectedProduct.UnitaryValue == context.UnitaryValue)
            {
                result = new OrderSelectProductResult
                {
                    Item = context.SelectedProduct,
                    Amount = context.Amount,
                };
            }
            else
            {
                result = new OrderSelectProductResult
                {
                    Item = new ItemResponseCustom
                    {
                        Description = context.Description!.FirstCharToUpper(),
                        UnitaryValue = context.UnitaryValue,
                        Measure = context.MeasureTypeSelected
                    },
                    Amount = context.Amount
                };
            }

            Dismiss(result);
        }
    }
}