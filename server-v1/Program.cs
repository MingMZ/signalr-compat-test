using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server_V1.Hubs;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new WebHostBuilder()
            .UseStartup<Startup>()
            .UseKestrel()
            .UseUrls("http://localhost:5170")
            .Build();

        app.Run();
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials()
                              .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors();

            app.UseSignalR(routes => { routes.MapHub<ChatHub>("/chat"); });

            app.UseMvc();
        }
    }
}