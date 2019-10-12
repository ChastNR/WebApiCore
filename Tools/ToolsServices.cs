using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools.EncryptTool;
using Tools.EnumTool;
using Tools.Logger;
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
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
        }
    }
}