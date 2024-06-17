using AutoMapper;
using Loja.Server.Dtos;
using Loja.Domain.Entities;

namespace Loja.Server.AutoMapper
{
    public class EntityToDtoMapping : Profile
    {
        public EntityToDtoMapping()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Items, opt => opt.MapFrom(x => x.OrderItems));
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}