using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface IPassageiroService
    {
        Task<IEnumerable<Passageiro>> GetAllAsync();
        Task<Passageiro?> GetByIdAsync(int id);
        Task<Passageiro> CreateAsync(Passageiro passageiro);
        Task UpdateAsync(int id, Passageiro passageiro);
        Task DeleteAsync(int id);
    }
}