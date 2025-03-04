﻿using HackernNews.Core.Interfaces;
using HackernNews.Infrastructure.Configurations;
using HackernNews.Infrastructure.DownstreamServices;
using HackernNews.Infrastructure.HackerNewsSource;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace HackernNews.Infrastructure
{
    /// <summary>
    /// Provides extension methods for registering infrastructure services.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds infrastructure services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <param name="config">The ConfigurationManager to retrieve configuration settings.</param>
        /// <param name="logger">The ILogger to log information.</param>
        /// <returns>The IServiceCollection with the added services.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CacheSettings>(config.GetSection(CacheSettings.Name));
            services.Configure<HackerApiClientSettings>(config.GetSection(HackerApiClientSettings.Name));


            var hackerApiClientSettings = config
                .GetSection(HackerApiClientSettings.Name)
                .Get<HackerApiClientSettings>();
            
            services
                .AddMemoryCache()
                .AddScoped<IHackerNewsService, HackerNewsService>()
                .AddScoped<ICacheService, MemoryCacheService>()
                .AddRefitClient<IHackerNewSourceApiClient>()
                // TODO: Add the base address for the Hacker News API in app settings.
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(hackerApiClientSettings.BaseUrl));

            return services;
        }
    }
}
