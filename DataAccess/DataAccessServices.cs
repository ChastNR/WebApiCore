using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;

using DataAccess.GraphQL;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Base;
using DataAccess.Repositories;
using DataAccess.Repositories.Base;

namespace DataAccess
{
    public static class DataAccessServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IMongoRepository, MongoRepository>()
            .AddTransient<ISqlRepository, SqlRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
            .AddScoped<AppSchema>()
            .AddGraphQL(o => o.ExposeExceptions = true)
                .AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }

        public static IApplicationBuilder AddDataAccessBuilder(this IApplicationBuilder app)
        {
            app.UseGraphQL<AppSchema>()
               .UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            return app;
        }
    }
}