using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Registry.DbMigrator.Commands;
using Registry.DbMigrator.Configurations;
using Registry.Business.Seeder;

namespace Registry.DbMigrator
{
   
    public class Program
    {
        
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting application...");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new ContainerBuilder();
            var startup = new Startup(configuration);
            startup.ConfigureServices(builder);

            // Logging setup
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure => configure.AddConsole());
            builder.Populate(serviceCollection);

            var container = builder.Build();
            await using (var scope = container.BeginLifetimeScope())
            {
                var seeder = scope.Resolve<CitySeeder>();
                await seeder.SeedAsync();
            }
            // TODO: Parse command-line arguments
            // Parser.Default.ParseArguments<Options>(args)
            //     .WithParsed<Options>(async options =>
            //     {
            //         using (var scope = container.BeginLifetimeScope())
            //         {
            //             var seeder = scope.Resolve<CitySeeder>();
            //             await seeder.SeedAsync();
            //         }
            //     });

            Console.WriteLine("Application finished.");
        }
    }
  
}