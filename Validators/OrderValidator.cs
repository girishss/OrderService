using FluentValidation;
using OrderService.Models.DTO;
using OrderService.Models.Entities;

namespace OrderService.Validators
{
    public class OrderValidator : AbstractValidator<OrderDTO>
    {
        public OrderValidator()
        {
            RuleFor(o => o.CustomerName)
                .NotEmpty().WithMessage("Customer name is required")
                .MaximumLength(100);

            RuleFor(o => o.CustomerEmail)
                .NotEmpty().EmailAddress();

            RuleFor(o => o.Items)
                .NotEmpty().WithMessage("Order must contain at least one item");

            RuleForEach(o => o.Items).SetValidator(new OrderItemValidator());
        }
    }
}
