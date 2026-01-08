using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinorFeature.Web.Models;
using MinorFeature.BLL.Manage;
using MinorFeature.Model;
using MinorFeature.BLL;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MinorFeature.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 授权访问的Cookie名称
        /// </summary>
        private const string authCookie = "AuthCookie";
        //private readonly IAdminUserManage userManage;

        //public HomeController(IAdminUserManage userManage)
        //{
        //    this.userManage = userManage;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// 访问登录页面
        /// </summary>
        /// <returns>登录页面</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel">用户名：登录账号|邮箱地址，登录密码</param>
        /// <param name="returnUrl">返回地址</param>
        /// <returns>登录页面或管理主页</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]// 防跨站请求伪造（必加）
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(loginModel);
            var loginResult = new ProcessResult<AdminUser>()
            {
                RCode = BLL.StatusCode.Success,
                ResultData = new AdminUser()
                {
                    Id=1,
                    LgAccount= "MF_Admin",
                    Email="admin@qq.com",
                    NickName= "管理员",
                    Gender="M"
                }
            };//userManage.LoginUser(loginModel.UserName, loginModel.PassWord);
            AdminUser user = loginResult.ResultData;
            if (loginResult.RCode == BLL.StatusCode.Success)
            {
                //用Claim来构造一个ClaimsIdentity，然后调用 SignInAsync 方法。
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.LgAccount),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.NickName),
                    new Claim(ClaimTypes.Gender, user.Gender)
                };
                var claimsIdentity = new ClaimsIdentity(claims, authCookie);
                DateTimeOffset? exUtc = null;
                if (loginModel.RememberMe)
                    exUtc = DateTimeOffset.UtcNow.AddDays(7);
                // 3. 配置Cookie属性（核心：记住我控制Expires）
                var authProperties = new AuthenticationProperties
                {
                    // 记住我：设置持久Cookie的过期时间
                    IsPersistent = loginModel.RememberMe,
                    ExpiresUtc = exUtc,
                    // 滑动过期：每次访问刷新有效期（可选）
                    AllowRefresh = true
                };
                await HttpContext.SignInAsync(authCookie, new ClaimsPrincipal(claimsIdentity), authProperties);
                if(!string.IsNullOrWhiteSpace(returnUrl))// 登录成功，跳转回原请求页
                    return LocalRedirect(returnUrl);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError(loginResult.RCode.ToString(), loginResult.Message);
            }
            // 模型验证失败，返回登录页
            return View(loginModel);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns>重定向首页</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(authCookie);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
