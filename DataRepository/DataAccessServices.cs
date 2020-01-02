using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;

using DataRepository.GraphQL;
using DataRepository.Interfaces;
using DataRepository.Interfaces.Base;
using DataRepository.Repositories;
using DataRepository.Repositories.Base;

namespace DataRepository
{
    public static class DataAccessServices
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //MongoDbRepositories
            services.AddTransient<IMongoRepository>(s =>
                new MongoRepository(
                    configuration.GetConnectionString("Mongo"),
                    configuration.GetConnectionString("MongoDbName")
            ))
            //SqlRepositories
            .AddTransient<ISqlRepository>(s => new SqlRepository(configuration.GetConnectionString("Sql")))
            .AddTransient<IUserRepository, UserRepository>()
            //GraphQL
            .AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
            .AddScoped<AppSchema>()
            .AddGraphQL(o => o.ExposeExceptions = true)
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        public static void AddDataConfigBuilder(this IApplicationBuilder app)
        {
            //GraphQL
            app.UseGraphQL<AppSchema>()
               .UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}