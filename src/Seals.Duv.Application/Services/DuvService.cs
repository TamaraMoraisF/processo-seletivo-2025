using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Services
{
    public class DuvService : IDuvService
    {
        private readonly IDuvRepository _repository;

        public DuvService(IDuvRepository repository) => _repository = repository;

        public Task<IEnumerable<Domain.Entities.Duv>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Domain.Entities.Duv?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<Domain.Entities.Duv> CreateAsync(Domain.Entities.Duv duv) => _repository.CreateAsync(duv);

        public Task UpdateAsync(int id, Domain.Entities.Duv duv) => _repository.UpdateAsync(id, duv);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
