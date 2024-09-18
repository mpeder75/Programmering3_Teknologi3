using Microsoft.AspNetCore.HttpLogging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// tilføjer AddHttpLoggin til services som er DI container/IoC container
builder.Services.AddHttpLogging(opts => 
    opts.LoggingFields = HttpLoggingFields.RequestProperties); 

// Konfigurerer logging systemet til kun at inkludere logs på Information niveau eller høje
builder.Logging.AddFilter( 
    "Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

WebApplication app = builder.Build();

// Tilføjer HTTP logging middleware, men kun i udviklingsmiljøet
if (app.Environment.IsDevelopment()) 
{
    app.UseHttpLogging(); // UseHttpLogging middleware tilføjes
}

// Opretter et HTTP GET endpoint på url'en "/"
app.MapGet("/", () => "Hello World!");

// Opretter et HTTP GET endpoint på url /person
// Når client laver GET request til /person, returner app en instans a Person
app.MapGet("/person", () => new Person("Andrew", "Lock")); 

app.Run();  // Starter applikationen

// Definerer en record type Person med to properties: FirstName og LastName
public record Person(string FirstName, string LastName);




