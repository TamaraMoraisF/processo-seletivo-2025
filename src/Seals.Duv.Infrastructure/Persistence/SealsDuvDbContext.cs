using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Infrastructure.Persistence
{
    public class SealsDuvDbContext : DbContext
    {
        public SealsDuvDbContext(DbContextOptions<SealsDuvDbContext> options)
            : base(options)
        {
        }

        public DbSet<Seals.Duv.Domain.Entities.Duv> Duvs { get; set; }
        public DbSet<Navio> Navios { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}