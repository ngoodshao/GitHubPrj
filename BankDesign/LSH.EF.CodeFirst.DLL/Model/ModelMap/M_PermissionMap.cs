using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSH.EF.CodeFirst.DLL.Model.ModelMap
{
    public class M_PermissionMap: EntityTypeConfiguration<M_Permission>
    {
        public M_PermissionMap()
        {
            HasKey(p => p.PermissionID);
            Property(p => p.PermissionID).HasMaxLength(20).IsRequired();
            Property(p => p.PermissionName).HasMaxLength(20).IsRequired();
            Property(p => p.PermissionIndex).HasMaxLength(20).IsRequired();
            Property(p => p.Remark).HasMaxLength(2000).IsOptional();
        }
    }
}
