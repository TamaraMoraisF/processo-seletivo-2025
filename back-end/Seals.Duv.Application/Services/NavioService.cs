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

        public Task<Navio?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<Navio> CreateAsync(Navio navio) => _repository.CreateAsync(navio);

        public Task UpdateAsync(int id, Navio navio) => _repository.UpdateAsync(id, navio);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
