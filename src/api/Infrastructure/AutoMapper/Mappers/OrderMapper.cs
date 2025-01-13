using AutoMapper;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.AutoMapper.Converters;

namespace OrderService.Infrastructure.AutoMapper.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderResponse>()
                .ConvertUsing<OrderConverters.OrderToOrderResponse>();

            CreateMap<OrderInsert, Order>()
                .ConvertUsing<OrderConverters.OrderInsertToOrder>();

            CreateMap<Order, OrderDetailResponse>();
        }
    }
}