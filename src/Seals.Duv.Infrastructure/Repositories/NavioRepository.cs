using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Infrastructure.Repositories
{
    public class NavioRepository : INavioRepository
    {
        private readonly DuvDbContext _context;

        public NavioRepository(DuvDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Navio>> GetAllAsync() =>
            await _context.Navios.ToListAsync();

        public async Task<Navio?> GetByIdAsync(int id) =>
            await _context.Navios.FindAsync(id);

        public async Task<Navio> CreateAsync(Navio navio)
        {
            _context.Navios.Add(navio);
            await _context.SaveChangesAsync();
            return navio;
        }

        public async Task UpdateAsync(int id, Navio navio)
        {
            navio.Id = id;
            _context.Navios.Update(navio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var navio = await _context.Navios.FindAsync(id);
            if (navio is not null)
            {
                _context.Navios.Remove(navio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
