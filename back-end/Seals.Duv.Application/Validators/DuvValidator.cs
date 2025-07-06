using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Exceptions;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Validators
{
    public class DuvValidator : IValidator<Domain.Entities.Duv>
    {
        public void Validate(Domain.Entities.Duv entity)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Numero))
                errors.Add(ValidationMessages.DuvNumeroObrigatorio);

            if (entity.DataViagem == default)
                errors.Add(ValidationMessages.DuvDataObrigatoria);

            if (entity.NavioId <= 0)
                errors.Add(ValidationMessages.DuvNavioIdInvalido);

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}
