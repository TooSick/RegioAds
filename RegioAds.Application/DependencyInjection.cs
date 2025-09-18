using Microsoft.Extensions.DependencyInjection;
using RegioAds.Application.Abstractions;
using RegioAds.Application.Services;

namespace RegioAds.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serices)
        {
            return serices.AddScoped<IAdPlatformService, AdPlatformService>();
        }
    }
}
