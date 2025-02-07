using HackernNews.Infrastructure;

namespace Clean.Architecture.Web.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddInfrastructureServices(configuration)
            .AddMediatrConfigs();


    return services;
  }


}
