using EventCalendar.Application;
using EventCalendar.Domain;
using EventCalendar.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace EventCalendar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddAutoMapper(x => x.AddProfile<EventCalendarProfile>(), Assembly.GetExecutingAssembly());
            services.AddScoped<IEventCategoryService, EventCategoryService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ISocialEventService, SocialEventService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ModelValidationFilter>();

            services.AddHttpContextAccessor();
            services.AddSession();

            //services.AddDbContext<EventCalendarDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("EventCalendarProduction")));
            services.AddDbContext<EventCalendarDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("EventCalendar")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedAccount = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.User.RequireUniqueEmail = true;

            }).AddRoles<ApplicationRole>().AddEntityFrameworkStores<EventCalendarDbContext>().AddDefaultUI().AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(o =>
                {
                    o.ClientId = Configuration["Google:ClientID"];
                    o.ClientSecret = Configuration["Google:ClientSecret"];
                });

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Views/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
