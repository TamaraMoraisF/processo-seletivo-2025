using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Services
{
    public class DuvService : IDuvService
    {
        private readonly IDuvRepository _repository;

        public DuvService(IDuvRepository repository) => _repository = repository;

        public Task<IEnumerable<Domain.Entities.Duv>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Domain.Entities.Duv?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public async Task<Domain.Entities.Duv> CreateAsync(Domain.Entities.Duv duv)
        {
            AdjustData(duv);
            return await _repository.CreateAsync(duv);
        }

        public async Task UpdateAsync(int id, Domain.Entities.Duv duv)
        {
            AdjustData(duv);
            await _repository.UpdateAsync(id, duv);
        }

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

        private static void AdjustData(Domain.Entities.Duv duv)
        {
            if (duv.DataViagem.Kind == DateTimeKind.Unspecified)
            {
                var localTime = DateTime.SpecifyKind(duv.DataViagem, DateTimeKind.Local);
                duv.DataViagem = localTime.ToUniversalTime();
            }
        }
    }
}
