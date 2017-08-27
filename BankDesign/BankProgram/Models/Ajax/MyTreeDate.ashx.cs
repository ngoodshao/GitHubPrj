using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;

namespace BankProgram.Models.Ajax
{
    /// <summary>
    /// MyTreeDate 的摘要说明
    /// </summary>
    public class MyTreeDate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            List<MyTreeNode> treeNodeList = CreateNode();
            context.Response.Write(new JavaScriptSerializer().Serialize(treeNodeList));
        }
        public List<MyTreeNode> CreateNode()
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("id", typeof(int));
            //dt.Columns.Add("pid", typeof(int));
            //dt.Columns.Add("text", typeof(string));
            //dt.Columns.Add("url", typeof(string));
            //dt.Columns.Add("isLeaf", typeof(bool));
            //dt.Columns.Add("isexpand", typeof(bool));
            //dt.Columns.Add("delay", typeof(int));
            /*
             * 1. 数据源字段如果和树中不一样，则要在树中写上 nodename=columnName
             * 2. 数据源可以是列表，可以是表，可以是数组，但是id、pid要对应
             */
            List<MyTreeNode> msterNode = new List<MyTreeNode>();
            MyTreeNode my1 = new MyTreeNode()
            {
                id = 1,
                pid = 0,
                text = "主表管理",
                isLeaf = true,
                url = "",
                isexpand = true,
                delay = 2,
                iconClsFieldName = "bluebook"
            };
            msterNode.Add(my1);
            MyTreeNode my10 = new MyTreeNode()
            {
                id = 11,
                pid = 1,
                text = "用户管理",
                isLeaf = false,
                url = "/MUser/Query",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "bookpen"
            };
            msterNode.Add(my10);
            MyTreeNode my11 = new MyTreeNode()
            {
                id = 11,
                pid = 1,
                text = "班时间管理",
                isLeaf = false,
                url = "/MShiftTime/Query",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "bookpen"
            };
            msterNode.Add(my11);
            MyTreeNode my12 = new MyTreeNode()
            {
                id = 12,
                pid = 1,
                text = "生产计划管理",
                isLeaf = false,
                url = "/TProductionPlan/Query",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "greenwarn"
            };
            msterNode.Add(my12);
            MyTreeNode my13 = new MyTreeNode()
            {
                id = 13,
                pid = 1,
                text = "状态管理",
                isLeaf = false,
                url = "http://localhost:39854/Master/ListPage",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "greenwarn"
            };
            msterNode.Add(my13);

            MyTreeNode my2 = new MyTreeNode()
            {
                id = 2,
                pid = 0,
                text = "实时显示",
                isLeaf = true,
                url = "",
                isexpand = true,
                delay = 2,
                iconClsFieldName = "greenwarn"
            };
            msterNode.Add(my2);
            MyTreeNode my21 = new MyTreeNode()
            {
                id = 21,
                pid = 2,
                text = "稼动率查看",
                isLeaf = false,
                url = "#",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "refresh"
            };
            msterNode.Add(my21);
            MyTreeNode my22 = new MyTreeNode()
            {
                id = 22,
                pid = 2,
                text = "生产实际查看",
                isLeaf = false,
                url = "#",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "save"
            };
            msterNode.Add(my22);

            MyTreeNode my3 = new MyTreeNode()
            {
                id = 3,
                pid = 0,
                text = "统计报表",
                isLeaf = true,
                url = "",
                isexpand = true,
                delay = 2,
                iconClsFieldName = "modify"
            };
            msterNode.Add(my3);
            MyTreeNode my31 = new MyTreeNode()
            {
                id = 31,
                pid = 3,
                text = "生产实际统计",
                isLeaf = false,
                url = "#",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "ok"
            };
            msterNode.Add(my31);
            MyTreeNode my32 = new MyTreeNode()
            {
                id = 32,
                pid = 3,
                text = "稼动率统计",
                isLeaf = false,
                url = "#",
                isexpand = false,
                delay = 2,
                iconClsFieldName = "mailbox"
            };
            msterNode.Add(my32);


            return msterNode;
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