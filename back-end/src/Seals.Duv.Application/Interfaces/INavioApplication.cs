using Seals.Duv.Application.DTOs;

namespace Seals.Duv.Application.Interfaces
{
    public interface INavioApplication
    {
        Task<IEnumerable<NavioDto>> GetAllAsync();
        Task<NavioDto?> GetByIdAsync(int id);
        Task<NavioDto> CreateAsync(NavioDto dto);
        Task UpdateAsync(int id, NavioDto dto);
        Task DeleteAsync(int id);
    }
}
