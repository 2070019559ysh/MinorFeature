using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinorFeature.Model
{
    /// <summary>
    /// 管理平台访问用户
    /// </summary>
    [Table("AdminUser")]
    public class AdminUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 用户设置登录账号
        /// </summary>
        [Required]
        [StringLength(255)]
        public string LgAccount { get; set; }
        /// <summary>
        /// 需要验证过的邮箱
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        /// <summary>
        /// 用户加密登录密码
        /// </summary>
        [Required]
        [StringLength(32)]
        public string PasswordHash { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [StringLength(255)]
        public string NickName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(1)]
        public string Gender { get; set; }
        /// <summary>
        /// 性别枚举
        /// </summary>
        [NotMapped]
        public Sex SexValue {
            get
            {
                switch (Gender?.ToUpper())
                {
                    case "M": return Sex.Male;
                    case "F": return Sex.Female;
                    default: return Sex.UnKnow;
                }
            }
            set
            {
                switch (value)
                {
                    case Sex.Male: Gender = "M"; break;
                    case Sex.Female: Gender = "F"; break;
                    default: Gender = "0"; break; // 可以使用'0'表示未知，或者在数据库中使用NULL或特定值表示未知
                }
            }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        public string Phone { get; set; }
        /// <summary>
        /// -1=被注销，0=正常，1=拒绝访问
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
    /// <summary>
    /// 定义人的性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 男
        /// </summary>
        Male = 'M',
        /// <summary>
        /// 女
        /// </summary>
        Female = 'F',
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow = '0'
    }
    /// <summary>
    /// 用户状态枚举
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 被注销
        /// </summary>
        LogOut = -1,
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 拒绝访问
        /// </summary>
        Refuse
    }
}
