using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SqlRepository.Interfaces;
using UniversalWebApi.Models;
using Xunit;

namespace ApplicationTests.SqlRepository
{
    public class RepositoryTests : IClassFixture<DependencySetupFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        public RepositoryTests(DependencySetupFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void GetAll()
        {
            using( var scope = _serviceProvider.CreateScope())
            {
                var expectedResult = scope.ServiceProvider.GetService<ISqlRepository>().GetAllAsync<User>();
                
                Assert.NotNull(expectedResult);
            }
        }

        [Fact]
        public void GetOneById()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var expectedResult = scope.ServiceProvider.GetService<ISqlRepository>().GetAsync<User>(1);

                Assert.NotNull(expectedResult);
                Assert.True(expectedResult.Id == 1);
            }
        }

        [Fact]
        public async Task AddOne()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<ISqlRepository>();
                var name = Guid.NewGuid().ToString();
                var user = new User
                {
                    Name = name,
                    Email = "01234",
                    Age = 01234
                };

                await repository.InsertAsync(user);

                var expectedResult = repository.GetAsync<User>($"Name = '{name}'").Result;

                Assert.Equal(expectedResult.Name, user.Name);

                await repository.DeleteRowAsync<User>(expectedResult.Id);
            }
        }

        //[Fact]
        //public async Task DeleteOne()
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var repository = scope.ServiceProvider.GetService<IDataRepository>();
        //        var name = Guid.NewGuid().ToString();
        //        var user = new User
        //        {
        //            Name = name,
        //            Email = "012345",
        //            Age = 012345
        //        };

        //        await repository.InsertAsync(user);

        //        var insertedUser = repository.Get<User>($"Name = '{name}'");

        //        await repository.DeleteRowAsync<User>(insertedUser.Id);

        //        var result = repository.Get<User>(insertedUser.Id);

        //        Assert.Null(result);
        //    }
        //}
    }
}
