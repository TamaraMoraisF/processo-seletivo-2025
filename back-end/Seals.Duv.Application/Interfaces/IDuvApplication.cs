using Seals.Duv.Application.DTOs;

namespace Seals.Duv.Application.Interfaces
{
    public interface IDuvApplication
    {
        Task<IEnumerable<DuvDto>> GetAllAsync();
        Task<DuvDto?> GetByIdAsync(int id);
        Task<DuvDto> CreateAsync(DuvDto dto);
        Task UpdateAsync(int id, DuvDto dto);
        Task DeleteAsync(int id);
    }
}
