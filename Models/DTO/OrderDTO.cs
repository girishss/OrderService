namespace OrderService.Models.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime MetaDateCreated { get; set; }
        public DateTime MetaDateUpdated { get; set; }
    }
}
