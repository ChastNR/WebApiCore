using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tools;
using AuthenticationProcessor;
using DataRepository;
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
            
            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(ApiAsyncActionFilter));
                //options.InputFormatters.Insert(0, new BinaryInputFormatter());
                //options.OutputFormatters.Insert(0, new BinaryOutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}