# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y \ 
	libfontconfig1 \ 
	libfreetype6 \ 
	libpng16-16 \ 
	libjpeg62-turbo \ 
	libwebp6 \ 
	libx11-6 \ 
	libxext6 \ 
	libxrender1 \ 
	libxcb1 \ 
	libx11-xcb1 \ 
	libxcomposite1 \ 
	libxcursor1 \ 
	libxdamage1 \ 
	libxfixes3 \ 
	libxi6 \ 
	libxtst6 \ 
	libxrandr2 \ 
	libasound2 \ 
	libatk1.0-0 \ 
	libatk-bridge2.0-0 \ 
	libcups2 \ 
	libdbus-1-3 \ 
	libgdk-pixbuf2.0-0 \ 
	libnspr4 \ 
	libnss3 \ 
	libpango-1.0-0 \ 
	libxshmfence1 \ 
	libgbm1 \ 
	libgtk-3-0

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

# Copy Modules
# Accounting Module
COPY ["AgricultureManager.Module.Accounting/AgricultureManager.Module.Accounting.csproj", "AgricultureManager.Module.Accounting/"]
RUN dotnet restore "./AgricultureManager.Module.Accounting/AgricultureManager.Module.Accounting.csproj"

# Pdf Module
COPY ["AgricultureManager.Module.Pdf/AgricultureManager.Module.Pdf.csproj", "AgricultureManager.Module.Pdf/"]
RUN dotnet restore "./AgricultureManager.Module.Pdf/AgricultureManager.Module.Pdf.csproj"

COPY . .
WORKDIR "/src/AgricultureManager.CoreApp"
RUN dotnet build "./AgricultureManager.CoreApp.csproj" -c $BUILD_CONFIGURATION -o /app/build
# Build Modules
WORKDIR "/src/AgricultureManager.Module.Accounting"
RUN dotnet build "./AgricultureManager.Module.Accounting.csproj" -c $BUILD_CONFIGURATION -o /app/plugins/accounting
WORKDIR "/src/AgricultureManager.Module.Pdf"
RUN dotnet build "./AgricultureManager.Module.Pdf.csproj" -c $BUILD_CONFIGURATION -o /app/plugins/pdf


# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/AgricultureManager.CoreApp"
RUN dotnet publish "./AgricultureManager.CoreApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
# Publish Modules
WORKDIR "/src/AgricultureManager.Module.Accounting"
RUN dotnet publish "./AgricultureManager.Module.Accounting.csproj" -c $BUILD_CONFIGURATION -o /app/plugins/accounting /p:UseAppHost=false
WORKDIR "/src/AgricultureManager.Module.Pdf"
RUN dotnet publish "./AgricultureManager.Module.Pdf.csproj" -c $BUILD_CONFIGURATION -o /app/plugins/pdf /p:UseAppHost=false



# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy Modules
COPY --from=publish /app/plugins/accounting ./plugins/accounting
COPY --from=publish /app/plugins/pdf ./plugins/pdf

ENTRYPOINT ["dotnet", "AgricultureManager.CoreApp.dll"]