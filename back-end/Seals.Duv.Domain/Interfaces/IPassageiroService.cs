using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Domain.Interfaces
{
    public interface IPassageiroService
    {
        Task<IEnumerable<Passageiro>> GetAllAsync();
        Task<Passageiro?> GetByGuidAsync(Guid guid);
        Task<Passageiro> CreateAsync(Passageiro passageiro);
        Task UpdateByGuidAsync(Guid guid, Passageiro passageiro);
        Task DeleteByGuidAsync(Guid guid);
    }
}