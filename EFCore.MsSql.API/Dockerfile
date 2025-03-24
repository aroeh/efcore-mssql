FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EFCore.MsSql.API/EFCore.MsSql.API.csproj", "EFCore.MsSql.API/EFCore.MsSql.API/"]
COPY ["EFCore.MsSql.Core/EFCore.MsSql.Core.csproj", "EFCore.MsSql.Core/EFCore.MsSql.Core/"]
COPY ["EFCore.MsSql.Infrastructure/EFCore.MsSql.Infrastructure.csproj", "EFCore.MsSql.Infrastructure/EFCore.MsSql.Infrastructure/"]
COPY ["EFCore.MsSql.Shared/EFCore.MsSql.Shared.csproj", "EFCore.MsSql.Shared/EFCore.MsSql.Shared/"]

RUN dotnet restore "./EFCore.MsSql.API/EFCore.MsSql.API/EFCore.MsSql.API.csproj"

COPY . EFCore.MsSql.API/
WORKDIR "/src/EFCore.MsSql.API/EFCore.MsSql.API"

RUN dotnet build "./EFCore.MsSql.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EFCore.MsSql.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCore.MsSql.API.dll"]