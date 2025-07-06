using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DuvDbContext>
    {
        public DuvDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Seals.Duv.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DuvDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DuvDbContext(optionsBuilder.Options);
        }
    }
}
