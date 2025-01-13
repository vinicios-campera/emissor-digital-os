using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Item;
using OrderService.Models.Order;
using OrderService.Pages.MainMenu;
using OrderService.View.Order;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderAddPage : ContentPage
    {
        public static readonly BindableProperty AmountOrderProperty =
            BindableProperty.Create(nameof(AmountOrder), typeof(double), typeof(OrderAddPage), default(double));

        public static readonly BindableProperty IsLoadingProperty =
            BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(OrderAddPage), default(bool));

        public static readonly BindableProperty ItemsToOrderProperty =
                    BindableProperty.Create(nameof(ItemsToOrder), typeof(ObservableCollection<OrderProductInsert>), typeof(OrderAddPage), new ObservableCollection<OrderProductInsert>());

        public static readonly BindableProperty ProductProperty =
            BindableProperty.Create(nameof(Product), typeof(string), typeof(OrderAddPage), default(string));

        public static readonly BindableProperty SearchProductCommandProperty =
            BindableProperty.Create(nameof(SearchProductCommand), typeof(ICommand), typeof(OrderAddPage));

        public OrderAddPage(OrderAddViewModel orderAddView)
        {
            InitializeComponent();
            this.BindingContext = orderAddView;

            if (!string.IsNullOrEmpty(orderAddView.ClientName))
                Title = orderAddView.ClientName;

            SetValue(SearchProductCommandProperty, new Command<string>(SearchProducts));
        }

        public double AmountOrder
        {
            get { return (double)GetValue(AmountOrderProperty); }
            set
            {
                SetValue(AmountOrderProperty, value);
            }
        }

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set
            {
                SetValue(IsLoadingProperty, value);
            }
        }

        public ObservableCollection<OrderProductInsert> ItemsToOrder
        {
            get { return (ObservableCollection<OrderProductInsert>)GetValue(ItemsToOrderProperty); }
            set
            {
                SetValue(ItemsToOrderProperty, value);
            }
        }

        public string Product
        {
            get { return (string)GetValue(ProductProperty); }
            set
            {
                SetValue(ProductProperty, value);
            }
        }

        public ICommand SearchProductCommand
        {
            get { return (ICommand)GetValue(SearchProductCommandProperty); }
        }

        private async void AddOrder(object sender, EventArgs e)
        {
            if (ItemsToOrder == null || ItemsToOrder.Count() < 1)
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Adicione produtos a O.S.");
                return;
            }

            var context = (OrderAddViewModel)this.BindingContext;

            Guid? clientId = null;

            if (!string.IsNullOrEmpty(context.ClientId))
                clientId = context.ClientId.ToGuid();

            var payload = new OrderInsert
            {
                ClientId = clientId,
                Discount = context.Discount,
                Finish = context.FinishIn.ToDateTime("dd/MM/yyyy"),
                Start = context.StartIn.ToDateTime("dd/MM/yyyy"),
                Products = ItemsToOrder,
                Note = context.Note
            };

            var result = await DependencyService.Get<IOrderService>().AddOrderAsync(payload);

            if (result != null)
            {
                result.OpenPdfAsync($"OS.pdf");
                App.Current.MainPage = new NavigationPage(new MainMenuPage());
                ItemsToOrder.Clear();
                this.BindingContext = new OrderAddViewModel();
            }
        }

        private void AddProductToListOfProducts(OrderSelectProductResult resultModal)
        {
            if (resultModal != null)
            {
                var itemExisting = ItemsToOrder.Where(x => x.Description == resultModal.Item!.Description).FirstOrDefault();
                if (itemExisting != null)
                {
                    var payload = new OrderProductInsert
                    {
                        Id = resultModal.Item!.Id,
                        Amount = itemExisting.Amount + resultModal.Amount,
                        Description = resultModal.Item.Description,
                        Measure = (Measure)resultModal.Item.Measure,
                        UnitaryValue = resultModal.Item.UnitaryValue
                    };
                    ItemsToOrder.Remove(itemExisting);
                    ItemsToOrder.Add(payload);
                }
                else
                {
                    var payload = new OrderProductInsert
                    {
                        Amount = resultModal.Amount,
                        Description = resultModal.Item!.Description,
                        Measure = (Measure)resultModal.Item.Measure,
                        UnitaryValue = resultModal.Item.UnitaryValue
                    };
                    ItemsToOrder.Add(payload);
                }

                var amount = 0.00;
                foreach (var item in ItemsToOrder)
                    amount += (item.Amount * item.UnitaryValue);

                AmountOrder = amount;

                Product = string.Empty;
            }
        }

        private void DeleteProductToListOfProducts(object sender, EventArgs e)
        {
            var product = (OrderProductInsert)((TappedEventArgs)e).Parameter;
            ItemsToOrder.Remove(product);

            var amount = 0.00;
            foreach (var item in ItemsToOrder)
                amount += (item.Amount * item.UnitaryValue);

            AmountOrder = amount;

            Product = string.Empty;
        }

        private async void ProductNotExist(object sender, EventArgs e)
        {
            var data = new List<ItemResponseCustom>();
            var resultModal = await App.Current.MainPage.Navigation.ShowPopupAsync(new OrderSelectProduct(data)!);
            AddProductToListOfProducts(resultModal!);
        }

        private void ProductoTextChanged(object sender, TextChangedEventArgs e)
        {
            IsLoading = true;
        }

        private async void SearchProducts(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var data = new List<ItemResponseCustom>();

                var result = await DependencyService.Get<IItemService>().GetItemsAsync(100, 0, filter: $"contains(toUpper({nameof(ProductResponse.Description)}), toUpper('{input}'))", showLoading: false);

                if (result != null && result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var product = new ItemResponseCustom { Id = item.Id, Description = item.Description, Measure = (Models.Item.MeasureType)item.Measure, UnitaryValue = item.UnitaryValue };
                        data.Add(product);
                    }

                    var resultModal = await App.Current.MainPage.Navigation.ShowPopupAsync(new OrderSelectProduct(data)!);
                    AddProductToListOfProducts(resultModal!);
                }
            }

            IsLoading = false;
        }
    }
}