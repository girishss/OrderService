using AutoMapper;
using MassTransit;
using OrderService.Models.DTO;
using OrderService.Models.Entities;
using OrderService.Repository;

namespace OrderService.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderServices(IOrderRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> PlaceOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.CalculateTotal();
            order.OrderId = Guid.NewGuid();
            order.MetaDateCreated = DateTime.UtcNow;
            order.MetaDateUpdated = DateTime.UtcNow;

            var createdOrder = await _repository.AddAsync(order);

            var createdOrderDto = _mapper.Map<OrderDTO>(createdOrder);

            await _publishEndpoint.Publish(new OrderPlacedEvent
            {
                OrderId = createdOrder.OrderId,
                CustomerName = createdOrder.CustomerName,
                TotalAmount = createdOrder.TotalAmount
            });

            return createdOrderDto;
        }

        public async Task UpdateOrderAsync(Guid id, OrderDTO orderDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Order not found");

            _mapper.Map(orderDto, existing);
            existing.MetaDateUpdated = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Order not found");

            await _repository.DeleteAsync(existing);
        }
    }
}
