using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
