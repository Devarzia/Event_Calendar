using EventCalendar.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EventCalendar;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
        {
            opt.SignIn.RequireConfirmedEmail = true;
            opt.SignIn.RequireConfirmedAccount = true;
            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.User.RequireUniqueEmail = true;

        }).AddRoles<ApplicationRole>().AddEntityFrameworkStores<EventCalendarDbContext>().AddDefaultUI().AddDefaultTokenProviders();

        return services;
    }
}