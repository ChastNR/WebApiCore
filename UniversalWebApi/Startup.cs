using DataRepository.Interfaces.Base;
using DataRepository.Repositories;
using DataRepository.Repositories.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniversalWebApi.Extensions.EmailSender;
using UniversalWebApi.Helpers.EncryptionHelper;
using UniversalWebApi.Helpers.ExceptionManager;
using UniversalWebApi.Helpers.Filters;
using UniversalWebApi.Helpers.Serializer;

namespace UniversalWebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApiAsyncActionFilter>();
            //services.AddScoped<ApiExceptionFilter>();


            services.AddTransient<ISqlRepository>(s =>
                new SqlRepository(Configuration.GetConnectionString("DbConnection")));


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });
            services.AddScoped<IEfRepository, EfRepository<ApplicationDbContext>>();

            services.AddTransient<IMongoRepository>(s =>
                new MongoRepository(
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