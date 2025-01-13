using Kernel.Data.Repository.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order, Guid>
    {
    }
}