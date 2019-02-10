using Database.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication;
using WebApplication.Services;
using WebCoreApplication.Models;
using WebCoreApplication.Services;

namespace WebCoreApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            RequestService.Initialize();
            KeyboardService.Initialize();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
            Data.Initialize();
            NotificationService.Initialize();
            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
        }

        public void Configure(IApplicationBuilder app) => app.UseMvc();
    }
}
