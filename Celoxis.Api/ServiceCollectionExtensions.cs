using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Celoxis.Api.Interfaces;

namespace Celoxis.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCeloxis(this IServiceCollection services, Action<CeloxisOptions> configureOptions)
        {
            services.Configure(configureOptions);
            
            services.AddSingleton<ICeloxisClient>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<CeloxisOptions>>().Value;
                return new CeloxisClient(options.AccessToken, options.BaseUrl, options.FlurlClient);
            });

            return services;
        }

        public static IServiceCollection AddCeloxis(this IServiceCollection services, CeloxisOptions options)
        {
            services.AddSingleton<ICeloxisClient>(_ => new CeloxisClient(options.AccessToken, options.BaseUrl, options.FlurlClient));
            return services;
        }
    }
}
