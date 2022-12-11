using EventCalendar.Application;
using EventCalendar.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.AzureAppServices;
using System.Reflection;

namespace EventCalendar;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.Configure<AzureFileLoggerOptions>(options => { options.FileName = "EventCalendar-"; });
        services.AddAutoMapper(x => x.AddProfile<EventCalendarProfile>(), Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();
        services.AddSession();
        services.AddScoped<ModelValidationFilter>();

        return services;
    }
}