using EventCalendar.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace EventCalendar;

public static class DomainLayerServicesExtenstions
{
    public static IServiceCollection AddDomainLayerServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }
}