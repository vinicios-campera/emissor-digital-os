using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Order;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersFilterPopup : Popup<FilterOrdersPopupResult>
    {
        public static readonly BindableProperty ClientNameProperty = BindableProperty.Create(nameof(ClientName), typeof(string), typeof(OrdersFilterPopup), null);
        public static readonly BindableProperty FilterOrderTypeProperty = BindableProperty.Create(nameof(FilterOrderType), typeof(FilterOrderType), typeof(OrdersFilterPopup), FilterOrderType.All);
        public static readonly BindableProperty OrderByOrderTypeProperty = BindableProperty.Create(nameof(OrderByOrderType), typeof(OrderByOrderType), typeof(OrdersFilterPopup), OrderByOrderType.Identifier);
        public static readonly BindableProperty RegisterInProperty = BindableProperty.Create(nameof(RegisterIn), typeof(string), typeof(OrdersFilterPopup), DateTime.Now.ToString("dd/MM/yyyy"));
        public static readonly BindableProperty RegisterUntilProperty = BindableProperty.Create(nameof(RegisterUntil), typeof(string), typeof(OrdersFilterPopup), DateTime.Now.ToString("dd/MM/yyyy"));

        public OrdersFilterPopup(FilterOrdersPopupResult filterOrdersPopupResult)
        {
            InitializeComponent();

            if (filterOrdersPopupResult != null)
            {
                ClientName = filterOrdersPopupResult.ClientName!;
                FilterOrderType = filterOrdersPopupResult.OrderType;
                OrderByOrderType = filterOrdersPopupResult.OrderBy;
                RegisterIn = filterOrdersPopupResult.RegisterIn.ToDateFromDateTime();
                RegisterUntil = filterOrdersPopupResult.RegisterUntil.ToDateFromDateTime();
            }

            foreach (var item in Enum.GetValues(typeof(FilterOrderType)))
            {
                var radio = new Plugin.InputKit.Shared.Controls.RadioButton
                {
                    TextFontSize = 12,
                    Color = (Color)App.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Text = ((FilterOrderType)item).Description(),
                    IsChecked = item.Equals(FilterOrderType)
                };
                FilterTypes.Children.Add(radio);
            }

            foreach (var item in Enum.GetValues(typeof(OrderByOrderType)))
            {
                var radio = new Plugin.InputKit.Shared.Controls.RadioButton
                {
                    TextFontSize = 12,
                    Color = (Color)App.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Text = ((OrderByOrderType)item).Description(),
                    IsChecked = item.Equals(OrderByOrderType)
                };
                OrderTypes.Children.Add(radio);
            }
        }

        public string ClientName
        {
            get => (string)GetValue(ClientNameProperty);
            set
            {
                SetValue(ClientNameProperty, value);
            }
        }

        public FilterOrderType FilterOrderType
        {
            get => (FilterOrderType)GetValue(FilterOrderTypeProperty);
            set
            {
                SetValue(FilterOrderTypeProperty, value);
            }
        }

        public OrderByOrderType OrderByOrderType
        {
            get => (OrderByOrderType)GetValue(OrderByOrderTypeProperty);
            set
            {
                SetValue(OrderByOrderTypeProperty, value);
            }
        }

        public string RegisterIn
        {
            get => (string)GetValue(RegisterInProperty);
            set
            {
                SetValue(RegisterInProperty, value);
            }
        }

        public string RegisterUntil
        {
            get => (string)GetValue(RegisterUntilProperty);
            set
            {
                SetValue(RegisterUntilProperty, value);
            }
        }

        private void Filter(object sender, EventArgs e)
        {
            var result = new FilterOrdersPopupResult
            {
                ClientName = ClientName,
                OrderType = FilterOrderType,
                OrderBy = OrderByOrderType
            };

            var haveDismiss = true;

            try { result.RegisterIn = RegisterIn.ToNullableDateTime("dd/MM/yyyy"); }
            catch
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Data inicio inválida");
                haveDismiss = false;
            }

            try { result.RegisterUntil = RegisterUntil.ToNullableDateTime("dd/MM/yyyy"); }
            catch
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Data final inválida");
                haveDismiss = false;
            }

            if (haveDismiss)
                Dismiss(result);
        }

        private void FilterTypesSelectedItemChanged(object sender, EventArgs e)
        {
            var radioGroup = (Plugin.InputKit.Shared.Controls.RadioButtonGroupView)sender;
            var filterType = (FilterOrderType)Enum.GetValues(typeof(FilterOrderType)).GetValue(radioGroup.SelectedIndex);
            FilterOrderType = filterType;
        }

        private void OrderTypesSelectedItemChanged(object sender, EventArgs e)
        {
            var radioGroup = (Plugin.InputKit.Shared.Controls.RadioButtonGroupView)sender;
            var filterType = (OrderByOrderType)Enum.GetValues(typeof(OrderByOrderType)).GetValue(radioGroup.SelectedIndex);
            OrderByOrderType = filterType;
        }

        private void ResetFilter(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}