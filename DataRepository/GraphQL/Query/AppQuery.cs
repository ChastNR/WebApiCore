using System;
using DataRepository.Contracts;
using DataRepository.GraphQL.Types;
using DataRepository.Interfaces.Base;
using GraphQL.Types;

namespace DataRepository.GraphQL.Query
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(ISqlRepository repository)
        {
            #region UserType
            
            Field<ListGraphType<UserType>>(
                "users",
                arguments: UserQueryArguments(),
                resolve: _ =>
                {
                    var id = _.GetArgument<Guid>("id");
                    if (id != Guid.Empty)
                        return repository.GetAllAsyncByCondition<User>($"Id = '{id}'");

                    var name = _.GetArgument<string>("name");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAllAsyncByCondition<User>($"Name like '%{name}%'");

                    var email = _.GetArgument<string>("email");
                    if (!string.IsNullOrEmpty(email))
                        return repository.GetAllAsyncByCondition<User>($"Email like '%{email}%'");

                    var phoneNumber = _.GetArgument<string>("phoneNumber");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAllAsyncByCondition<User>($"PhoneNumber like '%{phoneNumber}%'");

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
                        return repository.GetAsyncByCondition<User>($"Name like '%{name}%'");

                    var email = _.GetArgument<string>("email");
                    if (!string.IsNullOrEmpty(email))
                        return repository.GetAsyncByCondition<User>($"Email like '%{email}%'");

                    var phoneNumber = _.GetArgument<string>("phoneNumber");
                    if (!string.IsNullOrEmpty(name))
                        return repository.GetAsyncByCondition<User>($"PhoneNumber like '%{phoneNumber}%'");

                    return null;
                });
            
            #endregion
        }

        #region AdditionalMethods
        
        private QueryArguments UserQueryArguments()
            => new QueryArguments(
                new QueryArgument<IdGraphType> {Name = "id"},
                new QueryArgument<StringGraphType> {Name = "name"},
                new QueryArgument<StringGraphType> {Name = "email"},
                new QueryArgument<StringGraphType> {Name = "phoneNumber"},
                new QueryArgument<IntGraphType> {Name = "age"},
                new QueryArgument<StringGraphType> {Name = "passwordHash"},
                new QueryArgument<DateTimeGraphType> {Name = "creationDate"}
            );
        #endregion
        
    }
}