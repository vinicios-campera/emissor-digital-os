FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/api/Api/OrderService.Api.csproj", "src/api/Api/"]
COPY ["src/api/Infrastructure/Infrastructure.csproj", "src/api/Infrastructure/"]
COPY ["src/api/Application/Application.csproj", "src/api/Application/"]
COPY ["src/api/Domain/Domain.csproj", "src/api/Domain/"]

ARG GITHUB_USERNAME
ARG GITHUB_TOKEN
RUN sed -i "s|%GITHUB_USERNAME%|${GITHUB_USERNAME}|g" nuget.config && \
    sed -i "s|%GITHUB_TOKEN%|${GITHUB_TOKEN}|g" nuget.config

RUN dotnet restore "./src/api/Api/OrderService.Api.csproj"
COPY . .

WORKDIR "/src/src/api/Api"
RUN dotnet build "./OrderService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
LABEL org.opencontainers.image.source https://github.com/vinicios-campera/emissor-digital-os
ENTRYPOINT ["dotnet", "OrderService.Api.dll"]