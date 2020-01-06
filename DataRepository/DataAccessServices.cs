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
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMongoRepository>(s => new MongoRepository(
                    configuration.GetConnectionString("Mongo"),
                    configuration.GetConnectionString("MongoDbName")
                ))
            .AddTransient<ISqlRepository, SqlRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
            .AddScoped<AppSchema>()
            .AddGraphQL(o => o.ExposeExceptions = true)
                .AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }

        public static IApplicationBuilder AddDataConfigBuilder(this IApplicationBuilder app)
        {
            app.UseGraphQL<AppSchema>()
               .UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            return app;
        }
    }
}