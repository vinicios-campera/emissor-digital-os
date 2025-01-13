using FluentValidation;
using OrderService.Infrastructure.Extensions;
using System.Text.RegularExpressions;
using Kernel.Toolkit.Extensions;

namespace OrderService.Infrastructure.Validations
{
    public class ClientValidationInsert : AbstractValidator<Domain.Dto.Insert.ClientInsert>
    {
        public ClientValidationInsert()
        {
            RuleFor(x => x.Name)
                .NotNull().WithError("name_required", "Nome não deve ser valor NULL")
                .NotEmpty().WithError("name_required", "Nome não deve estar em branco")
                .Length(1, 100).WithError("name_invalid", $"Nome deve conter entre {1} e {100} caracteres");

            RuleFor(x => x.Document)
                .Must(x => x!.IsPhysicalDocument() || x!.IsLegalDocument()).WithError("document_invalid", "Documento deve estar no formato 999.999.999-99 ou 99.999.999/9999-99")
                .When(x => !string.IsNullOrEmpty(x.Document));

            RuleFor(x => x.State)
               .Length(2).WithError("state_required", $"Estado deve conter {2} caracteres")
               .When(x => !string.IsNullOrEmpty(x.State));

            RuleFor(x => x.Cep)
                .Matches(new Regex(@"^\d{5}-\d{3}$")).WithError("cep_invalid", "CEP deve estar no formato {99999-999}")
                .When(x => !string.IsNullOrEmpty(x.Cep));

            RuleFor(x => x.Cellphone)
                .NotNull().WithError("cell_phone_required", "Celular não deve ser valor NULL")
                .NotEmpty().WithError("cell_phone_required", "Celular não deve estar em branco")
                .Matches(new Regex(@"^\([1-9]{2}\) ?[0-9]{4,5}-[0-9]{4}$")).WithError("cell_phone_invalid", "Celular deve estar no formato (99)99999-9999 ou (99)9999-9999");
        }
    }

    public class ClientValidationUpdate : AbstractValidator<Domain.Dto.Update.ClientUpdate>
    {
        public ClientValidationUpdate()
        {
            RuleFor(x => x.Name)
               .NotNull().WithError("name_required", "Nome não deve ser valor NULL")
               .NotEmpty().WithError("name_required", "Nome não deve estar em branco")
               .Length(1, 100).WithError("name_invalid", $"Nome deve conter entre {1} e {100} caracteres");

            RuleFor(x => x.Document)
                .Must(x => x!.IsPhysicalDocument() || x!.IsLegalDocument()).WithError("document_invalid", "Documento deve estar no formato 999.999.999-99 ou 99.999.999/9999-99")
                .When(x => !string.IsNullOrEmpty(x.Document));

            RuleFor(x => x.State)
               .Length(2).WithError("state_required", $"Estado deve conter {2} caracteres")
               .When(x => !string.IsNullOrEmpty(x.State));

            RuleFor(x => x.Cep)
                .Matches(new Regex(@"^\d{5}-\d{3}$")).WithError("cep_invalid", "CEP deve estar no formato {99999-999}")
                .When(x => !string.IsNullOrEmpty(x.Cep));

            RuleFor(x => x.Cellphone)
                .NotNull().WithError("cell_phone_required", "Celular não deve ser valor NULL")
                .NotEmpty().WithError("cell_phone_required", "Celular não deve estar em branco")
                .Matches(new Regex(@"^\([1-9]{2}\) ?[0-9]{4,5}-[0-9]{4}$")).WithError("cell_phone_invalid", "Celular deve estar no formato (99)99999-9999 ou (99)9999-9999");
        }
    }
}