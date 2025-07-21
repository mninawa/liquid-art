using Registry.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Volo.Abp.Autofac;
 using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Registry.DbMigrator   
{
    [DependsOn(
        typeof(AbpAutofacModule)/*,
        typeof(MeowvBlogApplicationModule),
        typeof(MeowvBlogMongoDbModule)*/
    )]
    public class MeowvBlogDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddYamlFile("appsettings.yml", true, true)
                                                   .Build();

            
            context.Services.Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = config.GetSection("storage").GetValue<string>("mongodb");
            });
        }
    }
}