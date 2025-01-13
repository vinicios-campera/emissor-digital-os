using AutoMapper;
using Kernel.Toolkit.Extensions;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Constants;

namespace OrderService.Infrastructure.AutoMapper.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductResponse>();

            CreateMap<ProductInsert, Product>()
                .ForMember(d => d.UserEmail, x => x.MapFrom((s, d, x, c) => c.Items[AutomapperConstants.PARAM_USER_EMAIL].ConvertTo<string>()))
                .ForMember(d => d.Description, x => x.MapFrom(s => s.Description!.ToUppercaseFirst()))
                .AfterMap((s, d) => d.SetInsertedDate());

            CreateMap<ProductUpdate, Product>()
                .ForMember(d => d.Description, x => x.MapFrom(s => s.Description!.ToUppercaseFirst()))
                .AfterMap((s, d) => d.SetModifiedDate());
        }
    }
}