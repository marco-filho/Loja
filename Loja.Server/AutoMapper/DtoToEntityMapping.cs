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
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(x => x.Items))
                .AfterMap((src, dest) =>
                    dest.OrderItems.Select(x =>
                    {
                        x.OrderId = src.Id;
                        return x;
                    }));
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}