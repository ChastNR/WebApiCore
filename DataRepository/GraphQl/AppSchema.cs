using GraphQL;
using GraphQL.Types;

namespace DataRepository.GraphQl
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            //Query = resolver.Resolve<AppQuery>();
        }
    }
    
}