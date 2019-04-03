using Hub.DataAccessCore.Context;
using Hub.Services.Export.DataAccess.Model.Db.Tenanted;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Hub.Services.Export.DataAccess.Context
{
    public class TenantedExportDbContext : TenantedDbContext
    {
        protected DbConnection Connection { get; set; }

        private readonly bool closeConnectionOnDispose;

        public TenantedExportDbContext(DbConnection connection, bool closeConnectionOnDispose = false) : base(connection)
        {
            Connection = connection;
            this.closeConnectionOnDispose = closeConnectionOnDispose;
        }

        public TenantedExportDbContext(DbContextOptions options) : base(options)
        {
        }

        public override void Dispose()
        {
            base.Dispose();

            if (closeConnectionOnDispose)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }

        protected override void CustomOnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void CustomOnModelCreating(ModelBuilder modelBuilder)
        {
            if (!(Connection is SqliteConnection))
            {
                modelBuilder.HasDefaultSchema("exp");
            }

            modelBuilder.Entity<ExportConfiguration>().HasKey(u => new { u.ExportName });
            modelBuilder.Entity<ExportGroup>().HasKey(u => new { u.ExportName, u.GroupName });
            modelBuilder.Entity<ExportObject>().HasKey(u => new { u.Id });
            modelBuilder.Entity<ExportProperties>().HasKey(u => new { u.ExportName, u.GroupName, u.PropertyName });
            modelBuilder.Entity<ExportQueue>().HasKey(u => new { u.Id });
        }
    }
}
