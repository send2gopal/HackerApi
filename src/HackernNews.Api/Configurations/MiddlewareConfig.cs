using HackernNews.Api.Middleware;

namespace Clean.Architecture.Web.Configurations
{
    /// <summary>
    /// Configures middleware for the application.
    /// </summary>
    public static class MiddlewareConfig
    {
        /// <summary>
        /// Adds the app middleware to the application's request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseAppMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
