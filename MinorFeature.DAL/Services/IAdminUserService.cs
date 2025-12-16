using MinorFeature.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinorFeature.DAL.Services
{
    /// <summary>
    /// 管理用户数据存储服务接口
    /// </summary>
    public interface IAdminUserService
    {
        /// <summary>
        /// 新增管理用户
        /// </summary>
        /// <param name="user">管理用户</param>
        void AddAdminUser(AdminUser user);
        /// <summary>
        /// 查找指定账号是否存在
        /// </summary>
        /// <param name="lgAccount">库中唯一的用户设置账号</param>
        /// <returns>是否存在</returns>
        bool ExistsUser(string lgAccount);
        /// <summary>
        /// 查找指定邮箱账号是否存在
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <param name="userStatus">指定用户状态</param>
        /// <returns>是否存在</returns>
        bool ExistsUser(string email, UserStatus userStatus = UserStatus.Normal);
        /// <summary>
        /// 获取管理用户信息
        /// </summary>
        /// <param name="lgAccount">库中唯一的用户设置账号</param>
        /// <returns>用户信息</returns>
        AdminUser GetAdminUser(string lgAccount);
        /// <summary>
        /// 获取管理用户信息
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <param name="userStatus">指定用户状态</param>
        /// <returns>用户信息</returns>
        AdminUser GetUserByEmail(string email, UserStatus userStatus = UserStatus.Normal);
    }
}
