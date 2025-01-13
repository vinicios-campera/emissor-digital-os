using FluentValidation;

namespace OrderService.Infrastructure.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorCode, string errorMessage)
            => rule.WithErrorCode(errorCode).WithMessage(errorMessage);
    }
}