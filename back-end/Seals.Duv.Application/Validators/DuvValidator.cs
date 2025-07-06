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
                errors.Add("Número da DUV é obrigatório.");

            if (entity.DataViagem == default)
                errors.Add("Data da viagem é obrigatória.");

            if (entity.NavioId <= 0)
                errors.Add("NavioId inválido.");

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}
