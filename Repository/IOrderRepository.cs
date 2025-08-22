using OrderService.Models.Entities;

namespace OrderService.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
