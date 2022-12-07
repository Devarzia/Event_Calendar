using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventCalendar;

public static class ApplicationCookieServicesExtensions
{
    public static IServiceCollection AddApplicationCookieServices(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.LogoutPath = "/Identity/Account/Logout";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.LoginPath = "/Identity/Account/Login";
            options.ReturnUrlParameter = "returnUrl";
            options.SlidingExpiration = false;
            options.Cookie.IsEssential = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return services;
    }
}