using OrderService.Api.Client;

namespace OrderService.Interfaces
{
    public interface IItemService
    {
        Task<bool> AddItemAsync(ProductInsert payload);

        Task<bool> DeleteItemAsync(Guid id);

        Task<bool> EditItemAsync(ProductUpdate payload);

        Task<ProductResponse?> GetItemAsync(Guid id);

        Task<IEnumerable<ProductResponse>?> GetItemsAsync(int top, int skip, string? select = null, string? expand = null, string? filter = null, string? orderby = null, bool showLoading = true);
    }
}