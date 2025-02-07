using HackernNews.Infrastructure;

namespace Clean.Architecture.Web.Configurations
{

    /// <summary>
    /// Provides extension methods to configure application layers.
    /// </summary>
    public static class LayerConfig
    {
        /// <summary>
        /// Adds the necessary services for the application layers.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddLayers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            return services;
        }
    }
}
