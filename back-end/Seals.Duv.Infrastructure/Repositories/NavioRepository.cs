using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Infrastructure.Repositories
{
    public class NavioRepository(DuvDbContext context) : INavioRepository
    {
        private readonly DuvDbContext _context = context;

        public async Task<IEnumerable<Navio>> GetAllAsync() =>
            await _context.Navios.ToListAsync();

        public async Task<Navio?> GetByGuidAsync(Guid guid) =>
            await _context.Navios.FirstOrDefaultAsync(n => n.NavioGuid == guid);

        public async Task<Navio> CreateAsync(Navio navio)
        {
            _context.Navios.Add(navio);
            await _context.SaveChangesAsync();
            return navio;
        }

        public async Task UpdateAsync(Navio navio)
        {
            _context.Navios.Update(navio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Navio navio)
        {
            _context.Navios.Remove(navio);
            await _context.SaveChangesAsync();
        }
    }
}
