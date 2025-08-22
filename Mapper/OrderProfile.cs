using AutoMapper;
using OrderService.Models.DTO;
using OrderService.Models.Entities;

namespace OrderService.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderItem, OrderItemDTO>();

            CreateMap<OrderDTO, Order>()
                .ForMember(x => x.OrderId, opt => opt.Ignore())
                .ForMember(x => x.MetaDateCreated, opt => opt.Ignore());
            CreateMap<OrderItemDTO, OrderItem>();

        }      

    }
}
