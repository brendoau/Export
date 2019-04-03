﻿using Hub.DataAccessCore.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hub.Services.Export.DataAccess.Model.Db.Tenanted
{
    [Table("ExportQueue")]
    public class ExportQueue : TenantedEntity
    {
        [StringLength(128)]
        public string ExportName { get; set; }
        [StringLength(128)]
        public string GroupName { get; set; }
        [StringLength(255)]
        public string ObjectName { get; set; }
        [StringLength(255)]
        public string ReferenceId { get; set; }
        public DateTime DateExported { get; set; }

        public override void UpdateWith(TenantedEntity entity)
        {
            if (!(entity is ExportQueue eq))
            {
                return;
            }

            ExportName = eq.ExportName;
            GroupName = eq.GroupName;
            ObjectName = eq.ObjectName;
            ReferenceId = eq.ReferenceId;
            DateExported = eq.DateExported;
        }
    }
}