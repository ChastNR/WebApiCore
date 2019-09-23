using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlRepository.Interfaces;
using SqlRepository.Repositories;
using UniversalWebApi.Extensions.EmailSender;
using UniversalWebApi.Helpers.EncrytionHelper;
using UniversalWebApi.Helpers.Serializer;
using UniversalWebApi.Schedulers;

namespace UniversalWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataRepository>(s => new DataRepository(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<ISerializeHelper, SerializeHelper>();
            services.AddTransient<IEmailScheduler, EmailScheduler>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
