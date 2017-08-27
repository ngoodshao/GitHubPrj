using BankProgram.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BankProgram.HtmlHelpers
{
    public static class PagingHelpers
    {

        public static MvcHtmlString PageForLinks(this HtmlHelper html,PageInfo pageinfo,Func<int,string> pageUrls)
        {
            StringBuilder htmlhelp = new StringBuilder();
            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");

            TagBuilder tagLiL = new TagBuilder("li");
            TagBuilder tagAL = new TagBuilder("a");
            tagAL.MergeAttribute("href", pageUrls(pageinfo.CurrentPage == 1 ? 1 : (pageinfo.CurrentPage - 1)));
            if (pageinfo.CurrentPage == 1)
            {
                tagLiL.AddCssClass("disabled");
            }
            tagAL.InnerHtml = "<";//"&larr;";
            tagLiL.InnerHtml = tagAL.ToString();
            htmlhelp.Append(tagLiL.ToString());

            for (int i = 1; i <= pageinfo.TotalPages; i++)
            {
                TagBuilder tagLi = new TagBuilder("li");
                TagBuilder tagA = new TagBuilder("a");
                tagA.MergeAttribute("href", pageUrls(i));
                tagA.InnerHtml = i.ToString();
                if (i == pageinfo.CurrentPage)
                {
                    tagLi.AddCssClass("active");
                }
                tagLi.InnerHtml = tagA.ToString();
                htmlhelp.Append(tagLi.ToString());
            }

            TagBuilder tagLiR = new TagBuilder("li");
            TagBuilder tagAR = new TagBuilder("a");
            tagAR.MergeAttribute("href", pageUrls(pageinfo.CurrentPage == pageinfo.TotalPages ? pageinfo.CurrentPage : (pageinfo.CurrentPage + 1)));
            tagAR.InnerHtml = ">";//"&rarr;";
            if (pageinfo.CurrentPage == pageinfo.TotalPages)
            {
                tagLiR.AddCssClass("disabled");
            }
            tagLiR.InnerHtml = tagAR.ToString();
            htmlhelp.Append(tagLiR.ToString());

            tagUl.InnerHtml = htmlhelp.ToString();
            
            return MvcHtmlString.Create(tagUl.ToString());
        }


        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}