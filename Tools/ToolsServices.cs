using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Tools.EncryptTool;
using Tools.Logger;
using Tools.Messages;
using Tools.Messages.EmailSender;
using Tools.Messages.SmsSender;
using Tools.Serializer;

namespace Tools
{
    public static class ToolsServices
    {
        public static IServiceCollection AddToolsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IExceptionManager, ExceptionManager>()
                .AddTransient<ISerializeHelper, SerializeHelper>()
                .AddTransient<IEncryptionHelper, EncryptionHelper>()
                .Configure<EmailOptions>(options => configuration.GetSection(nameof(EmailOptions)))
                .Configure<SmsOptions>(options => configuration.GetSection(nameof(SmsOptions)))
                .AddScoped<IMessageSender>(provider =>
                {
                    if (DateTime.Now.Hour >= 12)
                    {
                        return new EmailSender(provider.GetService<IOptions<EmailOptions>>());
                    }

                    return new SmsSender(provider.GetService<IOptions<SmsOptions>>());
                }); 

            return services;
        }
    }
}