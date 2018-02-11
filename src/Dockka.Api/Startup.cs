using System;
using System.IO;
using System.Threading;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dockka.Api.Actors;
using Dockka.Api.Hubs;
using Dockka.Data.Context;
using Dockka.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Dockka.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json"); // Load configuration settings

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup SQL connection from Docker environment variables, or use appsettings as a fallback
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONN") ?? Configuration.GetConnectionString("DockkaSqlConnection");

            // Generate swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Dockka API", Version = "v1" });
            });

            // Configure Entity Framework
            services.AddDbContext<DockkaContext>(options => options.UseSqlServer(connectionString).WaitForActiveServer(TimeSpan.FromSeconds(300)));
            services.AddMvc();
            services.AddRouting();

            services.AddCors(o =>
                o.AddPolicy("Everything", p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                }));

            // Add SignalR for websockets
            services.AddSignalR();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<UpdateUiActor>();
            // Register AutoFac services here
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DockkaContext context)
        {
            // Auto-migrate (for docker especially)
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Map Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
            app.UseMvc();

            app.UseCors("Everything");

            app.UseSignalR(routes => routes.MapHub<PersonsHub>("persons"));
        }
    }
}
