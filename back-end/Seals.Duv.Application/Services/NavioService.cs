using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Services
{
    public class NavioService : INavioService
    {
        private readonly INavioRepository _repository;

        public NavioService(INavioRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Navio>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Navio?> GetByGuidAsync(Guid guid) => _repository.GetByGuidAsync(guid);

        public Task<Navio> CreateAsync(Navio navio)
        {
            navio.NavioGuid = Guid.NewGuid();

            return _repository.CreateAsync(navio);
        }

        public async Task UpdateByGuidAsync(Guid guid, Navio navio)
        {
            var existing = await _repository.GetByGuidAsync(guid);
            if (existing is null) return;

            existing.Nome = navio.Nome;
            existing.Bandeira = navio.Bandeira;
            existing.ImagemUrl = navio.ImagemUrl;

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
