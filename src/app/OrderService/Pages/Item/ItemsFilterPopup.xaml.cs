using OrderService.Extensions;
using OrderService.Models.Item;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsFilterPopup : Popup<FilterItemsPopupResult>
    {
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(ItemsFilterPopup), null);
        public static readonly BindableProperty FilterItemTypeProperty = BindableProperty.Create(nameof(FilterItemType), typeof(FilterItemType), typeof(ItemsFilterPopup), FilterItemType.All);
        public static readonly BindableProperty OrderByItemTypeProperty = BindableProperty.Create(nameof(OrderByItemType), typeof(OrderByItemType), typeof(ItemsFilterPopup), OrderByItemType.Name);

        public ItemsFilterPopup(FilterItemsPopupResult filterItemsPopupResult)
        {
            InitializeComponent();

            if (filterItemsPopupResult != null)
            {
                Description = filterItemsPopupResult.Description!;
                FilterItemType = filterItemsPopupResult.ItemType;
                OrderByItemType = filterItemsPopupResult.OrderBy;
            }

            foreach (var item in Enum.GetValues(typeof(FilterItemType)))
            {
                var radio = new Plugin.InputKit.Shared.Controls.RadioButton
                {
                    TextFontSize = 12,
                    Color = (Color)App.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Text = ((FilterItemType)item).Description(),
                    IsChecked = item.Equals(FilterItemType)
                };
                FilterTypes.Children.Add(radio);
            }

            foreach (var item in Enum.GetValues(typeof(OrderByItemType)))
            {
                var radio = new Plugin.InputKit.Shared.Controls.RadioButton
                {
                    TextFontSize = 12,
                    Color = (Color)App.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Text = ((OrderByItemType)item).Description(),
                    IsChecked = item.Equals(OrderByItemType)
                };
                OrderTypes.Children.Add(radio);
            }
        }

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        public FilterItemType FilterItemType
        {
            get => (FilterItemType)GetValue(FilterItemTypeProperty);
            set
            {
                SetValue(FilterItemTypeProperty, value);
            }
        }

        public OrderByItemType OrderByItemType
        {
            get => (OrderByItemType)GetValue(OrderByItemTypeProperty);
            set
            {
                SetValue(OrderByItemTypeProperty, value);
            }
        }

        private void Filter(object sender, EventArgs e)
        {
            var result = new FilterItemsPopupResult
            {
                Description = Description,
                ItemType = FilterItemType,
                OrderBy = OrderByItemType
            };
            Dismiss(result);
        }

        private void FilterTypesSelectedItemChanged(object sender, EventArgs e)
        {
            var radioGroup = (Plugin.InputKit.Shared.Controls.RadioButtonGroupView)sender;
            var filterType = (FilterItemType)Enum.GetValues(typeof(FilterItemType)).GetValue(radioGroup.SelectedIndex);
            FilterItemType = filterType;
        }

        private void OrderTypesSelectedItemChanged(object sender, EventArgs e)
        {
            var radioGroup = (Plugin.InputKit.Shared.Controls.RadioButtonGroupView)sender;
            var filterType = (OrderByItemType)Enum.GetValues(typeof(OrderByItemType)).GetValue(radioGroup.SelectedIndex);
            OrderByItemType = filterType;
        }

        private void ResetFilter(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}