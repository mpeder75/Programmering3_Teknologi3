using Change_database_provider.Models;
using Change_database_provider.Data;


// Instans af DbContext
using ContosoPizzaContext context = new ContosoPizzaContext();

// --------------------- Oprette data i databasen ---------------------------------


// Product tabel får tilføjet data
Product fitnessSpecial = new Product()
{
    Name = "Fitness special",
    Price = 7.99M
};
context.Add(fitnessSpecial);

Product marhherita = new Product()
{
    Name = "Classic margherita",
    Price = 61.50M
};
context.Add(marhherita);
