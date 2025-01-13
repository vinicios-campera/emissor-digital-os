using FluentValidation;
using OrderService.Infrastructure.Extensions;

namespace OrderService.Infrastructure.Validations
{
    public class ProductValidationInsert : AbstractValidator<Domain.Dto.Insert.ProductInsert>
    {
        public ProductValidationInsert()
        {
            RuleFor(x => x.UnitaryValue)
                .GreaterThan(0).WithError("unitaryValue_invalid", "Valor unitário deve ser maior que 0");

            RuleFor(x => x.Description)
                .NotNull().WithError("description_required", "Descrição não deve ser valor NULL")
                .NotEmpty().WithError("description_required", "Descrição não deve estar em branco")
                .Length(1, 100).WithError("description_invalid", $"Descrição deve conter entre {1} e {100} caracteres");
        }
    }

    public class ProductValidationUpdate : AbstractValidator<Domain.Dto.Update.ProductUpdate>
    {
        public ProductValidationUpdate()
        {
            RuleFor(x => x.UnitaryValue)
                .GreaterThan(0).WithError("unitaryValue_invalid", "Valor unitário deve ser maior que 0");

            RuleFor(x => x.Description)
                .NotNull().WithError("description_required", "Descrição não deve ser valor NULL")
                .NotEmpty().WithError("description_required", "Descrição não deve estar em branco")
                .Length(1, 100).WithError("description_invalid", $"Descrição deve conter entre {1} e {100} caracteres");
        }
    }

    public class OrderProductValidationInsert : AbstractValidator<Domain.Dto.Insert.OrderProductInsert>
    {
        public OrderProductValidationInsert()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithError("amount_invalid", "Quantidade deve ser maior que 0");

            RuleFor(x => x.UnitaryValue)
                .GreaterThan(0).WithError("unitaryValue_invalid", "Valor unitário deve ser maior que 0");

            RuleFor(x => x.Description)
                .NotNull().WithError("description_required", "Descrição não deve ser valor NULL")
                .NotEmpty().WithError("description_required", "Descrição não deve estar em branco")
                .Length(1, 100).WithError("description_invalid", $"Descrição deve conter entre {1} e {100} caracteres");
        }
    }
}