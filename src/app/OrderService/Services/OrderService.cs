using Acr.UserDialogs;
using OrderService.Extensions;
using OrderService.Api.Client;
using OrderService.AppConstant;
using OrderService.Interfaces;
using Xamarin.Essentials;

namespace OrderService.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        public async Task<byte[]?> AddOrderAsync(OrderInsert payload)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Criando O.S.", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.AddOrderAsync(payload);
                return result.Pdf;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Removendo O.S.", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.DeleteOrderAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteOrdersAsync(List<string> ids)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Removendo ordens", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                foreach (var id in ids)
                    await _client.DeleteOrderAsync(id.ToGuid());
                return true;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> EditOrderAsync(Guid id, OrderState orderState, bool showLoading = true)
        {
            try
            {
                string[] ids = { id.ToString() };

                if (!showLoading)
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.UpdateOrderStateAsync(ids, orderState);
                    return result;
                }

                using (var dialog = UserDialogs.Instance.Loading("Editando O.S.", maskType: MaskType.Black))
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.UpdateOrderStateAsync(ids, orderState);
                    return result;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<OrderResponse?> GetOrderAsync(Guid id)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Obtendo O.S.", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetOrdersAsync();
                return result.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<OrderResponse>?> GetOrdersAsync(int top, int skip, string? select = null, string? expand = null, string? filter = null, string? orderby = null, bool showLoading = true)
        {
            try
            {
                if (!showLoading)
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.GetOrdersAsync(select, expand, filter, top.ToString(), skip.ToString(), orderby);
                    return result;
                }

                using (var dialog = UserDialogs.Instance.Loading("Obtendo ordens", maskType: MaskType.Black))
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.GetOrdersAsync(select, expand, filter, top.ToString(), skip.ToString(), orderby);
                    return result;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<(byte[]?, string?)> GetOrdersBase64Async(List<string> ids)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading(ids.Count > 0 ? "Obtendo ordens" : "Obtendo ordem", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetOrderAsPdfAsync(ids);
                return (result.Pdf, result.FileName);
            }
            catch (Exception ex)
            {
                OnError(ex);
                return (null, null);
            }
        }

        public async Task UpdatePayOrders(List<string> ids, bool isPay)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Atualizando ordens", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                await _client.UpdateOrderStateAsync(ids, isPay ? OrderState.Pay : OrderState.None);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }
    }
}