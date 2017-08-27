using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSH.EF.CodeFirst.DLL.Model
{
    public class M_Permission
    {
        [Display(Name = "权限名称")]
        public string PermissionName { get; set; }
        [Display(Name = "权限编码")]
        public string PermissionID { get; set; }
        /// <summary>
        /// 用户权限 0:添加，1:删除，2:修改，3:查询，4:导入，5:导出
        /// </summary>
        [Display(Name = "用户权限")]
        public string PermissionIndex { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
