FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /app

COPY HotelBackend.ReservationService.csproj .

RUN dotnet restore HotelBackend.ReservationService.csproj

COPY . .

RUN dotnet publish HotelBackend.ReservationService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=base /app/publish .

ENTRYPOINT ["dotnet", "HotelBackend.ReservationService.dll"]