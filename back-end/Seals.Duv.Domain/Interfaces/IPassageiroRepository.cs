using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface IPassageiroRepository
    {
        Task<IEnumerable<Passageiro>> GetAllAsync();
        Task<Passageiro?> GetByGuidAsync(Guid guid);
        Task AddAsync(Passageiro passageiro);
        Task UpdateAsync(Passageiro passageiro);
        Task DeleteAsync(Passageiro passageiro);
    }
}