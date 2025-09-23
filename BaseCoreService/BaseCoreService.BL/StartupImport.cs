using BaseCoreService.DL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCoreService.BL
{
    public static class BLStartupImport
    {
        public static IServiceCollection AddBusinessLayerService(this IServiceCollection services)
        {
            var logger = LogManager.Setup()
                .LoadConfigurationFromFile("NLog.config");
                //.GetCurrentClassLogger();
            
            services.AddSingleton<ILogBL, LogBL>();
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextBL, HttpContextBL>();
            services.AddScoped<ServiceCollection>();
            services.AddScoped<IBaseBL, BaseBL>();
            services.AddScoped<IUserDL, UserDL>();
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<IRoleDL, RoleDL>();
            services.AddScoped<IRoleBL, RoleBL>();
            services.AddScoped<ITestBL, TestBL>();

            return services;
        }

        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddBusinessLayerService();
        }

        public static void Init(WebApplicationBuilder builder)
        {
            Init(builder.Services, builder.Configuration);
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
        }
    }
}
