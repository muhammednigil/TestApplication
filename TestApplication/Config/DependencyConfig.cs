using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestApplicationDB;
using TestApplicationDB.Repository;
using TestApplicationInterface;
using TestApplicationInterface.Repository;
using TestApplicationInterface.Service;
using TestApplicationService;

namespace TestApplication.Config
{
    public static class DependencyConfig
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<TestApplicationContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(90));
            });

            services.AddScoped<Func<TestApplicationContext>>((provider) => () => provider.GetService<TestApplicationContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }

        public static void AddCoresAllowAll(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAllOrigin",
                            options => options.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new OpenApiInfo
                             {
                                 Version = "1.0",
                                 Title = "Test Application",
                                 Description = "Test Application",
                             });

            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            var types = Assembly.Load(new AssemblyName("TestApplicationDB"))
                                .DefinedTypes
                                .ToList();
            var repos = new Dictionary<Type, Type>();
            foreach (var typeInfo in types)
            {
                var repoInterface = typeInfo.ImplementedInterfaces.FirstOrDefault(e => e.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)));
                if (repoInterface == null)
                    continue;

                repos.Add(repoInterface, typeInfo);
            }

            foreach (var repo in repos)
            {
                services.AddScoped(repo.Key, repo.Value);
            }
        }

        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IHotelService, HotelService>();
        }

    }
}
