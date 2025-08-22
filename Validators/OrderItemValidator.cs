using FluentValidation;
using OrderService.Models.DTO;
using OrderService.Models.Entities;

namespace OrderService.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemValidator()
        {
            RuleFor(i => i.ProductName)
                .NotEmpty().WithMessage("Product name is required");

            RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0");
        }
    }
}