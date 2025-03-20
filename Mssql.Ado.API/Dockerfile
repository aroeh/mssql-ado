FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Mssql.Ado.API/Mssql.Ado.API.csproj", "Mssql.Ado.API/Mssql.Ado.API/"]
COPY ["Mssql.Ado.Core/Mssql.Ado.Core.csproj", "Mssql.Ado.Core/Mssql.Ado.Core/"]
COPY ["Mssql.Ado.Infrastructure/Mssql.Ado.Infrastructure.csproj", "Mssql.Ado.Infrastructure/Mssql.Ado.Infrastructure/"]
COPY ["Mssql.Ado.Shared/Mssql.Ado.Shared.csproj", "Mssql.Ado.Shared/Mssql.Ado.Shared/"]

RUN dotnet restore "./Mssql.Ado.API/Mssql.Ado.API/Mssql.Ado.API.csproj"

COPY . Mssql.Ado.API/
WORKDIR "/src/Mssql.Ado.API/Mssql.Ado.API"

RUN dotnet build "./Mssql.Ado.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mssql.Ado.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mssql.Ado.API.dll"]