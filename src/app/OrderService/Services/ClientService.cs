using Acr.UserDialogs;
using OrderService.Api.Client;
using OrderService.AppConstant;
using OrderService.Interfaces;
using Xamarin.Essentials;

namespace OrderService.Services
{
    public class ClientService : ServiceBase, IClientService
    {
        public async Task<bool> AddClientAsync(ClientInsert payload)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Adicionando cliente", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.AddClientAsync(payload);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteClientAsync(Guid id)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Removendo cliente", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.DeleteClientAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> EditClientAsync(ClientUpdate payload)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Editando cliente", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.UpdateClientAsync(payload);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<CityResponse?> GetCity(string cep)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Obtendo cidade", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetCityByCepAsync(cep);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<ClientResponse?> GetClientAsync(Guid id)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Obtendo cliente", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetClientsAsync();
                return result.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<ClientResponse>?> GetClientsAsync(int top, int skip, string? select = null, string? expand = null, string? filter = null, string? orderby = null, bool showLoading = true)
        {
            try
            {
                if (!showLoading)
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.GetClientsAsync(select, expand, filter, top.ToString(), skip.ToString(), orderby);
                    return result;
                }

                using (var dialog = UserDialogs.Instance.Loading("Obtendo clientes", maskType: MaskType.Black))
                {
                    var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                    _client.SetBearerToken(token);
                    var result = await _client.GetClientsAsync(select, expand, filter, top.ToString(), skip.ToString(), orderby);
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