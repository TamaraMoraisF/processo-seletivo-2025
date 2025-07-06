﻿using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Infrastructure.Repositories
{
    public class PassageiroRepository(DuvDbContext context) : IPassageiroRepository
    {
        private readonly DuvDbContext _context = context;

        public async Task<IEnumerable<Passageiro>> GetAllAsync() =>
            await _context.Passageiros
                .Include(p => p.Duv)
                .ToListAsync();

        public async Task<Passageiro?> GetByGuidAsync(Guid guid) =>
            await _context.Passageiros
                .Include(p => p.Duv)
                .FirstOrDefaultAsync(p => p.PassageiroGuid == guid);

        public async Task AddAsync(Passageiro passageiro)
        {
            _context.Passageiros.Add(passageiro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Passageiro passageiro)
        {
            _context.Passageiros.Update(passageiro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Passageiro passageiro)
        {
            _context.Passageiros.Remove(passageiro);
            await _context.SaveChangesAsync();
        }
    }
}
