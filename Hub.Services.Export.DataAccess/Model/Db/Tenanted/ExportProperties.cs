using Hub.DataAccessCore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hub.Services.Export.DataAccess.Model.Db.Tenanted
{
    [Table("ExportProperties")]
    public class ExportProperties : TenantedEntity
    {
        [StringLength(128)]
        public string ExportName { get; set; }
        [StringLength(128)]
        public string GroupName { get; set; }
        [StringLength(128)]
        public string PropertyName { get; set; }
        [StringLength(255)]
        public string PropertyValue { get; set; }

        public ExportGroup ExportGroup { get; set; }
        
        public override void UpdateWith(TenantedEntity entity)
        {
            if (!(entity is ExportProperties ep))
            {
                return;
            }

            ExportName = ep.ExportName;
            GroupName = ep.GroupName;
            PropertyName = ep.PropertyName;
            PropertyValue = ep.PropertyValue;
        }
    }
}