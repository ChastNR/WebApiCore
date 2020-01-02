using Microsoft.Extensions.DependencyInjection;
using AuthenticationProcessor.Interfaces;

namespace AuthenticationProcessor
{
    public static class AuthProcessorServices
    {
        public static IServiceCollection AddAuthProcessorServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthProcessor, AuthProcessor>();

            return services;
        }
    }
}