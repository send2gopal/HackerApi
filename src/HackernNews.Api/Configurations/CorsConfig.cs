using Microsoft.Extensions.Options;

namespace HackernNews.Api.Configurations
{
    /// <summary>
    /// Configuration settings for CORS (Cross-Origin Resource Sharing).
    /// </summary>
    public class CorsConfig
    {
        public const string Name = "CorsConfig";
        public string[] AllowedOrigins { get; set; }
    }

    public static class CorsConfigExtensions
    {
        /// <summary>
        /// Adds the CORS policy to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddOptions<CorsConfig>().BindConfiguration(CorsConfig.Name);

            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<CorsConfig>>().Value);

            services.AddCors(options =>
            {
                options.AddPolicy(CorsConfig.Name, builder =>
                {
                    var config = services.BuildServiceProvider().GetRequiredService<CorsConfig>();
                    builder.WithOrigins(config.AllowedOrigins)
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            return services;
        }

        /// <summary>
        /// Uses the CORS policy in the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The updated application builder.</returns>
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            return app.UseCors(CorsConfig.Name);
        }
    }
}
