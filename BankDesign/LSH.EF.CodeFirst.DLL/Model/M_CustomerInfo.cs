using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LSH.EF.CodeFirst.DLL.Model
{
    public class M_CustomerInfo
    {
        /// <summary>
        /// 客户编码
        /// </summary>
        [Display(Name = "客户编码")]
        public string CusCode { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        [Display(Name = "客户姓名")]
        public string CusName { get; set; }
        /// <summary>
        /// 客户性别
        /// </summary>
        [Display(Name = "客户性别")]
        public int Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string ContactNO { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        [Display(Name = "客户地址")]
        public string Address { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [Display(Name = "身份证号")]
        public string IDCard { get; set; }
        /// <summary>
        /// 紧急联系人信息
        /// </summary>
        [Display(Name = "紧急联系人信息")]
        public string EmergencyLink { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        public string AddOrUpdateUserID { get; set; }

        private DateTime _AddOrUpdateDate = DateTime.Now;
        public DateTime AddOrUpdateDate
        {
            set
            {
                _AddOrUpdateDate = value;
            }
            get
            {
                return _AddOrUpdateDate;
            }
        }
    }
}
