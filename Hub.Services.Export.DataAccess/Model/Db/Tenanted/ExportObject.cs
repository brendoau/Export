using Hub.DataAccessCore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hub.Services.Export.DataAccess.Model.Db.Tenanted
{
    [Table("ExportObject")]
    public class ExportObject : TenantedEntity
    {
        [StringLength(128)]
        public string ExportName { get; set; }
        [StringLength(128)]
        public string GroupName { get; set; }
        [StringLength(128)]
        public string ObjectName { get; set; }
        public int Sequence { get; set; }
        [StringLength(10)]
        public string SourceType { get; set; }
        [StringLength(255)]
        public string Source { get; set; }
        [StringLength(250)]
        public string PrimaryKey { get; set; }
        [StringLength(250)]
        public string ExcludeFields { get; set; }
        [StringLength(128)]
        public string OutputName { get; set; }
        [StringLength(250)]
        public string OrderBy { get; set; }
        [StringLength(255)]
        public string Filter { get; set; }
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        public override void UpdateWith(TenantedEntity entity)
        {
            if (!(entity is ExportObject eo))
            {
                return;
            }

            ExportName = eo.ExportName;
            GroupName = eo.GroupName;
            ObjectName = eo.ObjectName;
            Sequence = eo.Sequence;
            SourceType = eo.SourceType;
            Source = eo.Source;
            PrimaryKey = eo.PrimaryKey;
            ExcludeFields = eo.ExcludeFields;
            OutputName = eo.OutputName;
            OrderBy = eo.OrderBy;
            Filter = eo.Filter;
            isActive = eo.isActive;
        }
    }
}