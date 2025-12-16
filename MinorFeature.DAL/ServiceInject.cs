using Microsoft.Extensions.DependencyInjection;
using MinorFeature.DAL.Services;
using System;

namespace MinorFeature.DAL
{
    /// <summary>
    /// 类库默认实现依赖服务注入
    /// </summary>
    public class ServiceInject
    {
        /// <summary>
        /// 进行默认实现的依赖服务配置
        /// </summary>
        /// <param name="services">提供服务注入的集合</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAdminUserService, AdminUserService>();
        }
    }
}
