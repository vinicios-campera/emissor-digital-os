using Kernel.Data.Repository.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        bool Insert(IEnumerable<Product> entities);
    }
}