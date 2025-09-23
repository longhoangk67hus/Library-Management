using BaseCoreService.BL;
using BaseCoreService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BaseCoreService.Authen
{
    public static class AuthenStartupImport
    {
        public static IServiceCollection AddAuthenService(this IServiceCollection services)
        {
            services.AddScoped<IAuthBL, AuthBL>();
            return services;
        }

        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenService();
            services.AddCustomJwtAuthentication(configuration);
        }

        public static void Init(IHostApplicationBuilder builder)
        {
            Init(builder.Services, builder.Configuration);
        }
    }
}
