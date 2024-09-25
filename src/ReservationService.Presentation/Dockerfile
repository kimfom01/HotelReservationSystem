FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ReservationService.Presentation/ReservationService.Presentation.csproj", "src/ReservationService.Presentation/"]
COPY ["src/ReservationService.Application/ReservationService.Application.csproj", "src/ReservationService.Application/"]
COPY ["src/Hrs.Common/Hrs.Common.csproj", "src/Hrs.Common/"]
COPY ["src/ReservationService.Domain/ReservationService.Domain.csproj", "src/ReservationService.Domain/"]
COPY ["src/ReservationService.Infrastructure/ReservationService.Infrastructure.csproj", "src/ReservationService.Infrastructure/"]
RUN dotnet restore "src/ReservationService.Presentation/ReservationService.Presentation.csproj"
COPY . .
WORKDIR "/src/src/ReservationService.Presentation"
RUN dotnet build "ReservationService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReservationService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReservationService.Presentation.dll"]
