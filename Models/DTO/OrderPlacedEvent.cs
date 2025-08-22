namespace OrderService.Models.DTO
{
    public class OrderPlacedEvent
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}
