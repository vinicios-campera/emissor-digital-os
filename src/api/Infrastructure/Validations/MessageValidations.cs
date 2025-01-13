using FluentValidation;
using OrderService.Infrastructure.Extensions;

namespace OrderService.Infrastructure.Validations
{
    public class MessageValidationInsert : AbstractValidator<Domain.Dto.Insert.MessageInsert>
    {
        public MessageValidationInsert()
        {
            RuleFor(x => x.Email)
                .NotNull().WithError("email_required", "Email não deve ser valor NULL")
                .NotEmpty().WithError("email_required", "Email não deve estar em branco")
                .Length(1, 100).WithError("email_required", $"Email deve conter entre {1} e {100} caracteres");

            RuleFor(x => x.Name)
               .NotNull().WithError("name_required", "Name não deve ser valor NULL")
               .NotEmpty().WithError("name_required", "Name não deve estar em branco")
               .Length(1, 100).WithError("name_required", $"Name deve conter entre {1} e {100} caracteres");

            RuleFor(x => x.Description)
                .NotNull().WithError("description_required", "Descrição não deve ser valor NULL")
                .NotEmpty().WithError("description_required", "Descrição não deve estar em branco")
                .Length(1, 100).WithError("description_invalid", $"Descrição deve conter entre {1} e {100} caracteres");
        }
    }
}