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
        public static void AddToolsServices(this IServiceCollection services)
        {
            services.AddTransient<IExceptionManager, ExceptionManager>();
            services.AddTransient<ISerializeHelper, SerializeHelper>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();
            services.AddTransient<IMessageSender, EmailSender>();
        }
    }
}