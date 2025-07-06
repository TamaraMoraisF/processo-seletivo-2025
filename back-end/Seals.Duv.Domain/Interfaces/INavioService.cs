using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface INavioService
    {
        Task<IEnumerable<Navio>> GetAllAsync();
        Task<Navio?> GetByGuidAsync(Guid guid);
        Task<Navio> CreateAsync(Navio navio);
        Task UpdateByGuidAsync(Guid guid, Navio navio);
        Task DeleteByGuidAsync(Guid guid);
    }
}
