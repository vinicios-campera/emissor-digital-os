using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Application.Interfaces.Services
{
    public interface IProductService
    {
        bool AddProduct(ProductInsert payload);

        bool AddProducts(IEnumerable<ProductInsert> payloads);

        bool UpdateProduct(ProductUpdate payload);

        IEnumerable<ProductResponse>? GetProducts(Expression<Func<Product, bool>> expression);

        bool DeleteProduct(Guid id);
    }
}