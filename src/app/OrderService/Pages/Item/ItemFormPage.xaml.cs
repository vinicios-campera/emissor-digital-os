using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.View.Item;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemFormPage : ContentPage
    {
        public ItemFormPage()
        {
            InitializeComponent();
            Title = "Novo produto";
        }

        public ItemFormPage(ItemFormViewModel itemEditViewModel)
        {
            InitializeComponent();
            this.BindingContext = itemEditViewModel;
            Title = "Editar produto";
        }

        private async void AddItem(object sender, EventArgs e)
        {
            var itemView = (ItemFormViewModel)BindingContext;
            var payload = new ProductInsert
            {
                Description = itemView.Description,
                Measure = (Measure)itemView.MeasureTypeSelected,
                UnitaryValue = itemView.UnitaryValue
            };

            var result = await DependencyService.Get<IItemService>().AddItemAsync(payload);

            if (result)
                await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void EditItem(object sender, System.EventArgs e)
        {
            var itemView = (ItemFormViewModel)BindingContext;
            var payload = new ProductUpdate
            {
                Id = itemView.Id.ToGuid(),
                Description = itemView.Description,
                Measure = (Measure)itemView.MeasureTypeSelected,
                UnitaryValue = itemView.UnitaryValue
            };

            var result = await DependencyService.Get<IItemService>().EditItemAsync(payload);

            if (result)
                DependencyService.Get<IDialogService>().ShowSucessToast("Produto editado");
        }

        private async void DeleteItem(object sender, System.EventArgs e)
        {
            var confirm = await App.Current.MainPage.DisplayAlert("Mensagem", "Deseja realmente excluir este produto?", "Sim", "Não");

            if (confirm)
            {
                var itemView = (ItemFormViewModel)BindingContext;

                var result = await DependencyService.Get<IItemService>().DeleteItemAsync(itemView.Id.ToGuid());

                if (result)
                    await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}