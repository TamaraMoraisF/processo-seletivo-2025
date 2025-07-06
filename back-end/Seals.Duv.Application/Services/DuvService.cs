using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Services
{
    public class DuvService(IDuvRepository duvRepository) : IDuvService
    {
        private readonly IDuvRepository _duvRepository = duvRepository;

        public Task<IEnumerable<Domain.Entities.Duv>> GetAllAsync() => _duvRepository.GetAllAsync();

        public Task<Domain.Entities.Duv?> GetByGuidAsync(Guid guid) => _duvRepository.GetByGuidAsync(guid);

        public async Task<Domain.Entities.Duv> CreateAsync(Domain.Entities.Duv duv)
        {
            AdjustData(duv);
            return await _duvRepository.CreateAsync(duv);
        }

        public async Task UpdateByGuidAsync(Guid guid, Domain.Entities.Duv duv)
        {
            var existing = await _duvRepository.GetByGuidAsync(guid);
            if (existing is null) return;

            existing.Numero = duv.Numero;
            existing.DataViagem = duv.DataViagem;
            existing.NavioId = duv.NavioId;

            AdjustData(existing);
            await _duvRepository.UpdateAsync(existing);
        }

        public async Task DeleteByGuidAsync(Guid guid)
        {
            var duv = await _duvRepository.GetByGuidAsync(guid);
            if (duv is not null)
                await _duvRepository.DeleteAsync(duv);
        }

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
