using Seals.Duv.Application.DTOs;

namespace Seals.Duv.Application.Interfaces
{
    public interface INavioApplication
    {
        Task<IEnumerable<NavioDto>> GetAllAsync();
        Task<NavioDto?> GetByGuidAsync(Guid guid);
        Task<NavioDto> CreateAsync(CreateNavioDto dto);
        Task UpdateByGuidAsync(Guid guid, UpdateNavioDto dto);
        Task DeleteByGuidAsync(Guid guid);
    }
}
