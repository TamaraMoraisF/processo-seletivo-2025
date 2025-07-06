using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface IPassageiroRepository
    {
        Task<IEnumerable<Passageiro>> GetAllAsync();
        Task<Passageiro?> GetByIdAsync(int id);
        Task AddAsync(Passageiro passageiro);
        Task UpdateAsync(Passageiro passageiro);
        Task DeleteAsync(Passageiro passageiro);
    }
}