using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Services
{
    public class PassageiroService(IPassageiroRepository repository, IValidator<Passageiro> validator) : IPassageiroService
    {
        private readonly IPassageiroRepository _repository = repository;
        private readonly IValidator<Passageiro> _validator = validator;

        public Task<IEnumerable<Passageiro>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Passageiro?> GetByGuidAsync(Guid guid) => _repository.GetByGuidAsync(guid);

        public async Task<Passageiro> CreateAsync(Passageiro passageiro)
        {
            _validator.Validate(passageiro);
            await _repository.AddAsync(passageiro);
            return passageiro;
        }

        public async Task UpdateByGuidAsync(Guid guid, Passageiro passageiro)
        {
            var existing = await _repository.GetByGuidAsync(guid);
            if (existing is null) return;

            existing.Nome = passageiro.Nome;
            existing.Tipo = passageiro.Tipo;
            existing.Nacionalidade = passageiro.Nacionalidade;
            existing.FotoUrl = passageiro.FotoUrl;
            existing.Sid = passageiro.Sid;
            existing.DuvId = passageiro.DuvId;

            _validator.Validate(existing);

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteByGuidAsync(Guid guid)
        {
            var passageiro = await _repository.GetByGuidAsync(guid);
            if (passageiro is not null)
                await _repository.DeleteAsync(passageiro);
        }
    }
}
