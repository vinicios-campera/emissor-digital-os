using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Order;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace OrderService.View.Order
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private const int _top = 10;

        private bool _isBusy;

        private bool _hasNext = true;
        private int _skip => Items != null ? Items.Count() : 0;

        private FilterOrdersPopupResult? _filter;

        public OrdersViewModel()
        {
            Items = new InfiniteScrollCollection<OrderResponseCustom>
            {
                OnCanLoadMore = () => _hasNext,
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    var result = await GetOrders(false);
                    _hasNext = result != null && result.Count() > 0;
                    IsBusy = false;
                    var resultConverted = result.ToList().ConvertAll(x => new OrderResponseCustom
                    {
                        Amount = x.Amount,
                        Client = x.Client.Name,
                        DateInsert = x.Inserted,
                        Id = x.Id,
                        Identifier = x.Identifier,
                        IsChecked = false,
                        Pay = x.State == OrderState.Pay,
                    });
                    return resultConverted;
                }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsAnyChecked
        {
            get => Items.Where(x => x.IsChecked).Count() > 0;
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public InfiniteScrollCollection<OrderResponseCustom> Items { get; }

        public int OrdersCheckedCount
        {
            get => Items.Where(x => x.IsChecked).Count();
        }

        public async void GetOrders(FilterOrdersPopupResult filter)
        {
            _filter = filter;
            Items.Clear();
            var result = await GetOrders();
            if (result != null)
            {
                _hasNext = result.Count() > 0;
                var resultConverted = result.ToList().ConvertAll(x => new OrderResponseCustom
                {
                    Amount = x.Amount,
                    Client = x.Client.Name,
                    DateInsert = x.Inserted,
                    Id = x.Id,
                    Identifier = x.Identifier,
                    IsChecked = false,
                    Pay = x.State == OrderState.Pay,
                });
                Items.AddRange(resultConverted);
            }
            else
            {
                _hasNext = false;
            }
        }

        public void SomeItemIsChanged()
        {
            OnPropertyChanged(nameof(IsAnyChecked));
            OnPropertyChanged(nameof(OrdersCheckedCount));
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private async Task<IEnumerable<OrderResponse>?> GetOrders(bool showLoading = true)
        {
            try
            {
                return await DependencyService.Get<IOrderService>().GetOrdersAsync(_top, _skip, filter: _filter!.GetFilterOData(), orderby: _filter.GetOrderOData(), showLoading: showLoading);
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}