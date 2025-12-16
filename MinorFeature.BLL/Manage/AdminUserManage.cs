using MinorFeature.DAL.Services;
using MinorFeature.Model;
using MinorFeature.Tool;
using MinorFeature.Tool.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MinorFeature.BLL.Manage
{
    /// <summary>
    /// 管理用户业务处理类
    /// </summary>
    public class AdminUserManage : IAdminUserManage
    {
        private readonly RedisHelper redisHelper;
        private readonly IAdminUserService userService;

        /// <summary>
        /// 实例化管理用户业务处理实例
        /// </summary>
        /// <param name="userService">依赖注入用户数据处理实例</param>
        public AdminUserManage(IAdminUserService userService)
        {
            redisHelper = new RedisHelper();
            this.userService = userService;
        }

        /// <summary>
        /// 新增管理用户
        /// </summary>
        /// <param name="user">管理用户</param>
        /// <returns>处理结果</returns>
        public ProcessResult AddAdminUser(AdminUser user)
        {
            if (string.IsNullOrWhiteSpace(user.LgAccount))
            {
                user.LgAccount = Guid.NewGuid().ToString("N");//创建用户唯一登录号
            }
            else
            {
                user.LgAccount = Regex.Replace(user.LgAccount, @"\s+", "");//去掉多余空白字符
            }
            ProcessResult result = new ProcessResult();
            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                result.RCode = StatusCode.NotEmpty;
                result.Message = "用户密码不能为空";
                return result;
            }
            if (userService.ExistsUser(user.LgAccount))
            {
                result.RCode = StatusCode.Exists;
                result.Message = "用户账号已经存在";
                return result;
            }
            ProcessResult checkResult = CheckEmail(user.Email);//验证邮箱账号
            if (checkResult.RCode != StatusCode.Success) return checkResult;
            user.PasswordHash = EncryptHelper.HashMD532(user.PasswordHash);//密码加密保存
            user.Status = (int)UserStatus.Normal;
            user.CreateTime = DateTime.Now;
            userService.AddAdminUser(user);
            result.RCode = StatusCode.Success;
            result.Message = "成功";
            return result;
        }

        /// <summary>
        /// 查找指定账号是否存在
        /// </summary>
        /// <param name="lgAccount">唯一的用户设置账号</param>
        /// <returns>是否存在</returns>
        public ProcessResult<bool> ExistsUser(string lgAccount)
        {
            ProcessResult<bool> result = new ProcessResult<bool>();
            if (string.IsNullOrWhiteSpace(lgAccount))
            {
                result.RCode = StatusCode.NotEmpty;
                result.Message = "用户设置账号不能为空";
            }
            lgAccount = Regex.Replace(lgAccount, @"\s+", "");//去掉多余空白字符
            result.ResultData = userService.ExistsUser(lgAccount);
            result.RCode = StatusCode.Success;
            result.Message = "查询成功";
            return result;
        }

        /// <summary>
        /// 查找指定邮箱账号是否存在
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <param name="userStatus">指定用户状态</param>
        /// <returns>是否存在</returns>
        public ProcessResult<bool> ExistsUser(string email, UserStatus userStatus = UserStatus.Normal)
        {
            ProcessResult checkResult = CheckEmail(email);//验证邮箱账号
            if (checkResult.RCode != StatusCode.Success) return checkResult;
            ProcessResult<bool> result = new ProcessResult<bool>();
            result.ResultData = userService.ExistsUser(email, userStatus);
            result.RCode = StatusCode.Success;
            result.Message = "查询成功";
            return result;
        }
        /// <summary>
        /// 用户登录的验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>登录验证结果</returns>
        public ProcessResult<AdminUser> LoginUser(string userName,string passWord)
        {
            ProcessResult<AdminUser> loginResult=new ProcessResult<AdminUser>();
            if (string.IsNullOrWhiteSpace(userName))
            {
                loginResult.RCode = StatusCode.NotEmpty;
                loginResult.Message = "用户名不能为空";
                return loginResult;
            }
            if (string.IsNullOrWhiteSpace(passWord))
            {
                loginResult.RCode = StatusCode.NotEmpty;
                loginResult.Message = "登录密码不能为空";
                return loginResult;
            }
            userName = Regex.Replace(userName, @"\s+", "");//去掉多余空白字符
            passWord = EncryptHelper.HashMD532(passWord);//验证MD5的值
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            AdminUser adminUser;
            if (Regex.IsMatch(userName, pattern))//邮箱登录
            {
                adminUser = userService.GetUserByEmail(userName);
            }
            else //账号登录
            {
                adminUser = userService.GetAdminUser(userName);
            }
            if(adminUser is null)
            {
                loginResult.RCode = StatusCode.NotFound;
                loginResult.Message = "账号不存在";
                return loginResult;
            }
            else if (!adminUser.PasswordHash.Equals(passWord,StringComparison.CurrentCultureIgnoreCase))
            {
                loginResult.RCode = StatusCode.ParamError;
                loginResult.Message = "密码错误";
                return loginResult;
            }
            else if(!(adminUser.Status == (int)UserStatus.Normal))
            {
                loginResult.RCode = StatusCode.Refuse;
                loginResult.Message = "被拒绝登录";
                return loginResult;
            }
            loginResult.RCode = StatusCode.Success;
            loginResult.Message = "查询成功";
            loginResult.ResultData = adminUser;
            return loginResult;
        }
        /// <summary>
        /// 验证邮箱地址
        /// </summary>
        /// <param name="email">邮箱账号</param>
        /// <returns>验证结果</returns>
        private static ProcessResult CheckEmail(string email)
        {
            ProcessResult result = new ProcessResult();
            result.RCode = StatusCode.Success;//默认是成功
            if (string.IsNullOrWhiteSpace(email))
            {
                result.RCode = StatusCode.NotEmpty;
                result.Message = "邮箱账号不能为空";
            }
            email = Regex.Replace(email, @"\s+", "");//去掉多余空白字符
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
            {
                result.RCode = StatusCode.ParamError;
                result.Message = "邮箱格式不正确";
            }
            return result;
        }
    }
}
