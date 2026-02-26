FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EFCore.MSSQL.API/EFCore.MSSQL.API.csproj", "EFCore.MSSQL.API/EFCore.MSSQL.API/"]
COPY ["EFCore.MSSQL.Core/EFCore.MSSQL.Core.csproj", "EFCore.MSSQL.Core/EFCore.MSSQL.Core/"]
COPY ["EFCore.MSSQL.Infrastructure/EFCore.MSSQL.Infrastructure.csproj", "EFCore.MSSQL.Infrastructure/EFCore.MSSQL.Infrastructure/"]
COPY ["EFCore.MSSQL.Shared/EFCore.MSSQL.Shared.csproj", "EFCore.MSSQL.Shared/EFCore.MSSQL.Shared/"]

RUN dotnet restore "./EFCore.MSSQL.API/EFCore.MSSQL.API/EFCore.MSSQL.API.csproj"

COPY . EFCore.MSSQL.API/
WORKDIR "/src/EFCore.MSSQL.API/EFCore.MSSQL.API"

RUN dotnet build "./EFCore.MSSQL.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EFCore.MSSQL.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCore.MSSQL.API.dll"]