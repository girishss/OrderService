using MassTransit;
using OrderService.Models.DTO;

namespace OrderService
{
    public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
    {
        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            Console.WriteLine($"✅ Order Received: {context.Message.OrderId} for Customer {context.Message.CustomerName}");
            await Task.CompletedTask;
        }
    }
}
