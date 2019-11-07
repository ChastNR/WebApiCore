using GraphQL.Types;
using DataRepository.Contracts;

namespace DataRepository.GraphQL.Types
{
    public class UserType: ObjectGraphType<User>
    {
        public UserType()
        {
            Field(t => t.Id, type: typeof(IdGraphType));
            Field(t => t.Name);
            Field(t => t.Email);
            Field(t => t.PhoneNumber);
            Field(t => t.Age);
            Field(t => t.PasswordHash);
            Field(t => t.CreationDate);
        }
    }
}