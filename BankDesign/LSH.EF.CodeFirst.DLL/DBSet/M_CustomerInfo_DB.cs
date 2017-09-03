using LSH.EF.CodeFirst.DLL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LSH.EF.CodeFirst.DLL.DBSet
{
    public class M_CustomerInfo_DB : DBCreate, IDBHelper<M_CustomerInfo>
    {
        public M_CustomerInfo_DB()
            : base()
        {
            
        }
        public bool Add(M_CustomerInfo t)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                dbcon.M_CustomerInfos.Add(t);
                int i = dbcon.SaveChanges();
                return i > 0 ? true : false;
            }
        }

        public bool Add(List<M_CustomerInfo> lstT)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                foreach (var m in lstT)
                {
                    dbcon.M_CustomerInfos.Add(m);
                }
                int i = dbcon.SaveChanges();
                return i > 0 ? true : false;
            }
        }

        public bool Remove(List<string> keys)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                foreach (string key in keys)
                {
                    dbcon.M_CustomerInfos.Remove(dbcon.M_CustomerInfos.Where(p => p.CusCode == key).FirstOrDefault());
                }
                int i = dbcon.SaveChanges();
                return i > 0 ? true : false;
            }
        }

        public bool Update(M_CustomerInfo t)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                var modifyEntity = dbcon.Entry(t);
                modifyEntity.State = EntityState.Modified;

                List<MemberInfo> lstM = t.GetType().GetMembers().ToList();
                foreach (MemberInfo m in lstM)
                {
                    if (m.MemberType == MemberTypes.Property)
                        modifyEntity.Property(m.Name).IsModified = true;
                }
                int i = dbcon.SaveChanges();
                return i > 0 ? true : false;
            }
        }

        public M_Data_Paging<M_CustomerInfo> Query(WhereFilter wherefilter, int iSIndex, int iEIndex)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                //动态分配条件
                var predicate = PredicateBuilder.True<M_CustomerInfo>();
                if (wherefilter != null)
                {
                    foreach (Rules rule in wherefilter.rules)
                    {
                        if (rule.field.Equals("CusCode"))
                        {
                            predicate = predicate.And(p => p.CusCode.Contains(rule.value));
                        }
                        if (rule.field.Equals("CusName"))
                        {
                            predicate = predicate.And(p => p.CusName.Contains(rule.value));
                        }
                    }
                }

                var mcus = dbcon.M_CustomerInfos.Where(predicate.Compile());
                int iRowCount = mcus.Count();
                var mcusb = mcus.OrderBy(p => p.CusCode).Skip(iSIndex).Take(iEIndex - iSIndex).ToList();

                M_Data_Paging<M_CustomerInfo> mdp_Ccus = new M_Data_Paging<M_CustomerInfo>()
                {
                    RowCount = iRowCount,
                    lstTObj = mcusb,
                };
                return mdp_Ccus;
            }
        }

        public M_CustomerInfo Query(string cuscode)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                var mcus = dbcon.M_CustomerInfos.Find(cuscode);
                return mcus;
            }
        }
    }
}
