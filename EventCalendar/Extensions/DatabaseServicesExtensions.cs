using EventCalendar.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventCalendar;

public static class DatabaseServicesExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EventCalendarDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("EventCalendar")));

        return services;
    }
}