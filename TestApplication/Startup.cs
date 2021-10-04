using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Config;

namespace TestApplication
{
    public class Startup
    {
        private readonly IHostEnvironment _hostEnvironment;

        public Startup(IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _hostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddDbContext(Configuration, _hostEnvironment);
            services.AddControllers(c => { c.ModelBinderProviders.Insert(0, new ModelBinderProvider()); });
            services.AddMapper();
            services.AddCoresAllowAll();
            services.AddSwagger();
            services.AddRepositories();
            services.AddDataServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllOrigin");
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health",
                                          new HealthCheckOptions()
                                          {
                                              Predicate = _ => true,
                                              ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                                          });
                endpoints.MapControllers();
            });

            app.UseSwagger()
               .UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Test Application Api"); });
        }
    }
}
