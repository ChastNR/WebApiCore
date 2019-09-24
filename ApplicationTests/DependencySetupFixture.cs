using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlRepository.Interfaces;
using SqlRepository.Repositories;
using System;
using System.IO;

namespace ApplicationTests
{
    public class DependencySetupFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public DependencySetupFixture()
        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName)
//                .AddJsonFile("settings.json").Build();
//
//            var serviceCollection = new ServiceCollection();
//
//            serviceCollection.AddTransient<IDataRepository>(s => new DataRepository(builder.GetSection("DbConnection").Value));

            //ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
