using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models.DTO;
using OrderService.Services;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PlaceOrder([FromBody]OrderDTO orderDto, [FromServices] IValidator<OrderDTO> validator)
        {
            var result = await validator.ValidateAsync(orderDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var createdOrder = await _orderService.PlaceOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDTO orderDto)
        {
            await _orderService.UpdateOrderAsync(id, orderDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
