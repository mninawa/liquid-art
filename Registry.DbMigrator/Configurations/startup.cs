
using System;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Registry.Business.Profiles;
using Registry.Business.Seeder;
using Registry.Repository.Abstraction;

namespace Registry.DbMigrator.Configurations
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterInstance(Configuration).As<IConfiguration>();
            builder.RegisterType<MongoConnectionSetting>().As<IMongoConnectionSetting>().SingleInstance();
            builder.RegisterType<Repository.Repository>().As<IRepository>().InstancePerDependency();
            builder.RegisterType<CitySeeder>().AsSelf().InstancePerDependency();

            // Register AutoMapper
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.IsSubclassOf(typeof(AssemblyMarker)))
                .As<Profile>();
            
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                // Use AddMaps to find profiles in the specified assemblies
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            })).AsSelf().SingleInstance();
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve)).As<IMapper>()
                .InstancePerLifetimeScope();
            
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve)).As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}

