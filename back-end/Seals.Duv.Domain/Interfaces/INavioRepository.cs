using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface INavioRepository
    {
        Task<IEnumerable<Navio>> GetAllAsync();
        Task<Navio?> GetByGuidAsync(Guid guid);
        Task<Navio> CreateAsync(Navio navio);
        Task UpdateAsync(Navio navio);
        Task DeleteAsync(Navio navio);
    }
}
