using OrderService.Api.Client;
using OrderService.Models.Item;
using OrderService.View.Item;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        private FilterItemsPopupResult _filter;

        public ItemsPage()
        {
            InitializeComponent();
            _filter = new FilterItemsPopupResult();
        }

        protected override void OnAppearing()
        {
            ((ItemsViewModel)this.BindingContext).GetItems(_filter);
            base.OnAppearing();
        }

        private async void AddItem(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ItemFormPage());
        }

        private async void FilterItems(object sender, System.EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new ItemsFilterPopup(_filter)!);
            _filter = result ?? new FilterItemsPopupResult();
            ((ItemsViewModel)this.BindingContext).GetItems(_filter);
        }

        private async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ProductResponse)e.SelectedItem;
            var payload = new ItemFormViewModel(item.Id.ToString(), item.Description, (Models.Item.MeasureType)item.Measure, item.UnitaryValue);

            await Application.Current.MainPage.Navigation.PushAsync(new ItemFormPage(payload));
        }
    }
}