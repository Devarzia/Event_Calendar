using EventCalendar.Application;
using EventCalendar.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventCalendar;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddAutoMapper(x => x.AddProfile<EventCalendarProfile>(), Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();
        services.AddSession();
        services.AddScoped<ModelValidationFilter>();

        return services;
    }
}