using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MinorFeature.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            // 关键：读取 Render 分配的 PORT 环境变量，fallback 到 5000
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            var url = $"http://0.0.0.0:{port}"; // 必须绑定 0.0.0.0，否则 Render 无法访问

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(url); // 绑定动态端口
        }
    }
}
