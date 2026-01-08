# 阶段1：构建项目（使用 .NET Core 2.2 SDK 镜像，用于编译和还原依赖）
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
# 设置工作目录
WORKDIR /src

# 第一步：复制项目文件（.csproj），先还原依赖（利用 Docker 缓存，加快后续构建）
COPY MinorFeature.Tool/MinorFeature.Tool.csproj MinorFeature.Tool/
COPY MinorFeature.Model/MinorFeature.Model.csproj MinorFeature.Model/
COPY MinorFeature.DAL/MinorFeature.DAL.csproj MinorFeature.DAL/
COPY MinorFeature.BLL/MinorFeature.BLL.csproj MinorFeature.BLL/
COPY MinorFeature.Web/MinorFeature.Web.csproj MinorFeature.Web/
#COPY *.csproj ./
# 还原 NuGet 依赖（Render 会自动联网下载，若有私有源需额外配置）
WORKDIR /src/MinorFeature.Web
RUN dotnet restore

# 第二步：复制项目所有文件（除了 .dockerignore 排除的文件）
WORKDIR /src
COPY MinorFeature.Tool/ MinorFeature.Tool/
COPY MinorFeature.Model/ MinorFeature.Model/
COPY MinorFeature.DAL/ MinorFeature.DAL/
COPY MinorFeature.BLL/ MinorFeature.BLL/
COPY MinorFeature.Web/ MinorFeature.Web/

# 第三步：编译发布项目（Release 版本，输出到 out 目录）
RUN dotnet publish MinorFeature.Web/MinorFeature.Web.csproj -c Release -o /src/out --no-restore

# 阶段2：运行项目（使用 .NET Core 2.2 ASP.NET Core 运行时镜像，体积更小）
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
# 设置工作目录
WORKDIR /app

# 从构建阶段复制编译产物（out 目录）到运行阶段的 app 目录
COPY --from=build /src/out ./

# 关键配置1：暴露端口（Render 会自动映射，此处先固定一个基础端口，后续代码适配动态 PORT）
EXPOSE 80
EXPOSE 8080

# 先打印目录内容（关键：验证 dll 是否存在，Runtime logs 会显示此输出）
RUN ls -la /app

# 启动命令：替换为你的项目名.dll（与 .csproj 文件名一致，无 .csproj 后缀）
ENTRYPOINT ["dotnet", "/app/MinorFeature.Web.dll"]