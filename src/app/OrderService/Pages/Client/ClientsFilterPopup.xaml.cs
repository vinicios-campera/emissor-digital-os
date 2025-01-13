using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Client;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsFilterPopup : Popup<FilterClientsPopupResult>
    {
        public static readonly BindableProperty ClientNameProperty = BindableProperty.Create(nameof(ClientName), typeof(string), typeof(ClientsFilterPopup), null);
        public static readonly BindableProperty FilterClientTypeProperty = BindableProperty.Create(nameof(FilterClientType), typeof(FilterClientType), typeof(ClientsFilterPopup), FilterClientType.Physical);
        public static readonly BindableProperty OrderByClientTypeProperty = BindableProperty.Create(nameof(OrderByClientType), typeof(OrderByClientType), typeof(ClientsFilterPopup), OrderByClientType.Name);
        public static readonly BindableProperty RegisterInProperty = BindableProperty.Create(nameof(RegisterIn), typeof(string), typeof(ClientsFilterPopup), DateTime.Now.ToString("dd/MM/yyyy"));
        public static readonly BindableProperty RegisterUntilProperty = BindableProperty.Create(nameof(RegisterUntil), typeof(string), typeof(ClientsFilterPopup), DateTime.Now.ToString("dd/MM/yyyy"));

        public ClientsFilterPopup(FilterClientsPopupResult filterClientsPopupResult)
        {
            InitializeComponent();

            if (filterClientsPopupResult != null)
            {
                ClientName = filterClientsPopupResult.Name!;
                FilterClientType = filterClientsPopupResult.ClientType;
                OrderByClientType = filterClientsPopupResult.OrderBy;
                RegisterIn = filterClientsPopupResult.RegisterIn.ToDateFromDateTime();
                RegisterUntil = filterClientsPopupResult.RegisterUntil.ToDateFromDateTime();
            }

            foreach (var item in Enum.GetValues(typeof(FilterClientType)))
            {
                var radio = new Plugin.InputKit.Shared.Controls.RadioButton
                {
                    TextFontSize = 12,
                    Color = (Color)App.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Text = ((FilterClientType)item).Description(),
                    IsChecked = item.Equals(FilterClientType)
                };
                FilterTypes.Children.Add(radio);
            }

            foreach (var item in Enum.GetValues(typeof(OrderByClientType)))
            {
                var radio = new Plugin.InputKit.Shared.Controls.RadioButton
                {
                    TextFontSize = 12,
                    Color = (Color)App.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Text = ((OrderByClientType)item).Description(),
                    IsChecked = item.Equals(OrderByClientType)
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

        public FilterClientType FilterClientType
        {
            get => (FilterClientType)GetValue(FilterClientTypeProperty);
            set
            {
                SetValue(FilterClientTypeProperty, value);
            }
        }

        public OrderByClientType OrderByClientType
        {
            get => (OrderByClientType)GetValue(OrderByClientTypeProperty);
            set
            {
                SetValue(OrderByClientTypeProperty, value);
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
            var result = new FilterClientsPopupResult
            {
                Name = ClientName,
                ClientType = FilterClientType,
                OrderBy = OrderByClientType
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
            var filterType = (FilterClientType)Enum.GetValues(typeof(FilterClientType)).GetValue(radioGroup.SelectedIndex);
            FilterClientType = filterType;
        }

        private void OrderTypesSelectedItemChanged(object sender, EventArgs e)
        {
            var radioGroup = (Plugin.InputKit.Shared.Controls.RadioButtonGroupView)sender;
            var filterType = (OrderByClientType)Enum.GetValues(typeof(OrderByClientType)).GetValue(radioGroup.SelectedIndex);
            OrderByClientType = filterType;
        }

        private void ResetFilter(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}