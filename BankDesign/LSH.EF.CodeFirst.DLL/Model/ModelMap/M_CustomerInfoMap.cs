using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace LSH.EF.CodeFirst.DLL.Model.ModelMap
{
    public class M_CustomerInfoMap:EntityTypeConfiguration<M_CustomerInfo>
    {
        public M_CustomerInfoMap()
        {
            HasKey(p => p.CusCode);
            Property(p => p.CusCode).HasMaxLength(20).IsRequired();
            Property(p => p.CusName).HasMaxLength(20).IsRequired();
            Property(p => p.Gender).IsRequired();
            Property(p => p.BornDate).IsOptional();
            Property(p => p.ContactNO).HasMaxLength(50).IsOptional();
            Property(p => p.Address).HasMaxLength(200).IsOptional();
            Property(p => p.IDCard).HasMaxLength(20).IsRequired();
            Property(p => p.EmergencyLink).HasMaxLength(200).IsOptional();
            Property(p => p.Remark).HasMaxLength(2000).IsOptional();
        }
    }
}
