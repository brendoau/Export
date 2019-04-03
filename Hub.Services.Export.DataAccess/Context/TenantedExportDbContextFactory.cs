using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Hub.Services.Export.DataAccess.Context
{
    public class TenantedExportDbContextFactory : IDesignTimeDbContextFactory<TenantedExportDbContext>
    {
        public TenantedExportDbContext CreateDbContext(string[] args)
        {
            var sharedFolder = Path.Combine(Directory.GetCurrentDirectory(), "..");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(sharedFolder, "connectionStrings.json"), optional: true)
                .AddJsonFile(Path.Combine(sharedFolder, "connectionStrings.Development.json"), optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<TenantedExportDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DevShardDB"));
            return new TenantedExportDbContext(builder.Options);
        }
    }
}
