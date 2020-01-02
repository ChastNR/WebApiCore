using Microsoft.Extensions.DependencyInjection;
using Tools.EncryptTool;
using Tools.Logger;
using Tools.Messages;
using Tools.Messages.EmailSender;
using Tools.Serializer;

namespace Tools
{
    public static class ToolsServices
    {
        public static IServiceCollection AddToolsServices(this IServiceCollection services)
        {
            services
                .AddTransient<IExceptionManager, ExceptionManager>()
                .AddTransient<ISerializeHelper, SerializeHelper>()
                .AddTransient<IEncryptionHelper, EncryptionHelper>()
                .AddTransient<IMessageSender, EmailSender>();

            return services;
        }
    }
}