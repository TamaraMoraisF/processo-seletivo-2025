using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Infrastructure.Persistence
{
    public class DuvDbContext(DbContextOptions<DuvDbContext> options) : DbContext(options)
    {
        public DbSet<Domain.Entities.Duv> Duvs { get; set; }
        public DbSet<Navio> Navios { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Duv>()
                .HasMany(d => d.Passageiros)
                .WithOne(p => p.Duv)
                .HasForeignKey(p => p.DuvId);

            modelBuilder.Entity<Domain.Entities.Duv>()
                .HasOne(d => d.Navio)
                .WithMany(n => n.Duvs)
                .HasForeignKey(d => d.NavioId);
        }
    }
}
