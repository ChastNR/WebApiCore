using DataRepository.Contracts;
 using GraphQL.Types;
 
 namespace DataRepository.GraphQl.Types
 {
     public class UserType : ObjectGraphType<User>
     {
         public UserType()
         {
             Field(x => x.Id, type: typeof(IdGraphType)).Description("User Id");
             Field(x => x.Name);
             Field(x => x.PhoneNumber);
             Field(x => x.Email);
             Field(x => x.PasswordHash);
             Field(x => x.CreationDate);
             Field(x => x.Age);
         }
     }
 }