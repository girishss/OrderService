using OrderService.Models.DTO;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(Guid id);
        Task<OrderDTO> PlaceOrderAsync(OrderDTO orderDto);
        Task UpdateOrderAsync(Guid id, OrderDTO orderDto);
        Task DeleteOrderAsync(Guid id);
    }
}
