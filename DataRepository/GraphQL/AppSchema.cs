using GraphQL;
using GraphQL.Types;
using DataRepository.GraphQL.Query;

namespace DataRepository.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<UserQuery>();
        }
    }
}