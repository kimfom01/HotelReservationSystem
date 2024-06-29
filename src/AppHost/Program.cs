var builder = DistributedApplication.CreateBuilder(args);

var rabbitmqUser = builder.AddParameter("rabbitmq-user", secret: true);
var rabbitmqPassword = builder.AddParameter("rabbitmq-password", secret: true);
var rabbitmq = builder.AddRabbitMQ("rabbitmq", rabbitmqUser, rabbitmqPassword, port: 5672)
    .WithEndpoint(15672, 15672, scheme:"http", name: "rmqManagement")
    .WithImage("masstransit/rabbitmq");

var postgresUser = builder.AddParameter("postgres-user", secret: true);
var postgresPassword = builder.AddParameter("postgres-password", secret: true);
var postgres = builder.AddPostgres("postgres", postgresUser, postgresPassword, 5432)
    .WithEnvironment("POSTGRES_DB", "hoteldb")
    .AddDatabase("hoteldb");

var mailpit = builder.AddContainer("mailpit", "axllent/mailpit")
    .WithEndpoint(targetPort: 1025, name: "mailpit", port: 1025)
    .WithHttpEndpoint(targetPort: 8025, name: "mailpitclient", port: 8025)
    .GetEndpoint("mailpit");

var emailSender = builder.AddParameter("SenderEmail", secret: true);
var emailPassword = builder.AddParameter("Password", secret: true);
var emailHost = builder.AddParameter("Host", secret: true);
var emailPort = builder.AddParameter("Port", secret: true);

var jwtKey = builder.AddParameter("Key", secret: true);
var jwtIssuer = builder.AddParameter("Issuer", secret: true);
var jwtAudience = builder.AddParameter("Audience", secret: true);
var jwtExpiresIn = builder.AddParameter("ExpiresIn", secret: true);


builder.AddProject<Projects.Hrs_Presentation>("api")
    .WithReference(rabbitmq)
    .WithReference(postgres)
    .WithReference(mailpit)
    .WithEnvironment("SenderEmail", emailSender)
    .WithEnvironment("Password", emailPassword)
    .WithEnvironment("Host", emailHost)
    .WithEnvironment("Port", emailPort)
    .WithEnvironment("Key", jwtKey)
    .WithEnvironment("Issuer", jwtIssuer)
    .WithEnvironment("Audience", jwtAudience)
    .WithEnvironment("ExpiresIn", jwtExpiresIn)
    .WithReplicas(4)
    .WithExternalHttpEndpoints();

var app = builder.Build();

app.Run();