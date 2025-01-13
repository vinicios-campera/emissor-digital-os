using OrderService.Api.Client;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Client;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace OrderService.View.Client
{
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private const int _top = 10;

        private bool _isBusy;

        private bool _hasNext = true;
        private int _skip => Items != null ? Items.Count() : 0;

        private FilterClientsPopupResult? _filter;

        public ClientsViewModel()
        {
            Items = new InfiniteScrollCollection<ClientResponse>
            {
                OnCanLoadMore = () => _hasNext,
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    var result = await GetClients(false);
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

        public InfiniteScrollCollection<ClientResponse> Items { get; }

        public async void GetClients(FilterClientsPopupResult filter)
        {
            _filter = filter;
            Items.Clear();
            var result = await GetClients();
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

        private async Task<IEnumerable<ClientResponse>?> GetClients(bool showLoading = true)
        {
            try
            {
                return await DependencyService.Get<IClientService>().GetClientsAsync(_top, _skip, filter: _filter!.GetFilterOData(), orderby: _filter.GetOrderOData(), showLoading: showLoading);
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}