using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LSH.EF.CodeFirst.DLL.Model
{
    public class M_User
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [Display(Name="用户编码")]
        public string UserID { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Display(Name = "用户名称")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Display(Name = "用户密码")]
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户权限 0:添加，1:删除，2:修改，3:查询;0,1,2...
        /// </summary>
        [Display(Name = "用户权限")]
        public string UserPermission { get; set; }
        /// <summary>
        /// 用户状态 0:NONE，1:激活，2:禁用
        /// </summary>
        [Display(Name = "用户状态")]
        public int UserState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
