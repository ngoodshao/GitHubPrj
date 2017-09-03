using BankProgram.Models;
using LSH.EF.CodeFirst.DLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankProgram.Controllers
{
    public class MCustomerInfoController : Controller
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
        public ActionResult Add(M_CustomerInfo mcus)
        {
            mcus.AddOrUpdateUserID = "";
            bool iTrue = SQLDBHelperClient.CreateCus().Add(mcus);

            if (iTrue)
            {
                ViewBag.Succ = "1";
                ViewBag.Msg = "数据添加成功!";
                return View(new M_CustomerInfo());
            }
            else
            {
                ViewBag.Succ = "0";
                ViewBag.Msg = "数据添加失败!";
                return View(mcus);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.AState = "ADD";
            return View();
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(string keys)
        {
            bool iSucc = false;
            //if (string.IsNullOrEmpty(userid))
            //{
            if (Request.Params["keys"] != null)
            {
                string[] strUserid = Request.Params["keys"].Split(',');
                iSucc = SQLDBHelperClient.CreateCus().Remove(Request.Params["keys"].Split(',').ToList());
            }
            //}
            if (iSucc)
            {
                ViewBag.Succ = "1";
                ViewBag.Msg = "数据删除成功!";
            }
            else
            {
                ViewBag.Succ = "0";
                ViewBag.Msg = "数据删除失败!";
            }
            return View("Query");
        }
        #endregion

        #region Edit
        [HttpPost]
        public ActionResult Edit(M_CustomerInfo mcus)
        {
            if (ModelState.IsValid)
            {
                SQLDBHelperClient.CreateCus().Update(mcus);
                ViewBag.Succ = "1";
                ViewBag.Msg = "数据编辑成功!";
                //return View("Query");
                return View("Edit");
            }
            else
            {
                //ViewData["succ"] = "0";
                ViewBag.Succ = "0";
                ViewBag.Msg = "数据编辑失败!";
                return View(mcus);
            }
        }

        [HttpGet]
        public ActionResult Edit(string key)
        {
            ViewBag.AState = "EDIT";
            M_CustomerInfo Cus = SQLDBHelperClient.CreateCus().Query(key);
            return View("Edit", Cus);
        }
        #endregion

	}
}