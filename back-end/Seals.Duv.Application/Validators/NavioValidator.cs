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
                errors.Add("Nome do navio é obrigatório.");

            if (string.IsNullOrWhiteSpace(entity.Bandeira))
                errors.Add("Bandeira do navio é obrigatória.");

            if (string.IsNullOrWhiteSpace(entity.ImagemUrl))
                errors.Add("Imagem do navio é obrigatória.");

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}
