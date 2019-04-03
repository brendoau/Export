using Hub.DataAccessCore;
using Hub.Services.Export.DataAccess.DbConfiguration;
using Hub.Services.Export.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hub.Services.Export.AzureFunctions
{
    public static class MigrateDatabaseShard
    {
        [FunctionName("MigrateDatabaseShard")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, ILogger log, ExecutionContext context)
        {
            log.LogInformation("MigrateDatabaseShard triggered");

            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IServiceFactory, LocalServiceFactory>();

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("connectionStrings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"connectionStrings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var jsonContent = await req.Content.ReadAsStringAsync();
            var shardInfo = JsonConvert.DeserializeObject<ShardInfo>(jsonContent);
            log.LogInformation($"Database Name: {shardInfo.ShardName}");

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
                ApplicationName = shardSettings.GetValue<string>("ApplicationName", "Export-MigrateDatabaseShard")
            };
            if (!shardingConfiguration.IntegrationSecurity)
            {
                shardingConfiguration.Username = shardSettings.GetValue<string>("Username");
                shardingConfiguration.Password = shardSettings.GetValue<string>("Password");
            }

            services.AddSingleton<ISharding>(x => new Sharding(shardingConfiguration));

            var serviceProvider = services.BuildServiceProvider();

            var serviceFactory = serviceProvider.GetService<IServiceFactory>();

            log.LogInformation("Initializing...");
            new TenantedDbConfiguration(serviceFactory, log).DbInitialization(shardInfo.ShardName);
            log.LogInformation("Initializing finished");

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class ShardInfo
    {
        public string ShardName { get; set; }
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