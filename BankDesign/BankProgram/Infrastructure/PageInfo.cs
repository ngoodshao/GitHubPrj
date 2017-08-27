using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProgram.Infrastructure
{
    public class PageInfo
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 分页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / PageSize);
            }
        }

    }
}