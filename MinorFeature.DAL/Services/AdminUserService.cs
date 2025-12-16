using MinorFeature.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinorFeature.DAL.Services
{
    /// <summary>
    /// 管理用户数据存储服务类
    /// </summary>
    public class AdminUserService : IAdminUserService
    {
        private readonly MFAdminDbContext _db;
        /// <summary>
        /// 实例化管理用户数据存储服务类
        /// </summary>
        /// <param name="db">依赖注入DB</param>
        public AdminUserService(MFAdminDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增管理用户
        /// </summary>
        /// <param name="user">管理用户</param>
        public void AddAdminUser(AdminUser user)
        {
            _db.AdminUsers.Add(user);
            _db.SaveChanges();
        }
        /// <summary>
        /// 查找指定账号是否存在
        /// </summary>
        /// <param name="lgAccount">库中唯一的用户设置账号</param>
        /// <returns>是否存在</returns>
        public bool ExistsUser(string lgAccount)
        {
            return _db.AdminUsers.Where(user => user.LgAccount.Equals(lgAccount)).Any();
        }
        /// <summary>
        /// 查找指定邮箱账号是否存在
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <param name="userStatus">指定用户状态</param>
        /// <returns>是否存在</returns>
        public bool ExistsUser(string email, UserStatus userStatus = UserStatus.Normal)
        {
            int normal = (int)userStatus;
            return _db.AdminUsers.Where(user => user.Email.Equals(email) && user.Status == normal).Any();
        }
        /// <summary>
        /// 获取管理用户信息
        /// </summary>
        /// <param name="lgAccount">库中唯一的用户设置账号</param>
        /// <returns>用户信息</returns>
        public AdminUser GetAdminUser(string lgAccount)
        {
            return _db.AdminUsers.Where(user => user.LgAccount.Equals(lgAccount)).FirstOrDefault();
        }
        /// <summary>
        /// 获取管理用户信息
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <param name="userStatus">指定用户状态</param>
        /// <returns>用户信息</returns>
        public AdminUser GetUserByEmail(string email, UserStatus userStatus = UserStatus.Normal)
        {
            int normal = (int)userStatus;
            return _db.AdminUsers.Where(user => user.Email.Equals(email) && user.Status == normal).FirstOrDefault();
        }
    }
}
