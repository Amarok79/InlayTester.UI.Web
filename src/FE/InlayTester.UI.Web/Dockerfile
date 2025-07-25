# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["nuget.config", "."]
COPY ["src/InlayTester.UI.Web/InlayTester.UI.Web.csproj", "src/InlayTester.UI.Web/"]
COPY ["src/InlayTester.Domain/InlayTester.Domain.csproj", "src/InlayTester.Domain/"]
COPY ["src/InlayTester.Infrastructure.Gateway/InlayTester.Infrastructure.Gateway.csproj", "src/InlayTester.Infrastructure.Gateway/"]
COPY ["src/InlayTester.Infrastructure.Services/InlayTester.Infrastructure.Services.csproj", "src/InlayTester.Infrastructure.Services/"]
COPY ["src/InlayTester.UI.Web.Components/InlayTester.UI.Web.Components.csproj", "src/InlayTester.UI.Web.Components/"]
COPY ["src/InlayTester.UI.Web.Theme/InlayTester.UI.Web.Theme.csproj", "src/InlayTester.UI.Web.Theme/"]
COPY ["src/InlayTester.UI.Web.Shell.Abstractions/InlayTester.UI.Web.Shell.Abstractions.csproj", "src/InlayTester.UI.Web.Shell.Abstractions/"]
COPY ["src/InlayTester.UI.Web.Shell.Components/InlayTester.UI.Web.Shell.Components.csproj", "src/InlayTester.UI.Web.Shell.Components/"]
COPY ["src/InlayTester.UI.Web.Shell/InlayTester.UI.Web.Shell.csproj", "src/InlayTester.UI.Web.Shell/"]
COPY ["src/InlayTester.UI.Web.Users/InlayTester.UI.Web.Users.csproj", "src/InlayTester.UI.Web.Users/"]
COPY ["src/InlayTester.UI.Web.Validations/InlayTester.UI.Web.Validations.csproj", "src/InlayTester.UI.Web.Validations/"]
RUN dotnet restore "./src/InlayTester.UI.Web/InlayTester.UI.Web.csproj"
COPY . .
WORKDIR "/src/src/InlayTester.UI.Web"
RUN dotnet build "./InlayTester.UI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InlayTester.UI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app/bin
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InlayTester.UI.Web.dll"]
