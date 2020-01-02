using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using UniversalWebApi.HealthCheckers;
using UniversalWebApi.BackgroundServices;
using UniversalWebApi.Filters;
using UniversalWebApi.Attributes;

using DataRepository;

using Tools;
using Tools.Messages.EmailSender;

using AuthenticationProcessor;
using AuthenticationProcessor.Settings;

namespace UniversalWebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection _)
        {
            _.AddDataAccessServices(Configuration)
                .AddToolsServices()
                .AddAuthProcessorServices()
                .AddHealthCheckServices()
                .AddHostedService<ApiHealthHostedService>()
                .AddHttpClient()
                .Configure<EmailSettings>(Configuration.GetSection("EmailSettings"))
                .Configure<AuthOptions>(Configuration.GetSection("AuthOptions"))
                .Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true)
                .AddCors(options => options
                    .AddPolicy("CorsPolicy", builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    ))
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            Configuration.GetSection("AuthOptions").Get<AuthOptions>().SecurityKey))
                    };
                });

            _.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
                options.Filters.Add<ApiBackgroundActionFilter>();
            });
            
            _.AddSpaStaticFiles(configuration => configuration.RootPath = "app/build");
        }

        public void Configure(IApplicationBuilder _, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _.UseDeveloperExceptionPage();
            }

            _.UseRouting()
                .UseAuthorization()
                .UseAuthentication()
                .AddDataConfigBuilder()
                .AddHealthCheckBuilder()
                .UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            })
                .UseSpa(spa =>
            {
                spa.Options.SourcePath = "app";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer("start");
                }
            });
        }
    }
}