using Hub.DataAccessCore;
using Hub.Services.Export.DataAccess.Context;
using Hub.Services.Reporting.Interfaces;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Hub.Services.Export.DataAccess.DbConfiguration
{
    public class TenantedDbConfiguration
    {
        private ILogger Logger { get; }
        private IServiceFactory ServiceFactory { get; }

        public TenantedDbConfiguration(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
            Logger = serviceFactory.Resolve<ILoggerFactory>().CreateLogger("TenantedDbConfiguration");
        }

        public TenantedDbConfiguration(IServiceFactory serviceFactory, ILogger logger)
        {
            ServiceFactory = serviceFactory;
            Logger = logger;
        }

        public void DbInitialization()
        {
            try
            {
                var sharding = ServiceFactory.Resolve<ISharding>();

                var connectionString = GetAuthenticationConnectionString(sharding);

                foreach (var shard in sharding.ShardMap.GetShards().ToList())
                {
                    MigrateShard(connectionString, shard);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        }

        public void DbInitialization(string shardName)
        {
            try
            {
                var sharding = ServiceFactory.Resolve<ISharding>();

                var shard = sharding.ShardMap.GetShards().FirstOrDefault(s => s.Location.Database == shardName);
                if (shard == null)
                {
                    throw new KeyNotFoundException($"Cannot find database with name: {shardName}");
                }

                var connectionString = GetAuthenticationConnectionString(sharding);

                MigrateShard(connectionString, shard);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        }

        private void MigrateShard(string connectionString, Shard shard)
        {
            using (var connection = shard.OpenConnection(connectionString))
            using (var context = new TenantedExportDbContext(connection))
            {
                Logger.LogInformation($"Running database migration on {shard.Location.Database}");
                context.Database.Migrate();
                Logger.LogInformation($"Running database migration on {shard.Location.Database} completed");
            }
        }

        private string GetAuthenticationConnectionString(ISharding sharding)
        {
            var configuration = sharding.Configuration;

            var builder = new SqlConnectionStringBuilder()
            {
                IntegratedSecurity = configuration.IntegrationSecurity,
                PersistSecurityInfo = true,
                ConnectTimeout = configuration.ConnectTimeout,
                Pooling = configuration.Pooling,
                MaxPoolSize = configuration.MaxPoolSize,
                ApplicationName = configuration.ApplicationName
            };

            if (!configuration.IntegrationSecurity)
            {
                builder.UserID = configuration.Username;
                builder.Password = configuration.Password;
            }

            return builder.ConnectionString;
        }
    }
}
