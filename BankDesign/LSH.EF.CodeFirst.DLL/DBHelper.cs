using LSH.EF.CodeFirst.DLL.DBSet;
using LSH.EF.CodeFirst.DLL.Model;
using LSH.EF.CodeFirst.DLL.Model.ModelMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LSH.EF.CodeFirst.DLL
{
    public interface IDBHelper<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add(T t);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool Remove(List<string> keys);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(T t);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="wherefilter"></param>
        /// <param name="iSIndex"></param>
        /// <param name="iEIndex"></param>
        /// <returns></returns>
        M_Data_Paging<T> Query(WhereFilter wherefilter, int iSIndex, int iEIndex);
    }
    public class DBCreate
    {
        public DBCreate()
        {
            //初始化数据库
            Database.SetInitializer(new InitDB());
        }
    }
    public class M_Data_Paging<T>
    {
        public int RowCount { get; set; }
        public List<T> lstTObj { get; set; }
    }

    public class DBHelperContext : DbContext
    {
        public DBHelperContext()
            : base("name=DBConnect")
        {

        }

        public DbSet<M_User> M_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new M_UserMap());
        }
    }
    /// <summary>
    /// 如果数据结构变化，则重新生成
    /// </summary>
    public class InitDB : DropCreateDatabaseIfModelChanges<DBHelperContext>
    {
        //Insert data default
        protected override void Seed(DBHelperContext context)
        {
            //context.Addresses.Add(new Address() { Street = "abc" });
            //base.Seed(context);
        }
    }
    /// <summary>
    /// 动态添加查询条件
    /// </summary>
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    public class SQLDBHelper
    {
        public static T CreateDBClass<T>() where T : new()
        {
            return new T();
        }

    }

    public class WhereFilter
    {
        //{"rules":[{"field":"UserName","op":"equal","value":"Leishaohua0","type":"string"},{"field":"UserID","op":"equal","value":"Lei0","type":"string"}],"op":"and"}
        public List<Rules> rules { get; set; }
        public string op { get; set; }

        public string GetWhere()
        {
            string sWhere = " ";
            foreach (Rules rule in rules)
            {
                if (!rule.field.Equals("undefined"))
                {
                    sWhere += rule.Operator;
                }
            }
            return sWhere;
        }
    }

    public class Rules
    {
        public string field { get; set; }
        public string op
        {
            get;
            set;
        }
        public string value { get; set; }
        public string type { get; set; }

        public string Operator
        {
            get
            {
                string oper = "";
                switch (op)
                {
                    //case "and":
                    //    oper = "and {0}";
                    //    break;
                    //case "or":
                    //    oper = "or {0}";
                    //    break;
                    case "equal":
                        oper = field + " = '" + value + "'";
                        break;
                    case "notequal":
                        oper = field + " !=";
                        break;
                    case "startwith":
                        oper = field + " like '" + value + "%'";
                        break;
                    case "endwith":
                        oper = field + " like '%" + value + "'";
                        break;
                    case "like":
                        oper = field + " like '%" + value + "%'";
                        break;
                    case "greater":
                        oper = field + " > '" + value + "'";
                        break;
                    case "greaterorequal":
                        oper = field + " >= '" + value + "'";
                        break;
                    case "less":
                        oper = field + " < '" + value + "'";
                        break;
                    case "lessorequal":
                        oper = field + " <= '" + value + "'";
                        break;
                    case "in":
                        oper = field + " in (" + value + ")";
                        break;
                    case "notin":
                        oper = field + " not in (" + value + ")";
                        break;
                    default:
                        oper = field + "  = '" + value + "'";
                        break;
                }
                return " and " + oper;
            }
        }

    }
}
