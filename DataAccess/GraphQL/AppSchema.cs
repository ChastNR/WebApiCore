using GraphQL;
using GraphQL.Types;

using DataAccess.GraphQL.Query;

namespace DataAccess.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AppQuery>();
        }
    }
}