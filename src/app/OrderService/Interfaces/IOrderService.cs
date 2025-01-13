using OrderService.Api.Client;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Task<byte[]?> AddOrderAsync(OrderInsert payload);

        Task<bool> DeleteOrderAsync(Guid id);

        Task<bool> DeleteOrdersAsync(List<string> ids);

        Task<bool> EditOrderAsync(Guid id, OrderState orderState, bool showLoading = true);

        Task<OrderResponse?> GetOrderAsync(Guid id);

        Task<IEnumerable<OrderResponse>?> GetOrdersAsync(int top, int skip, string? select = null, string? expand = null, string? filter = null, string? orderby = null, bool showLoading = true);

        Task<(byte[]?, string?)> GetOrdersBase64Async(List<string> ids);

        Task UpdatePayOrders(List<string> ids, bool isPay);
    }
}