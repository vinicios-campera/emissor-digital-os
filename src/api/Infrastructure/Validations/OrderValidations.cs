using FluentValidation;
using OrderService.Infrastructure.Extensions;

namespace OrderService.Infrastructure.Validations
{
    public class OrderValidationInsert : AbstractValidator<Domain.Dto.Insert.OrderInsert>
    {
        public OrderValidationInsert()
        {
            RuleFor(x => x.Products)
                .NotNull().WithError("products_required", "Produtos não deve ser valor NULL")
                .NotEmpty().WithError("products_required", "Produtos não deve estar em branco");

            RuleForEach(x => x.Products).SetValidator(new OrderProductValidationInsert());
        }
    }
}