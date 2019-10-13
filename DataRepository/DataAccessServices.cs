using DataRepository.Interfaces;
using DataRepository.Interfaces.Base;
using DataRepository.Repositories;
using DataRepository.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository
{
    public static class DataAccessServices
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //EFRepositories
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            services.AddTransient<IEfRepository, EfRepository<ApplicationDbContext>>();

            //MongoDbRepositories
            services.AddTransient<IMongoRepository>(s =>
                new MongoRepository(
                    configuration.GetSection("MongoDbSettings").GetSection("DbConnection").Value,
                    configuration.GetSection("MongoDbSettings").GetSection("DbName").Value));

            //SqlRepositories
            services.AddTransient<ISqlRepository>(s =>
                new SqlRepository(configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IUserRepository>(s =>
                new UserRepository(configuration.GetConnectionString("DbConnection")));
        }
    }
}