using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSH.EF.CodeFirst.DLL.Model.ModelMap
{
    public class M_UserMap : EntityTypeConfiguration<M_User>
    {
        public M_UserMap()
        {
            HasKey(p => p.UserID);
            Property(p => p.UserID).HasMaxLength(20).IsRequired();
            Property(p => p.UserName).HasMaxLength(20).IsRequired();
            Property(p => p.UserPermission).HasMaxLength(20).IsRequired();
            Property(p => p.UserPwd).HasMaxLength(20).IsRequired();
            Property(p => p.UserState).IsRequired();
            Property(p => p.Remark).HasMaxLength(2000).IsOptional();
            Property(p => p.AddOrUpdateUserID).HasMaxLength(20).IsOptional();
        }
    }
}
