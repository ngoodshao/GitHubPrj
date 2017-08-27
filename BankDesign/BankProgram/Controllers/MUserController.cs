using BankProgram.Infrastructure;
using BankProgram.Models;
using LSH.EF.CodeFirst.DLL;
using LSH.EF.CodeFirst.DLL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BankProgram.Controllers
{
    public class MUserController : Controller
    {
        
        #region Query 用js调用ashx来实现数据查询
        //
        // GET: /MUser/
        public ActionResult Query()
        {
            return View();
        }
        #endregion

        #region Add
        [HttpPost]
        public ActionResult Add(M_User muser)
        {
            //return RedirectToAction("Edit", muser);
            bool iTrue = SQLDBHelperClient.CreateUser().Add(muser);
            if (iTrue )
            {
                ViewBag.Succ = "1";
                ViewBag.Msg = "数据添加成功!";
                return View(new M_User());
                //return RedirectToAction("ListPageLigerUI");
            }
            else
            {
                ViewBag.Succ = "0";
                ViewBag.Msg = "数据添加失败!";
                return View(muser);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(string userid)
        {
            bool iSucc=false;
            //if (string.IsNullOrEmpty(userid))
            //{
                if (Request.Params["userids"] != null)
                {
                    string[] strUserid = Request.Params["userids"].Split(',');
                    iSucc = SQLDBHelperClient.CreateUser().Remove(Request.Params["userids"].Split(',').ToList());
                }
            //}

            //M_User[] lstUsers = db.QueryUser("");
            //return RedirectToAction("ListPageLigerUI");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("result", iSucc ? "1" : "0");
            dic.Add("errMsg", iSucc ? "数据删除成功!" : "数据删除失败!");

            var res = new JsonResult();
            res.Data = dic;
            return res;// new JavaScriptSerializer().Serialize(dic);
        }
        #endregion

        #region Edit
        [HttpPost]
        public ActionResult Edit(M_User muser)
        {
            if (ModelState.IsValid)
            {
                SQLDBHelperClient.CreateUser().Update(muser);
                ViewBag.Succ = "1";
                ViewBag.Msg = "数据编辑成功!";
                return View("Edit");
            }
            else
            {
                //ViewData["succ"] = "0";
                ViewBag.Succ = "0";
                ViewBag.Msg = "数据编辑失败!";
                return View(muser);
            }
        }

        [HttpGet]
        public ActionResult Edit(string userid)
        {
            M_User User = SQLDBHelperClient.CreateUser().Query(userid);
            return View("Edit", User);
        }
        #endregion


        #region Import
        [HttpPost]
        public ActionResult Import(string id)
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                DataTable dt = CommClass.CreateTB("UserName:System.String,UserID:System.String,UserPwd:System.String,UserPermission:System.String,UserState:System.Int32,Remark:System.String");
                string str = NPOIExcel.ImportExcelFile(files, dt, 1, 0);

                List<M_User> lstUser = new JavaScriptSerializer().Deserialize<List<M_User>>(str);
                bool iSucc = SQLDBHelperClient.CreateUser().Add(lstUser);
                if (iSucc)
                {
                    ViewBag.Succ = "1";
                    ViewBag.Msg = "数据添加成功!";
                    return View();
                    //return RedirectToAction("ListPageLigerUI");
                }
                else
                {
                    ViewBag.Succ = "0";
                    ViewBag.Msg = "数据添加失败!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Succ = "0";
                ViewBag.Msg = "数据添加失败!";// +ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }
        #endregion

        #region Export
        [HttpPost]
        public ActionResult Export(string id)
        {
            try
            {
                string sWhere = "";
                //条件
                WhereFilter QF = null;
                if (!string.IsNullOrEmpty(Request.Params["where"]))
                {
                    QF = new JavaScriptSerializer().Deserialize<WhereFilter>(Request.Params["where"]);
                    //sWhere = QF.GetWhere();
                }
                //DataTable dt = SQLDBHelper.MUser.QueryDT(sWhere, 1, int.MaxValue);
                M_Data_Paging<M_User> lstMUserPage = SQLDBHelperClient.CreateUser().Query(QF, 1, int.MaxValue);
                Stream stream = NPOIExcel.RenderDataTableToExcelHSS(lstMUserPage.lstTObj);
                ViewBag.Succ = "1";
                ViewBag.Msg = "数据下载成功!";
                return File(stream, "application/vnd.ms-excel", DateTime.Now.Ticks + ".xls");
            }
            catch (Exception ex)
            {
                return View("Query");
            }
        }

        #endregion
	}
}