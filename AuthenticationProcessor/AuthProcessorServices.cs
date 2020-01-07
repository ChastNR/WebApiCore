using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.Settings;

namespace AuthenticationProcessor
{
    public static class AuthProcessorServices
    {
        public static IServiceCollection AddAuthProcessorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(options => configuration.GetSection("AuthOptions"))
                .AddScoped<IAuthProcessor, AuthProcessor>();

            return services;
        }
    }
}