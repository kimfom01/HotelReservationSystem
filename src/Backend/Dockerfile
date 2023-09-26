FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
COPY Api/Api.csproj Api/
COPY DataAccess/DataAccess.csproj DataAccess/
RUN dotnet restore "Api/Api.csproj"
RUN dotnet restore "DataAccess/DataAccess.csproj"
COPY . .
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_URLS=http://+:5001
WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]