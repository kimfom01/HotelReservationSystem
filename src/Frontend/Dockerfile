FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
COPY Web/Web.csproj Web/
RUN dotnet restore "Web/Web.csproj"
COPY . .
RUN dotnet publish "Web/Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]