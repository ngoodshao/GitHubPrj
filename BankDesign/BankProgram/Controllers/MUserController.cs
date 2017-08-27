using BankProgram.Models;
using LSH.EF.CodeFirst.DLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            bool iTrue = SQLDBHelperClient.Create().Add(muser);
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
	}
}