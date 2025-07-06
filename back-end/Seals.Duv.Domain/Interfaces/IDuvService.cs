namespace Seals.Duv.Domain.Interfaces
{
    public interface IDuvService
    {
        Task<IEnumerable<Entities.Duv>> GetAllAsync();
        Task<Entities.Duv?> GetByGuidAsync(Guid guid);
        Task<Entities.Duv> CreateAsync(Entities.Duv duv);
        Task UpdateByGuidAsync(Guid guid, Entities.Duv duv);
        Task DeleteByGuidAsync(Guid guid);
    }
}
