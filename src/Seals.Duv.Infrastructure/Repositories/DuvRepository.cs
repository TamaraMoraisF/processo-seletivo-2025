using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Infrastructure.Repositories
{
    public class DuvRepository : IDuvRepository
    {
        private readonly DuvDbContext _context;

        public DuvRepository(DuvDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Duv>> GetAllAsync() =>
            await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .ToListAsync();

        public async Task<Domain.Entities.Duv?> GetByIdAsync(int id) =>
            await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Domain.Entities.Duv> CreateAsync(Domain.Entities.Duv duv)
        {
            _context.Duvs.Add(duv);
            await _context.SaveChangesAsync();
            return duv;
        }

        public async Task UpdateAsync(int id, Domain.Entities.Duv duv)
        {
            duv.Id = id;
            _context.Duvs.Update(duv);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var duv = await _context.Duvs.FindAsync(id);
            if (duv is not null)
            {
                _context.Duvs.Remove(duv);
                await _context.SaveChangesAsync();
            }
        }
    }
}
