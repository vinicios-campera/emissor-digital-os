using FluentValidation;
using Kernel.Toolkit.Extensions;
using OrderService.Infrastructure.Extensions;
using System.Text.RegularExpressions;

namespace OrderService.Infrastructure.Validations
{
    public class UserValidationUpdate : AbstractValidator<Domain.Dto.Update.UserUpdate>
    {
        public UserValidationUpdate()
        {
            RuleFor(x => x.Name)
               .NotNull().WithError("name_required", "Nome não deve ser valor NULL")
               .NotEmpty().WithError("name_required", "Nome não deve estar em branco")
               .Length(1, 100).WithError("name_invalid", $"Nome deve conter entre {1} e {100} caracteres");

            RuleFor(x => x.NameFull)
               .NotNull().WithError("name_full_required", "Nome completo não deve ser valor NULL")
               .NotEmpty().WithError("name_full_required", "Nome completo não deve estar em branco")
               .Length(1, 100).WithError("name_full_required", $"Nome completo deve conter entre {1} e {100} caracteres");

            RuleFor(x => x.Address)
               .Length(1, 100).WithError("address_required", $"Endereço deve conter entre {1} e {100} caracteres")
               .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Document)
                .Must(x => x!.IsPhysicalDocument() || x!.IsLegalDocument()).WithError("document_invalid", "Documento deve estar no formato 999.999.999-99 ou 99.999.999/9999-99")
                .When(x => !string.IsNullOrEmpty(x.Document));

            RuleFor(x => x.City)
               .Length(1, 100).WithError("city_required", $"Cidade deve conter entre {1} e {100} caracteres")
               .When(x => !string.IsNullOrEmpty(x.City));

            RuleFor(x => x.Telephone)
                .Matches(new Regex(@"^\([1-9]{2}\) ?[0-9]{4,5}-[0-9]{4}$")).WithError("tele_phone_invalid", "Telefone deve estar no formato (99)99999-9999 ou (99)9999-9999")
                .When(x => !string.IsNullOrEmpty(x.Telephone));

            RuleFor(x => x.State)
               .Length(2).WithError("state_required", $"Estado deve conter {2} caracteres")
               .When(x => !string.IsNullOrEmpty(x.State));

            RuleFor(x => x.Cellphone)
                .Matches(new Regex(@"^\([1-9]{2}\) ?[0-9]{4,5}-[0-9]{4}$")).WithError("cell_phone_invalid", "Celular deve estar no formato (99)99999-9999 ou (99)9999-9999")
                .When(x => !string.IsNullOrEmpty(x.Cellphone));
        }
    }
}