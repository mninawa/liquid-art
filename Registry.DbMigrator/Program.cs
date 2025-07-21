using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Registry.Business.Abstraction;
using Registry.Business.Seeder;
using Registry.Repository.Abstraction;

namespace Registry.DbMigrator
{
    
    public class Program
    {
        
        public static async Task Main(string[] args)
        {
            //var test = new CitySeeder();
             await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddHostedService<CitySeeder>();
                    services.AddScoped<IRepository, Repository.Repository>();
                    services.AddScoped<IBusiness, Business.Business>();
                    services.AddHostedService<DbMigratorHostedService>();
                });
    }
    // public class Program
    // {
    //     public static async Task Main(string[] args)
    //     {
    //         await CreateHostBuilder(args).Build().RunAsync();
    //     }
    //
    //     internal static IHostBuilder CreateHostBuilder(string[] args)
    //     {
    //         return Host.CreateDefaultBuilder(args).ConfigureLogging(ConfigureLogging) .ConfigureServices(ConfigureServices);
    //     }
    //
    //     private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging)
    //     {
    //         logging.ClearProviders();
    //     }
    //
    //     private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    //     {
    //         services.AddHostedService<DbMigratorHostedService>();
    //         services.AddScoped<Repository.Abstraction.IRepository, Repository.Repository>();
    //         services.AddScoped<IBusiness, Business.Business>();
    //         //services.AddScoped<CitySeeder>();
    //     }
    //     
    // }
}