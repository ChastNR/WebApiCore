using Microsoft.Extensions.Configuration;
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
        public static void AddToolsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionManager, ExceptionManager>();
            services.AddTransient<ISerializeHelper, SerializeHelper>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();
            services.AddTransient<IMessageSender, EmailSender>();
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
        }
    }
}