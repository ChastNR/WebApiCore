using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tools;
using AuthenticationProcessor;
using DataRepository;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using UniversalWebApi.Filters;

namespace UniversalWebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApiAsyncActionFilter>();
            services.AddScoped<ApiExceptionFilter>();

            services.AddDataAccessServices(Configuration);
            services.AddToolsServices(Configuration);
            services.AddAuthProcessorServices(Configuration);

            #region CookieAuth
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options =>
            //     {
            //         options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            //     });
            #endregion

            #region JwtAuth
            // services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
            // var authConfig = Configuration.GetSection("AuthOptions").Get<AuthOptions>();
            // var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SecurityKey));
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.RequireHttpsMetadata = false;
            //         options.SaveToken = true;
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             //what to validate
            //             ValidateIssuer = true,
            //             ValidateAudience = true,
            //             ValidateIssuerSigningKey = true,
            //             //setup validate data
            //             ValidIssuer = authConfig.Issuer,
            //             ValidAudience = authConfig.Audience,
            //             IssuerSigningKey = symmetricSecurityKey,
            //             ClockSkew = TimeSpan.Zero
            //         };
            //     });
            #endregion

            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(ApiAsyncActionFilter));
                //options.InputFormatters.Insert(0, new BinaryInputFormatter());
                //options.OutputFormatters.Insert(0, new BinaryOutputFormatter());
            });

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "app/build"; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseCookiePolicy();
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSpa(spa =>
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