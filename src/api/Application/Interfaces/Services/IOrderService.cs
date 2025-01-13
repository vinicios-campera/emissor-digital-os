using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using System.Linq.Expressions;

namespace OrderService.Application.Interfaces.Services
{
    public interface IOrderService
    {
        OrderPdfResponse? AddOrder(OrderInsert payload);

        bool UpdateOrderState(string[] ids, OrderState state);

        IEnumerable<OrderResponse>? GetOrders(Expression<Func<Order, bool>> expression);

        bool DeleteOrder(Guid id);

        OrderPdfResponse? GetOrderAsPdf(string[] ids);

        byte[]? GetOrderPdfById(Guid id);

        OrderDetailResponse? GetOrderDetailById(Guid id);
    }
}