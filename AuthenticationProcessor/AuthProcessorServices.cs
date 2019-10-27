using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationProcessor
{
    public static class AuthProcessorServices
    {
        public static void AddAuthProcessorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthProcessor, AuthProcessor>();
        }
    }
}