using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Exceptions;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Validators
{
    public class PassageiroValidator : IValidator<Passageiro>
    {
        public void Validate(Passageiro entity)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Nome))
                errors.Add(ValidationMessages.PassageiroNomeObrigatorio);

            if (string.IsNullOrWhiteSpace(entity.Nacionalidade))
                errors.Add(ValidationMessages.PassageiroNacionalidadeObrigatoria);

            if (string.IsNullOrWhiteSpace(entity.FotoUrl))
                errors.Add(ValidationMessages.PassageiroFotoObrigatoria);

            if (entity.Tipo == Domain.Enum.TipoPassageiro.Tripulante && string.IsNullOrWhiteSpace(entity.Sid))
                errors.Add(ValidationMessages.PassageiroSidObrigatorioParaTripulante);

            if (entity.DuvId <= 0)
                errors.Add(ValidationMessages.PassageiroDuvIdInvalido);

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}
