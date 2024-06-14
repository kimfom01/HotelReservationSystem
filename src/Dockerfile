FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /src

COPY ./Common/ ./Common/

COPY ./Reservations/ ./Reservations/

RUN dotnet restore ./Reservations/Presentation/HotelBackend.Reservations.ReservationService/HotelBackend.Reservations.ReservationService.csproj

RUN dotnet publish ./Reservations/Presentation/HotelBackend.Reservations.ReservationService/HotelBackend.Reservations.ReservationService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=base /app/publish .

ENTRYPOINT ["dotnet", "HotelBackend.Reservations.ReservationService.dll"]