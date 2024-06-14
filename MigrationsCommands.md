# Commands to Generate Migrations

> > > First make sure you are in the solution root directory

## To remove migrations

```sh
rm -rf **/**/Migrations/
```

### Navigate to `Hrs.Infrastructure` directory

```sh
cd src/Hrs.Infrastructure/
```

### Run the following to generate reservations migration

```sh
dotnet ef migrations add InitialCreate --project Hrs.Infrastructure.csproj --context ReservationDataContext --output-dir ./Database/Migrations/Reservations
```

### Run the following to generate payments migration

```sh
dotnet ef migrations add InitialCreate --project Hrs.Infrastructure.csproj --context PaymentDataContext --output-dir ./Database/Migrations/Payments
```

### Run the following to generate admin migration

```sh
dotnet ef migrations add InitialCreate --project Hrs.Infrastructure.csproj --context AdminDataContext --output-dir ./Database/Migrations/Admin
```

### In a nutshell,

```sh
rm -rf **/**/Migrations/

cd src/Hrs.Infrastructure/

dotnet ef migrations add InitialCreate --project Hrs.Infrastructure.csproj --context ReservationDataContext --output-dir ./Database/Migrations/Reservations

dotnet ef migrations add InitialCreate --project Hrs.Infrastructure.csproj --context PaymentDataContext --output-dir ./Database/Migrations/Payments

dotnet ef migrations add InitialCreate --project Hrs.Infrastructure.csproj --context AdminDataContext --output-dir ./Database/Migrations/Admin
```
