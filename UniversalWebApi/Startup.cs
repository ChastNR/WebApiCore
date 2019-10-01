using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoRepository.Interfaces;
using SqlRepository.Interfaces;
using SqlRepository.Repositories;
using System;
using System.IO;
using UniversalWebApi.Extensions.EmailSender;
using UniversalWebApi.Helpers.EncryptionHelper;
using UniversalWebApi.Helpers.ExceptionManager;
using UniversalWebApi.Helpers.Filters;
using UniversalWebApi.Helpers.Serializer;

namespace UniversalWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApiAsyncActionFilter>();

            services.AddTransient<IDataRepository>(s =>
                new DataRepository(Configuration.GetConnectionString("DbConnection")));

            services.AddTransient<IMongoRepository>(s =>
                new MongoRepository.MongoRepository(
                    Configuration.GetSection("MongoDbSettings").GetSection("DbConnection").Value,
                    Configuration.GetSection("MongoDbSettings").GetSection("DbName").Value));

            services.AddTransient<ISerializeHelper, SerializeHelper>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();

            services.AddTransient<IExceptionManager, ExceptionManager>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(ApiAsyncActionFilter));
                options.InputFormatters.Insert(0, new BinaryInputFormatter());
                options.OutputFormatters.Insert(0, new BinaryOutputFormatter());
            });
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}