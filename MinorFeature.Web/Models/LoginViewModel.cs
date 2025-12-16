using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinorFeature.Web.Models
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名：登录账号|邮箱地址
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(100, ErrorMessage = "用户名长度超过100字符")]
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "登录密码不能为空")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        /// <summary>
        /// 是否记住我
        /// </summary>
        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}
