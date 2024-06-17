using AutoMapper;
using Loja.Server.Dtos;
using Loja.Domain.Entities;

namespace Loja.Server.AutoMapper
{
    public class DtoToEntityMapping : Profile
    {
        public DtoToEntityMapping()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(x => x.Items));
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}