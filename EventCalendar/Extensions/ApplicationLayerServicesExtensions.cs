using EventCalendar.Application;
using Microsoft.Extensions.DependencyInjection;

namespace EventCalendar;

public static class ApplicationLayerServicesExtensions
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IEventCategoryService, EventCategoryService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<ISocialEventService, SocialEventService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}