using AutoMapper;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Constants;
using System.Linq.Expressions;
using Kernel.Data.AutoMapper.Extensions;
using Kernel.Net.Http.Interfaces;
using Kernel.Toolkit.Extensions;

namespace OrderService.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IProductRepository _productRepository;
        private readonly Expression<Func<Product, bool>> _defaultExpression;

        public ProductService(IMapper mapper, IUserAccessor userAccessor, IProductRepository productRepository)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _productRepository = productRepository;
            _defaultExpression = x => x.UserEmail == userAccessor.GetUserEmail();
        }

        public bool AddProduct(ProductInsert payload) => _productRepository.Insert(_mapper.Map<Product>(payload, GetMapperUserId()));

        public bool AddProducts(IEnumerable<ProductInsert> payloads) => _productRepository.Insert(_mapper.Map<IEnumerable<Product>>(payloads));

        public bool DeleteProduct(Guid id) => _productRepository.Delete(id);

        public IEnumerable<ProductResponse>? GetProducts(Expression<Func<Product, bool>> expression)
            => _mapper.Map<IEnumerable<ProductResponse>>(_productRepository.Find(_defaultExpression.And(expression)));

        public bool UpdateProduct(ProductUpdate payload)
        {
            var existing = _productRepository.FindById(payload.Id).ValidateIsNull();
            var mapped = _mapper.Map(payload, existing);
            return _productRepository.Update(mapped!);
        }

        private Action<IMappingOperationOptions> GetMapperUserId()
            => new Dictionary<string, object?>
            {
                {
                    AutomapperConstants.PARAM_USER_EMAIL,
                    _userAccessor.GetUserEmail()
                }
            }.GetParams();
    }
}