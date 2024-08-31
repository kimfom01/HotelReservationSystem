var builder = DistributedApplication.CreateBuilder(args);

var rabbitmqUser = builder.AddParameter("rabbitmq-user", secret: true);
var rabbitmqPassword = builder.AddParameter("rabbitmq-password", secret: true);
var rabbitmq = builder.AddRabbitMQ("rabbitmq", rabbitmqUser, rabbitmqPassword)
    .WithHealthCheck()
    .WithEndpoint(15672, 15672, scheme:"http", name: "rmqManagement")
    .WithImage("masstransit/rabbitmq");

var postgresUser = builder.AddParameter("postgres-user", secret: true);
var postgresPassword = builder.AddParameter("postgres-password", secret: true);
var postgres = builder.AddPostgres("postgres", postgresUser, postgresPassword, port: 5432)
    .WithPgAdmin()
    .WithDataVolume("hrs-db-volume")
    .WithHealthCheck()
    .WithEnvironment("POSTGRES_DB", "hrs-db")
    .AddDatabase("hrs-db");

var mailpit = builder.AddContainer("mailpit", "axllent/mailpit")
    .WithEndpoint(targetPort: 1025, name: "mailpit-host", port: 1025)
    .WithHttpEndpoint(targetPort: 8025, name: "mailpit-client", port: 8025)
    .GetEndpoint("mailpit-host");

var emailSender = builder.AddParameter("EmailOptionsSenderEmail", secret: true);
var emailPassword = builder.AddParameter("EmailOptionsPassword", secret: true);
var emailHost = builder.AddParameter("EmailOptionsHost", secret: true);
var emailPort = builder.AddParameter("EmailOptionsPort", secret: true);

var jwtKey = builder.AddParameter("JwtConfigOptionsKey", secret: true);
var jwtIssuer = builder.AddParameter("JwtConfigOptionsIssuer", secret: true);
var jwtAudience = builder.AddParameter("JwtConfigOptionsAudience", secret: true);
var jwtExpiresIn = builder.AddParameter("JwtConfigOptionsExpiresIn", secret: true);

builder.AddProject<Projects.Hrs_Presentation>("hrs-backend")
    .WithReference(rabbitmq)
    .WithReference(postgres)
    .WithReference(mailpit)
    .WithEnvironment("EmailOptions__SenderEmail", emailSender)
    .WithEnvironment("EmailOptions__Password", emailPassword)
    .WithEnvironment("EmailOptions__Host", emailHost)
    .WithEnvironment("EmailOptions__Port", emailPort)
    .WithEnvironment("JwtConfigOptions__Key", jwtKey)
    .WithEnvironment("JwtConfigOptions__Issuer", jwtIssuer)
    .WithEnvironment("JwtConfigOptions__Audience", jwtAudience)
    .WithEnvironment("JwtConfigOptions__ExpiresIn", jwtExpiresIn)
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(rabbitmq);

builder.Build().Run();
