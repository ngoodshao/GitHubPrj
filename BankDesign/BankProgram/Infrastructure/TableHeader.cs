using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProgram.Infrastructure
{
    public class TableHeader
    {
        //id	String	列ID	 
        //name	String	表格列名	 
        public string name { get; set; }
        public bool isAllowHide { get; set; }
        //totalSummary	Object	汇总	 
        //columns	Object	多表头支持	 
        //isAllowHide	Bool	是否允许隐藏,如果允许，将会出现在【显示/隐藏列右键菜单】	 
        //isSort	Bool	是否允许排序	 
        //type	String	排序类型,包括string、int、float、date	
        public string type;
        //width	Int	表格列宽度	
        public int width { get; set; }
        //minWidth	Int	表格列最小允许宽度(调整大小时将不允许小于这个值)	 
        public int minWidth { get; set; }
        public string format { get; set; }
        //format	String	格式化	 
        //align	String	左右对齐,left、right、center
        public string align { get; set; }
        public string hide { get; set; }
        //hide	String	初始化隐藏	 
        //editor	Obect	编辑器	 
        //render	Function	单元格渲染器	 
        public object render { get; set; }
        //display	String	表格列标题	 
        public string display { get; set; }
        //headerRender	Function	头部单元格渲染器(column)
        //frozen 是否冻结列 true false
        public bool frozen { get; set; }
    }
}