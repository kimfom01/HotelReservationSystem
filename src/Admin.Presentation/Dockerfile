FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Admin.Presentation/Admin.Presentation.csproj", "src/Admin.Presentation/"]
COPY ["src/Admin.Application/Admin.Application.csproj", "src/Admin.Application/"]
COPY ["src/Hrs.Common/Hrs.Common.csproj", "src/Hrs.Common/"]
COPY ["src/Admin.Domain/Admin.Domain.csproj", "src/Admin.Domain/"]
COPY ["src/Admin.Infrastructure/Admin.Infrastructure.csproj", "src/Admin.Infrastructure/"]
COPY ["src/ServiceDefaults/ServiceDefaults.csproj", "src/ServiceDefaults/"]
RUN dotnet restore "src/Admin.Presentation/Admin.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Admin.Presentation"
RUN dotnet build "Admin.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Admin.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Admin.Presentation.dll"]
