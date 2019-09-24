using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlRepository.Interfaces;
using SqlRepository.Repositories;
using UniversalWebApi.Extensions.EmailSender;
using UniversalWebApi.Helpers.EncrytionHelper;
using UniversalWebApi.Helpers.ExceptionManager;
using UniversalWebApi.Helpers.Filters;
using UniversalWebApi.Helpers.Serializer;
using UniversalWebApi.Schedulers;

namespace UniversalWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApiAsyncActionFilter>();
            
            services.AddTransient<IDataRepository>(s => new DataRepository(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<ISerializeHelper, SerializeHelper>();
            services.AddTransient<IEmailScheduler, EmailScheduler>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();

            services.AddTransient<IExceptionManager, ExceptionManager>();
            
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddControllers();
            
//            services.AddControllers(options =>
//            {
//                options.Filters.Add(typeof(ApiAsyncActionFilter));
//            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
