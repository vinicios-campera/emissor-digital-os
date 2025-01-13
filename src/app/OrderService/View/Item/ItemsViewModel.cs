using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Item;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace OrderService.View.Item
{
    public class ItemsViewModel : INotifyPropertyChanged
    {
        private const int _top = 10;

        private bool _isBusy;

        private bool _hasNext = true;
        private int _skip => Items != null ? Items.Count() : 0;

        private FilterItemsPopupResult? _filter;

        public ItemsViewModel()
        {
            Items = new InfiniteScrollCollection<ProductResponse>
            {
                OnCanLoadMore = () => _hasNext,
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    var result = await GetItems(false);
                    _hasNext = result != null && result.Count() > 0;
                    IsBusy = false;
                    return result;
                }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public InfiniteScrollCollection<ProductResponse> Items { get; }

        public async void GetItems(FilterItemsPopupResult filter)
        {
            _filter = filter;
            Items.Clear();
            var result = await GetItems();
            if (result != null)
            {
                _hasNext = result.Count() > 0;
                Items.AddRange(result);
            }
            else
            {
                _hasNext = false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private async Task<IEnumerable<ProductResponse>?> GetItems(bool showLoading = true)
        {
            try
            {
                return await DependencyService.Get<IItemService>().GetItemsAsync(_top, _skip, filter: _filter!.GetFilterOData(), orderby: _filter.GetOrderOData(), showLoading: showLoading);
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}