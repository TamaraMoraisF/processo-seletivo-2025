using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Infrastructure.Repositories
{
    public class DuvRepository(DuvDbContext context) : IDuvRepository
    {
        private readonly DuvDbContext _context = context;

        public async Task<IEnumerable<Domain.Entities.Duv>> GetAllAsync() =>
            await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .ToListAsync();

        public async Task<Domain.Entities.Duv?> GetByGuidAsync(Guid guid) =>
            await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .FirstOrDefaultAsync(d => d.DuvGuid == guid);

        public async Task<Domain.Entities.Duv> CreateAsync(Domain.Entities.Duv duv)
        {
            _context.Duvs.Add(duv);
            await _context.SaveChangesAsync();
            return duv;
        }

        public async Task UpdateAsync(Domain.Entities.Duv duv)
        {
            _context.Duvs.Update(duv);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain.Entities.Duv duv)
        {
            _context.Duvs.Remove(duv);
            await _context.SaveChangesAsync();
        }
    }
}
