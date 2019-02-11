using Database.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            UpdateService.Initialize(new BotService(new BotConfiguration("689079563:AAEZFSFQ_juEfVTuyOAwevo4Fz2oJ-UrnXs", "", 25000)));
            Data.Initialize();
            NotificationService.Initialize();
        }

        public void Configure(IApplicationBuilder app) => app.UseMvc();
    }
}
