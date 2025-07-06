using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface INavioRepository
    {
        Task<IEnumerable<Navio>> GetAllAsync();
        Task<Navio?> GetByIdAsync(int id);
        Task<Navio> CreateAsync(Navio navio);
        Task UpdateAsync(int id, Navio navio);
        Task DeleteAsync(int id);
    }
}
