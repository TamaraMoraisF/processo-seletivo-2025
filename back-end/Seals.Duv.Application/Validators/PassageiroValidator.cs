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
                errors.Add("Nome do passageiro é obrigatório.");

            if (string.IsNullOrWhiteSpace(entity.Nacionalidade))
                errors.Add("Nacionalidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(entity.FotoUrl))
                errors.Add("Foto do passageiro é obrigatória.");

            if (entity.Tipo == Domain.Enum.TipoPassageiro.Tripulante && string.IsNullOrWhiteSpace(entity.Sid))
                errors.Add("Tripulantes devem ter um documento SID.");

            if (entity.DuvId <= 0)
                errors.Add("DuvId inválido.");

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}
