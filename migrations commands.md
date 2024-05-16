# Commands to Generate Migrations

> > First make sure you are in the solution root directory

## To remove migrations

```sh
rm -rf **/**/Migrations/
```

## To generate migrations for Reservation

### Navigate to Reservations directory

```sh
cd ./src/Reservations
```

### Run the following to generage reservations migration

```sh
dotnet ef migrations add InitialCreate --project Infrastructure/HotelBackend.Reservations.Persistence/HotelBackend.Reservations.Persistence.csproj --context DatabaseContext --output-dir ./Migrations/
```

## To generate migrations for Payments

### Navigate to Payments directory

```sh
cd ./src/Payments
```

### Run the following to generage payments migration

```sh
dotnet ef migrations add InitialCreate --project Infrastructure/HotelBackend.Payments.Infrastructure/HotelBackend.Payments.Infrastructure.csproj --context PaymentDataContext --output-dir ./Migrations/
```
