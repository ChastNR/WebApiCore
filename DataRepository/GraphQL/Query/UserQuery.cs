using System;
using GraphQL.Types;
using DataRepository.Contracts;
using DataRepository.GraphQL.Types;
using DataRepository.Interfaces;

namespace DataRepository.GraphQL.Query
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserRepository repository)
        {
            Field<ListGraphType<UserType>>(
                "users",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "id"},
                    new QueryArgument<StringGraphType> {Name = "name"},
                    new QueryArgument<StringGraphType> {Name = "email"},
                    new QueryArgument<StringGraphType> {Name = "phoneNumber"},
                    new QueryArgument<IntGraphType> {Name = "age"},
                    new QueryArgument<StringGraphType> {Name = "passwordHash"},
                    new QueryArgument<DateTimeGraphType> {Name = "creationDate"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    if (id != Guid.Empty)
                        return repository.GetAllAsyncByCondition<User>($"Id = '{id}'");

                    var name = context.GetArgument<string>("name");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAllAsyncByCondition<User>($"Name like '%{name}%'");

                    var email = context.GetArgument<string>("email");
                    if (!string.IsNullOrEmpty(email))
                        return repository.GetAllAsyncByCondition<User>($"Email like '%{email}%'");

                    var phoneNumber = context.GetArgument<string>("phoneNumber");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAllAsyncByCondition<User>($"PhoneNumber like '%{phoneNumber}%'");

                    return repository.GetAllAsync<User>();
                }
            );

            Field<UserType>(
                "user",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "id"},
                    new QueryArgument<StringGraphType> {Name = "name"},
                    new QueryArgument<StringGraphType> {Name = "email"},
                    new QueryArgument<StringGraphType> {Name = "phoneNumber"},
                    new QueryArgument<IntGraphType> {Name = "age"},
                    new QueryArgument<StringGraphType> {Name = "passwordHash"},
                    new QueryArgument<DateTimeGraphType> {Name = "creationDate"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    if (id != Guid.Empty)
                        return repository.GetAsync<User>($"Id = '{id}'");

                    var name = context.GetArgument<string>("name");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAsyncByCondition<User>($"Name like '%{name}%'");

                    var email = context.GetArgument<string>("email");
                    if (!string.IsNullOrEmpty(email))
                        return repository.GetAsyncByCondition<User>($"Email like '%{email}%'");

                    var phoneNumber = context.GetArgument<string>("phoneNumber");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAsyncByCondition<User>($"PhoneNumber like '%{phoneNumber}%'");

                    return null;
                });
        }
    }
}