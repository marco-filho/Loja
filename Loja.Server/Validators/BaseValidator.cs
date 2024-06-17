using FluentValidation;
using Loja.Domain.Messages.Error;

namespace Loja.Server.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public async Task ValidateAndThrowAsync(T schema)
        {
            var results = await ValidateAsync(schema);

            if (!results.IsValid || schema is null)
            {
                throw new ValidationException(ValidationErrorMessage.INVALID_SCHEMA, results.Errors);
            }
        }
    }
}
