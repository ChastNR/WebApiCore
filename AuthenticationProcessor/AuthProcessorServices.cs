using AuthenticationProcessor.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationProcessor
{
    public static class AuthProcessorServices
    {
        public static void AddAuthProcessorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthProcessor, AuthProcessor>();
        }
    }
}