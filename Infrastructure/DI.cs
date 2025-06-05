using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Serialization;

namespace Infrastructure;

public static class DI
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
    {
        services.AddSingleton<SerializationService>();
        return services;
    }
}
