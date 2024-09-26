FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PaymentService.Presentation/PaymentService.Presentation.csproj", "src/PaymentService.Presentation/"]
COPY ["src/PaymentService.Application/PaymentService.Application.csproj", "src/PaymentService.Application/"]
COPY ["src/Hrs.Common/Hrs.Common.csproj", "src/Hrs.Common/"]
COPY ["src/PaymentService.Domain/PaymentService.Domain.csproj", "src/PaymentService.Domain/"]
COPY ["src/PaymentService.Infrastructure/PaymentService.Infrastructure.csproj", "src/PaymentService.Infrastructure/"]
COPY ["src/ServiceDefaults/ServiceDefaults.csproj", "src/ServiceDefaults/"]
RUN dotnet restore "src/PaymentService.Presentation/PaymentService.Presentation.csproj"
COPY . .
WORKDIR "/src/src/PaymentService.Presentation"
RUN dotnet build "PaymentService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PaymentService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.Presentation.dll"]
