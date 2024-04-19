FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /src
COPY HotelBackend.BackgroundConsumer.csproj .
RUN dotnet restore HotelBackend.BackgroundConsumer.csproj
COPY . .
RUN dotnet publish HotelBackend.BackgroundConsumer.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=base /app/publish .

ENTRYPOINT ["dotnet", "HotelBackend.BackgroundConsumer.dll"]
