using Seals.Duv.Application.DTOs;

namespace Seals.Duv.Application.Interfaces
{
    public interface IPassageiroApplication
    {
        Task<IEnumerable<PassageiroDto>> GetAllAsync();
        Task<PassageiroDto?> GetByIdAsync(int id);
        Task<PassageiroDto> CreateAsync(PassageiroDto passageiro);
        Task UpdateAsync(int id, PassageiroDto passageiro);
        Task DeleteAsync(int id);
    }
}
