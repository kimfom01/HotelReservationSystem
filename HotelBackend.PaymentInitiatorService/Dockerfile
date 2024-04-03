FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /src

COPY HotelBackend.PaymentInitiatorService.csproj .

RUN dotnet restore HotelBackend.PaymentInitiatorService.csproj

COPY . .

RUN dotnet publish HotelBackend.PaymentInitiatorService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=base /app/publish .

ENTRYPOINT ["dotnet", "HotelBackend.PaymentInitiatorService.dll"]