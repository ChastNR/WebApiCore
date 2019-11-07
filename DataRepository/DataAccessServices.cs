using DataRepository.GraphQL;
using DataRepository.Interfaces;
using DataRepository.Interfaces.Base;
using DataRepository.Repositories;
using DataRepository.Repositories.Base;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository
{
    public static class DataAccessServices
    {
        public static string ConnectionString { get; private set; }
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DbConnection");
            
            //MongoDbRepositories
            services.AddTransient<IMongoRepository>(s =>
                new MongoRepository(
                    configuration.GetSection("MongoDbSettings").GetSection("DbConnection").Value,
                    configuration.GetSection("MongoDbSettings").GetSection("DbName").Value));
            
            //SqlRepositories
            services.AddTransient<ISqlRepository, SqlRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            
            //GraphQL
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        public static void AddDataConfigBuilder(this IApplicationBuilder app)
        {
            //GraphQL
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}