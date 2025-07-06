using Seals.Duv.Application.DTOs;

namespace Seals.Duv.Application.Interfaces
{
    public interface IPassageiroApplication
    {
        Task<IEnumerable<PassageiroDto>> GetAllAsync();
        Task<PassageiroDto?> GetByGuidAsync(Guid guid);
        Task<PassageiroDto?> CreateAsync(CreatePassageiroDto dto);
        Task<bool> UpdateByGuidAsync(Guid guid, UpdatePassageiroDto dto);
        Task DeleteByGuidAsync(Guid guid);
    }
}
