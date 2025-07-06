using Seals.Duv.Application.Applications;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Application.Mappings;
using Seals.Duv.Application.Services;
using Seals.Duv.Application.Validators;
using Seals.Duv.Domain.Entities;
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
            RegisterValidators(services);

            return services;
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<IDuvApplication, DuvApplication>();
            services.AddScoped<INavioApplication, NavioApplication>();
            services.AddScoped<IPassageiroApplication, PassageiroApplication>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IDuvService, DuvService>();
            services.AddScoped<INavioService, NavioService>();
            services.AddScoped<IPassageiroService, PassageiroService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IDuvRepository, DuvRepository>();
            services.AddScoped<INavioRepository, NavioRepository>();
            services.AddScoped<IPassageiroRepository, PassageiroRepository>();
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<Passageiro>, PassageiroValidator>();
            services.AddScoped<IValidator<Navio>, NavioValidator>();
            services.AddScoped<IValidator<Domain.Entities.Duv>, DuvValidator>();
        }
    }
}
