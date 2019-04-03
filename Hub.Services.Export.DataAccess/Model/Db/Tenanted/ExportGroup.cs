using System.Collections.Generic;
using Hub.DataAccessCore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hub.Services.Export.DataAccess.Model.Db.Tenanted
{
    [Table("ExportGroup")]
    public class ExportGroup : TenantedEntity
    {
        [StringLength(128)]
        public string ExportName { get; set; }
        [StringLength(128)]
        public string GroupName { get; set; }
        [StringLength(50)]
        public string GroupType { get; set; }
        [Required]
        [StringLength(250)]
        public string InternalFolder { get; set; }
        [Required]
        [StringLength(250)]
        public string ExternalFolder { get; set; }
        [StringLength(250)]
        public string ArchiveFolder { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [Column(TypeName = "bit")]
        public bool UpdateExportURL { get; set; }
        [Column(TypeName = "bit")]
        public bool? IsIncremental { get; set; }
        [StringLength(128)]
        public string QueueTable { get; set; }
        [StringLength(250)]
        public string OrderBy { get; set; }
        [StringLength(255)]
        public string Filter { get; set; }
        [StringLength(50)]
        public string FileSuffix { get; set; }
        [Column(TypeName = "bit")]
        public bool? SendEmail { get; set; }
        [StringLength(250)]
        public string ToAddr { get; set; }
        [StringLength(50)]
        public string Project { get; set; }
        [StringLength(50)]
        public string Role { get; set; }

        public ExportConfiguration ExportConfiguration { get; set; }

        public List<ExportObject> ExportObjects { get; set; }
        public List<ExportProperties> ExportProperties { get; set; }
        public List<ExportQueue> ExportQueues { get; set; }

        public override void UpdateWith(TenantedEntity entity)
        {
            if (!(entity is ExportGroup eg))
            {
                return;
            }

            ExportName = eg.ExportName;
            GroupName = eg.GroupName;
            GroupType = eg.GroupType;
            InternalFolder = eg.InternalFolder;
            ExternalFolder = eg.ExternalFolder;
            ArchiveFolder = eg.ArchiveFolder;
            IsActive = eg.IsActive;
            UpdateExportURL = eg.UpdateExportURL;
            IsIncremental = eg.IsIncremental;
            QueueTable = eg.QueueTable;
            OrderBy = eg.OrderBy;
            Filter = eg.Filter;
            FileSuffix = eg.FileSuffix;
            SendEmail = eg.SendEmail;
            ToAddr = eg.ToAddr;
            Project = eg.Project;
            Role = eg.Role;
        }
    }
}