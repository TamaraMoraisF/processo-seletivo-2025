using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Exceptions;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Validators
{
    public class NavioValidator : IValidator<Navio>
    {
        public void Validate(Navio entity)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Nome))
                errors.Add(ValidationMessages.NavioNomeObrigatorio);

            if (string.IsNullOrWhiteSpace(entity.Bandeira))
                errors.Add(ValidationMessages.NavioBandeiraObrigatoria);

            if (string.IsNullOrWhiteSpace(entity.ImagemUrl))
                errors.Add(ValidationMessages.NavioImagemObrigatoria);

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}
