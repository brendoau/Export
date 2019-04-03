using Hub.DataAccessCore;
using Hub.Services.Export.DataAccess.DbConfiguration;
using Hub.Services.Export.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Hub.Services.Export.DatabaseMigrationTool
{
    static class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IServiceFactory, LocalServiceFactory>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var shardSettings = config.GetSection("AppSettings:ShardSettings");
            var shardingConfiguration = new ShardingConfiguration
            {
                Server = shardSettings.GetValue<string>("Server"),
                Database = shardSettings.GetValue<string>("Map"),
                ShardMapName = "ReflexMap",
                IntegrationSecurity = shardSettings.GetValue<bool>("IntegratedSecurity"),
                ConnectTimeout = shardSettings.GetValue<int>("ConnectTimeout", 120),
                Pooling = shardSettings.GetValue<bool>("Pooling", true),
                MaxPoolSize = shardSettings.GetValue<int>("MaxPoolSize", 100),
                ApplicationName = "Export-DatabaseMigrationTool"
            };
            if (!shardingConfiguration.IntegrationSecurity)
            {
                shardingConfiguration.Username = shardSettings.GetValue<string>("Username");
                shardingConfiguration.Password = shardSettings.GetValue<string>("Password");
            }

            services.AddSingleton<ISharding>(x => new Sharding(shardingConfiguration));

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("DatabaseMigrationTool");
            logger.LogInformation("Running");

            //var dbOptions = new DbContextOptionsBuilder<TenantedExportDbContext>();
            //dbOptions.UseSqlServer(config.GetConnectionString("ReportsDB"), o => o.MigrationsAssembly("Hub.Services.Export.DataAccess"));

            //logger.LogInformation("Migrating...");
            //var dbContext = new TenantedExportDbContext(dbOptions.Options);
            //dbContext.Database.Migrate();
            //logger.LogInformation("Migrating finished");

            var serviceFactory = serviceProvider.GetService<IServiceFactory>();

            logger.LogInformation("Initializing...");
            new TenantedDbConfiguration(serviceFactory).DbInitialization();
            logger.LogInformation("Initializing finished");
        }

        public class LocalServiceFactory : IServiceFactory
        {
            private IServiceProvider ServiceProvider { get; }

            public LocalServiceFactory(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }

            public T Resolve<T>() where T : class
            {
                return ServiceProvider.GetService<T>();
            }
        }
    }
}
