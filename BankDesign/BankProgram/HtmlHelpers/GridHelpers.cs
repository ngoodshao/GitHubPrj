using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BankProgram.HtmlHelpers
{
    public static class GridHelpers
    {
        /// <summary>
        /// 生成Grid标签元素
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageloadid">加载标识ID</param>
        /// <param name="gridid">Grid ID</param>
        /// <returns></returns>
        public static MvcHtmlString GridFor(this HtmlHelper html,string pageloadid,string gridid)
        {
            StringBuilder htmlhelp = new StringBuilder();
            TagBuilder tagdiv1 = new TagBuilder("div");
            tagdiv1.AddCssClass("l-loading");
            tagdiv1.Attributes.Add("style", "display:block");
            tagdiv1.GenerateId(pageloadid);//"pageloading"
            htmlhelp.Append(tagdiv1.ToString());

            TagBuilder tagdiv2 = new TagBuilder("div");
            tagdiv2.Attributes.Add("style", "margin: 0; padding: 0");
            tagdiv2.GenerateId(gridid);//"maingrid4"
            htmlhelp.Append(tagdiv2.ToString());

            TagBuilder tagdiv3 = new TagBuilder("div");
            tagdiv3.Attributes.Add("style", "display: none;");
            //tagdiv3.GenerateId(id);
            htmlhelp.Append(tagdiv3.ToString());

            return MvcHtmlString.Create(htmlhelp.ToString());
            //<div class="l-loading" style="display: block" id="pageloading"></div>
            //<div id="maingrid4" style="margin: 0; padding: 0"></div>
            //<div style="display: none;"></div>
        }
    }
}