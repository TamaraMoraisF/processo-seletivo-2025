using Seals.Duv.Application.Applications;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Application.Mappings;
using Seals.Duv.Application.Services;
using Seals.Duv.Domain.Interfaces;
using Seals.Duv.Infrastructure.Repositories;

namespace Seals.Duv.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            RegisterApplications(services);
            RegisterServices(services);
            RegisterRepositories(services);

            return services;
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<IPassageiroApplication, PassageiroApplication>();
            services.AddScoped<INavioApplication, NavioApplication>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPassageiroService, PassageiroService>();
            services.AddScoped<INavioService, NavioService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IPassageiroRepository, PassageiroRepository>();
            services.AddScoped<INavioRepository, NavioRepository>();
        }
    }
}
