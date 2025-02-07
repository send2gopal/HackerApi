using HackernNews.UseCases.Properties;
using System.Reflection;

namespace Clean.Architecture.Web.Configurations
{

    /// <summary>
    /// Configuration class for setting up MediatR in the application.
    /// </summary>
    public static class MediatrConfig
    {
        /// <summary>
        /// Adds MediatR services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The IServiceCollection with MediatR services added.</returns>
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            var assemblies = new[]
            {
            Assembly.GetAssembly(typeof(UseCaseAnchor))
        };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies!));
            return services;
        }
    }
}
