using Microsoft.EntityFrameworkCore;

namespace OrderService.Models.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItem> Items { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = OrderStatus.Pending.ToString();

        public DateTime MetaDateCreated { get; set; }
        public DateTime MetaDateUpdated { get; set; }

        public void CalculateTotal()
        {
            TotalAmount = Items.Sum(x => x.Quantity * x.UnitPrice);
        }

    }

    public enum OrderStatus
    {
        Pending,
        Completed,
        Failed
    }

}
