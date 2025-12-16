using MinorFeature.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinorFeature.BLL.Manage
{
    /// <summary>
    /// 管理用户业务处理接口
    /// </summary>
    public interface IAdminUserManage
    {
        /// <summary>
        /// 新增管理用户
        /// </summary>
        /// <param name="user">管理用户</param>
        /// <returns>处理结果</returns>
        ProcessResult AddAdminUser(AdminUser user);
        /// <summary>
        /// 查找指定账号是否存在
        /// </summary>
        /// <param name="lgAccount">唯一的用户设置账号</param>
        /// <returns>是否存在</returns>
        ProcessResult<bool> ExistsUser(string lgAccount);
        /// <summary>
        /// 查找指定邮箱账号是否存在
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <param name="userStatus">指定用户状态</param>
        /// <returns>是否存在</returns>
        ProcessResult<bool> ExistsUser(string email, UserStatus userStatus = UserStatus.Normal);
        /// <summary>
        /// 用户登录的验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>登录验证结果</returns>
        ProcessResult<AdminUser> LoginUser(string userName, string passWord);
    }
}
