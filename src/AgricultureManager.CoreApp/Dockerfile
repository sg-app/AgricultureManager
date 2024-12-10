# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AgricultureManager.CoreApp/AgricultureManager.CoreApp.csproj", "AgricultureManager.CoreApp/"]
COPY ["AgricultureManager.Infrastructure.Persistence/AgricultureManager.Infrastructure.Persistence.csproj", "AgricultureManager.Infrastructure.Persistence/"]
COPY ["AgricultureManager.Core.Application/AgricultureManager.Core.Application.csproj", "AgricultureManager.Core.Application/"]
COPY ["AgricultureManager.Core.Application.Shared/AgricultureManager.Core.Application.Shared.csproj", "AgricultureManager.Core.Application.Shared/"]
COPY ["AgricultureManager.Core.Domain/AgricultureManager.Core.Domain.csproj", "AgricultureManager.Core.Domain/"]
COPY ["AgricultureManager.Module.Manager/AgricultureManager.Module.Manager.csproj", "AgricultureManager.Module.Manager/"]
COPY ["AgricultureManager.Module.Api/AgricultureManager.Module.Api.csproj", "AgricultureManager.Module.Api/"]
COPY ["AgricultureManager.SharedComponents/AgricultureManager.SharedComponents.csproj", "AgricultureManager.SharedComponents/"]
RUN dotnet restore "./AgricultureManager.CoreApp/AgricultureManager.CoreApp.csproj"
COPY . .
WORKDIR "/src/AgricultureManager.CoreApp"
RUN dotnet build "./AgricultureManager.CoreApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AgricultureManager.CoreApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgricultureManager.CoreApp.dll"]