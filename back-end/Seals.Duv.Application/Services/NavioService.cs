using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Application.Validators;

namespace Seals.Duv.Application.Services
{
    public class NavioService(INavioRepository repository, IValidator<Navio> validator) : INavioService
    {
        private readonly INavioRepository _repository = repository;
        private readonly IValidator<Navio> _validator = validator;

        public Task<IEnumerable<Navio>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Navio?> GetByGuidAsync(Guid guid) => _repository.GetByGuidAsync(guid);

        public async Task<Navio> CreateAsync(Navio navio)
        {
            _validator.Validate(navio);
            return await _repository.CreateAsync(navio);
        }

        public async Task UpdateByGuidAsync(Guid guid, Navio navio)
        {
            var existing = await _repository.GetByGuidAsync(guid);
            if (existing is null) return;

            existing.Nome = navio.Nome;
            existing.Bandeira = navio.Bandeira;
            existing.ImagemUrl = navio.ImagemUrl;

            _validator.Validate(existing);

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteByGuidAsync(Guid guid)
        {
            var navio = await _repository.GetByGuidAsync(guid);
            if (navio is not null)
                await _repository.DeleteAsync(navio);
        }
    }
}
