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
    public class M_User_DB : DBCreate, IDBHelper<M_User>
    {
        public M_User_DB()
            : base()
        {
            
        }
        public bool Add(M_User t)
        {
            using (DBHelperContext dbcon =new DBHelperContext())
            {
                dbcon.M_Users.Add(t);
                int i = dbcon.SaveChanges();
                return i > 0 ? true : false;
            }
        }

        public bool Add(List<M_User> lstUser)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                foreach (var m in lstUser)
                {
                    dbcon.M_Users.Add(m);
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
                    dbcon.M_Users.Remove(dbcon.M_Users.Where(p => p.UserID == key).FirstOrDefault());

                }
                int i = dbcon.SaveChanges();
                return i > 0 ? true : false;
            }
        }

        public bool Update(M_User t)
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

        public M_Data_Paging<M_User> Query(WhereFilter wherefilter, int iSIndex, int iEIndex)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                //动态分配条件
                var predicate = PredicateBuilder.True<M_User>();
                //List<MemberInfo> lstM =typeof(M_User).GetMembers().ToList();
                //var lstC=(from c in lstM
                //         select new {
                //             colName=c.Name,
                //         }).ToList();

                //foreach (MemberInfo m in lstM)
                //{
                //    modifyEntity.Property(m.Name).IsModified = true;
                //}

                if (wherefilter!=null)
                {
                    foreach (Rules rule in wherefilter.rules)
                    {
                        if (rule.field.Equals("UserID"))
                        {
                            predicate = predicate.And(p => p.UserID.Contains(rule.value));
                        }
                        if (rule.field.Equals("UserName"))
                        {
                            predicate = predicate.And(p => p.UserName.Contains(rule.value));
                        }
                    }
                }


                //if (string.IsNullOrEmpty(wherefilter.UserID))
                //{
                //    predicate = predicate.And(p => p.UserID.Contains(wherefilter.UserID));
                //}
                //if (string.IsNullOrEmpty(wherefilter.UserName))
                //{
                //    predicate = predicate.And(p => p.UserName.Contains(wherefilter.UserName));
                //}

                //predicate = predicate.And(p => p.UserActive.Equals(wherefilter.UserActive));

                //if (string.IsNullOrEmpty(wherefilter.Remark))
                //{
                //    predicate = predicate.And(p => p.Remark.Contains(wherefilter.Remark));
                //}

                var musers = dbcon.M_Users.Where(predicate.Compile());
                int iRowCount = musers.Count();
                var muserb = musers.OrderBy(p => p.UserID).Skip(iSIndex).Take(iEIndex - iSIndex).ToList();

                M_Data_Paging<M_User> mdp_Users = new M_Data_Paging<M_User>()
                {
                    RowCount = iRowCount,
                    lstTObj = muserb,
                };
                return mdp_Users;
            }
        }

        public M_User Query(string userid)
        {
            using (DBHelperContext dbcon = new DBHelperContext())
            {
                var muser = dbcon.M_Users.Find(userid);
                return muser;
            }
        }
    }
}
