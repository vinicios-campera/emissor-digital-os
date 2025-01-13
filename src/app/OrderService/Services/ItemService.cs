using Acr.UserDialogs;
using OrderService.Api.Client;
using OrderService.AppConstant;
using OrderService.Interfaces;
using Xamarin.Essentials;

namespace OrderService.Services
{
    public class ItemService : ServiceBase, IItemService
    {
        public async Task<bool> AddItemAsync(ProductInsert payload)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Adicionando produto", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.AddProductAsync(payload);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Removendo produto", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.DeleteProductAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> EditItemAsync(ProductUpdate payload)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Editando produto", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.UpdateProductAsync(payload);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<ProductResponse?> GetItemAsync(Guid id)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Obtendo produto", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetProductsAsync();
                return result.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProductResponse>?> GetItemsAsync(int top, int skip, string? select = null, string? expand = null, string? filter = null, string? orderby = null, bool showLoading = true)
        {
            try
            {
                if (!showLoading)
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.GetProductsAsync(select, expand, filter, top.ToString(), skip.ToString(), orderby);
                    return result;
                }

                using (var dialog = UserDialogs.Instance.Loading("Obtendo produtos", maskType: MaskType.Black))
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.GetProductsAsync(select, expand, filter, top.ToString(), skip.ToString(), orderby);
                    return result;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }
    }
}