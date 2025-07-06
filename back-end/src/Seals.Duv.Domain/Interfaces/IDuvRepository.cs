namespace Seals.Duv.Domain.Interfaces
{
    public interface IDuvRepository
    {
        Task<IEnumerable<Entities.Duv>> GetAllAsync();
        Task<Entities.Duv?> GetByIdAsync(int id);
        Task<Entities.Duv> CreateAsync(Entities.Duv duv);
        Task UpdateAsync(int id, Entities.Duv duv);
        Task DeleteAsync(int id);
    }
}
