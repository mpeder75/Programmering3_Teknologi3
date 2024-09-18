using Microsoft.AspNetCore.HttpLogging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// tilf�jer AddHttpLoggin til services som er DI container/IoC container
builder.Services.AddHttpLogging(opts => 
    opts.LoggingFields = HttpLoggingFields.RequestProperties); 

// Konfigurerer logging systemet til kun at inkludere logs p� Information niveau eller h�je
builder.Logging.AddFilter( 
    "Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

WebApplication app = builder.Build();

// Tilf�jer HTTP logging middleware, men kun i udviklingsmilj�et
if (app.Environment.IsDevelopment()) 
{
    app.UseHttpLogging(); // UseHttpLogging middleware tilf�jes
}

// Opretter et HTTP GET endpoint p� url'en "/"
app.MapGet("/", () => "Hello World!");

// Opretter et HTTP GET endpoint p� url /person
// N�r client laver GET request til /person, returner app en instans a Person
app.MapGet("/person", () => new Person("Andrew", "Lock")); 

app.Run();  // Starter applikationen

// Definerer en record type Person med to properties: FirstName og LastName
public record Person(string FirstName, string LastName);




