using Microsoft.Extensions.DependencyInjection;
using RegioAds.Application.Abstractions;
using RegioAds.Infrastructure.Repos;

namespace RegioAds.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAdPlatformRepository, FileAdPlatformRepository>();
            services.AddMemoryCache();
            return services;
        }
    }
}
