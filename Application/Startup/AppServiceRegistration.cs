﻿using MyApp.Application.Database;
using MyApp.Application.Events.Listeners;
using MyApp.Application.Models;
using MyApp.Application.Services.Auth;
using MyApp.Application.Services;
using BlazorSpark.Library.Database;
using BlazorSpark.Library.Logging;
using Coravel;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorSpark.Library.Auth;
using MyApp.Application.Tasks;

namespace MyApp.Application.Startup
{
    public static class AppServiceRegistration
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCustomServices();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDatabase<ApplicationDbContext>(config);
            services.AddLogger(config);
            services.AddAuthorization(config, new string[] { CustomRoles.Admin, CustomRoles.User });
            services.AddAuthentication<ICookieService>(config);
            services.AddTaskServices();
            services.AddScheduler();
            services.AddQueue();
            services.AddEventServices();
            services.AddEvents();
            return services;
        }

        private static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // add custom services
            services.AddScoped<UsersService>();
            services.AddScoped<RolesService>();
            services.AddScoped<IExampleService, ExampleService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<AuthenticationStateProvider, SparkAuthenticationStateProvider>();
            return services;
        }

        private static IServiceCollection AddEventServices(this IServiceCollection services)
        {
            // add custom events here
            services.AddTransient<EmailNewUser>();
            return services;
        }

        private static IServiceCollection AddTaskServices(this IServiceCollection services)
        {
            // add custom background tasks here
            services.AddTransient<ExampleTask>();
            return services;
        }
    }
}
