using HackernNews.UseCases.Properties;
using MediatR;
using System.Reflection;

namespace Clean.Architecture.Web.Configurations;

public static class MediatrConfigs
{
  public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
  {
    var mediatRAssemblies = new[]
      {
        Assembly.GetAssembly(typeof(UseCaseAnchor)), // Core
      };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
    return services;
  }
}
