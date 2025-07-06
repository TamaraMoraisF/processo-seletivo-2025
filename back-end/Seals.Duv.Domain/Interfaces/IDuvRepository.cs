namespace Seals.Duv.Domain.Interfaces
{
    public interface IDuvRepository
    {
        Task<IEnumerable<Entities.Duv>> GetAllAsync();
        Task<Entities.Duv?> GetByGuidAsync(Guid guid);
        Task<Entities.Duv> CreateAsync(Entities.Duv duv);
        Task UpdateAsync(Entities.Duv duv);
        Task DeleteAsync(Entities.Duv duv);
    }
}
