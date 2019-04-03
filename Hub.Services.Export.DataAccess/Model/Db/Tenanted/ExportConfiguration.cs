using Hub.DataAccessCore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hub.Services.Export.DataAccess.Model.Db.Tenanted
{
    [Table("ExportConfiguration")]
    public class ExportConfiguration : TenantedEntity
    {
        [StringLength(128)]
        public string ExportName { get; set; }
        [StringLength(128)]
        public string ExportProgram { get; set; }
        [StringLength(20)]
        public string ExportType { get; set; }
        [StringLength(255)]
        public string PreExecute { get; set; }
        [StringLength(255)]
        public string PostExecute { get; set; }

        public override void UpdateWith(TenantedEntity entity)
        {
            if (!(entity is ExportConfiguration ec))
            {
                return;
            }

            ExportName = ec.ExportName;
            ExportProgram = ec.ExportProgram;
            ExportType = ec.ExportType;
            PreExecute = ec.PreExecute;
            PostExecute = ec.PostExecute;
        }
    }
}