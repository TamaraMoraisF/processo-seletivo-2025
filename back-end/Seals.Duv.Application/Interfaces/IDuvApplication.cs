using Seals.Duv.Application.DTOs;

namespace Seals.Duv.Application.Interfaces
{
    public interface IDuvApplication
    {
        Task<IEnumerable<DuvDto>> GetAllAsync();
        Task<DuvDto?> GetByGuidAsync(Guid guid);
        Task<DuvDto?> CreateAsync(CreateDuvDto dto);
        Task<bool> UpdateByGuidAsync(Guid guid, UpdateDuvDto dto);
        Task DeleteByGuidAsync(Guid guid);
    }
}
