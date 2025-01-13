using OrderService.Api.Client;

namespace OrderService.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddClientAsync(ClientInsert payload);

        Task<bool> DeleteClientAsync(Guid id);

        Task<bool> EditClientAsync(ClientUpdate payload);

        Task<ClientResponse?> GetClientAsync(Guid id);

        Task<CityResponse?> GetCity(string cep);

        Task<IEnumerable<ClientResponse>?> GetClientsAsync(int top, int skip, string? select = null, string? expand = null, string? filter = null, string? orderby = null, bool showLoading = true);
    }
}