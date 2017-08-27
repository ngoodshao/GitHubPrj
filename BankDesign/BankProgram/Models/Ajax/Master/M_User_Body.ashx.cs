using BankProgram.Infrastructure;
using LSH.EF.CodeFirst.DLL;
using LSH.EF.CodeFirst.DLL.DBSet;
using LSH.EF.CodeFirst.DLL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace BankProgram.Models.Ajax.Master
{
    /// <summary>
    /// M_User_Body 的摘要说明
    /// </summary>
    public class M_User_Body : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                int PageSize = Convert.ToInt32(context.Request.Params["pagesize"]);
                int currentpage = Convert.ToInt32(context.Request.Params["page"]);

                string sWhere = "";
                WhereFilter QF = null;
                //条件
                if (context.Request.Params["where"] != null)
                {
                    QF = new JavaScriptSerializer().Deserialize<WhereFilter>(context.Request.Params["where"]);
                    //sWhere = QF.GetWhere();
                }
                //排序
                if (context.Request.Params["sortorder"] != null)
                {
                    string sortname = context.Request.Params["sortname"];
                    string sortorder = context.Request.Params["sortorder"];
                    sWhere += " order by " + sortname + " " + sortorder;
                }

                //M_User m = new M_User
                //{
                //    UserID = "a",
                //    UserName = "b",
                //    UserPwd = "s",
                //    UserPermission = "1,2",
                //    UserState = 1,
                //    Remark = "abc",
                //};

                //bool i = SQLDBHelper.CreateDBClass<M_User_DB>().Add(m);

                M_Data_Paging<M_User> lstMUserPage = SQLDBHelper.CreateDBClass<M_User_DB>().Query(QF, (currentpage - 1) * PageSize, currentpage * PageSize);
                //分页
                var griddata = new { Rows = lstMUserPage.lstTObj, Total = lstMUserPage.RowCount };
                string str = new JavaScriptSerializer().Serialize(griddata);
                context.Response.Write(str);

            }
            catch (Exception ex)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}