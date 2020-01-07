using System;

using GraphQL.Types;

using DataAccess.Contracts;
using DataAccess.GraphQL.Types;
using DataAccess.Interfaces.Base;

namespace DataAccess.GraphQL.Query
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(ISqlRepository repository)
        {
            Field<ListGraphType<UserType>>(
                "users",
                arguments: UserQueryArguments(),
                resolve: _ =>
                {
                    var id = _.GetArgument<Guid>("id");
                    if (id != Guid.Empty)
                        return repository.GetAllByConditionAsync<User>($"Id = '{id}'");

                    var name = _.GetArgument<string>("name");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAllByConditionAsync<User>($"Name like '%{name}%'");

                    var email = _.GetArgument<string>("email");
                    if (!string.IsNullOrEmpty(email))
                        return repository.GetAllByConditionAsync<User>($"Email like '%{email}%'");

                    var phoneNumber = _.GetArgument<string>("phoneNumber");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAllByConditionAsync<User>($"PhoneNumber like '%{phoneNumber}%'");

                    return repository.GetAllAsync<User>();
                }
            );

            Field<UserType>(
                "user",
                arguments: UserQueryArguments(),
                resolve: _ =>
                {
                    var id = _.GetArgument<Guid>("id");
                    if (id != Guid.Empty)
                        return repository.GetAsync<User>($"Id = '{id}'");

                    var name = _.GetArgument<string>("name");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetByConditionAsync<User>($"Name like '%{name}%'");

                    var email = _.GetArgument<string>("email");
                    if (!string.IsNullOrEmpty(email))
                        return repository.GetByConditionAsync<User>($"Email like '%{email}%'");

                    var phoneNumber = _.GetArgument<string>("phoneNumber");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetByConditionAsync<User>($"PhoneNumber like '%{phoneNumber}%'");

                    return null;
                });
        }
        
        private QueryArguments UserQueryArguments()
        {
            return new QueryArguments(
                new QueryArgument<IdGraphType> {Name = "id"},
                new QueryArgument<StringGraphType> {Name = "name"},
                new QueryArgument<StringGraphType> {Name = "email"},
                new QueryArgument<StringGraphType> {Name = "phoneNumber"},
                new QueryArgument<IntGraphType> {Name = "age"},
                new QueryArgument<StringGraphType> {Name = "passwordHash"},
                new QueryArgument<DateTimeGraphType> {Name = "creationDate"}
            );
        }
    }
}