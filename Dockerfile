# 阶段1：构建项目（使用官方 .NET SDK 镜像，包含编译工具和 NuGet 环境）
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# 设置工作目录
WORKDIR /src

# 第一步：复制项目文件（.csproj），先还原依赖（利用 Docker 缓存，加快后续构建）
COPY *.csproj ./
# 还原 NuGet 依赖（Render 会自动联网下载，若有私有源需额外配置）
RUN dotnet restore

# 第二步：复制项目所有文件（除了 .dockerignore 排除的文件）
COPY . ./

# 第三步：编译发布项目（Release 版本，输出到 out 目录）
RUN dotnet publish -c Release -o out

# 阶段2：运行项目（使用官方 .NET 运行时镜像，体积更小，运行更高效）
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# 设置工作目录
WORKDIR /app

# 从构建阶段复制编译产物（out 目录）到运行阶段的 app 目录
COPY --from=build /src/out ./

# 关键配置：读取 Render 自动分配的 PORT 环境变量（适配 Render 端口要求）
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE ${PORT}

# 启动命令：替换为你的项目名.dll（与 .csproj 文件名一致，无 .csproj 后缀）
ENTRYPOINT ["dotnet", "YourProjectName.dll"]