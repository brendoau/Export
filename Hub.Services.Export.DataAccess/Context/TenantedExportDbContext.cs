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

            modelBuilder.Entity<ExportConfiguration>().HasKey(c => new {c.ExportName});

            modelBuilder.Entity<ExportGroup>().HasKey(g => new { g.ExportName, g.GroupName });
            modelBuilder.Entity<ExportGroup>()
                .HasOne(g => g.ExportConfiguration)
                .WithMany(c => c.ExportGroups)
                .HasForeignKey(g => g.ExportName);

            modelBuilder.Entity<ExportObject>().HasKey(o => new { o.Id });
            modelBuilder.Entity<ExportObject>()
                .HasOne(o => o.ExportGroup)
                .WithMany(g => g.ExportObjects)
                .HasForeignKey(o => new {o.ExportName, o.GroupName});
            
            modelBuilder.Entity<ExportProperties>().HasKey(p => new { p.ExportName, p.GroupName, p.PropertyName });
            modelBuilder.Entity<ExportProperties>()
                .HasOne(p => p.ExportGroup)
                .WithMany(g => g.ExportProperties)
                .HasForeignKey(p => new { p.ExportName, p.GroupName });

            modelBuilder.Entity<ExportQueue>().HasKey(q => new { q.Id });
            modelBuilder.Entity<ExportQueue>()
                .HasOne(q => q.ExportGroup)
                .WithMany(g => g.ExportQueues)
                .HasForeignKey(q => new { q.ExportName, q.GroupName });
        }
    }
}
