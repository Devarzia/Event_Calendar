using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventCalendar.Extensions
{
    public static class AuthenticationServicesExtensions
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(o =>
                {
                    o.ClientId = configuration["Google:ClientID"];
                    o.ClientSecret = configuration["Google:ClientSecret"];
                });

            return services;
        }
    }
}
