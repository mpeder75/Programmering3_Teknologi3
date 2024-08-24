using ContosoPizza.Data;
using ContosoPizza.Models;

// Instans af DbContext
using ContosoPizzaDbContext context = new ContosoPizzaDbContext();

// --------------------- Oprette data i databasen ---------------------------------


// Product tabel får tilføjet data
Product veggieSpecial = new Product()
{
    Name = "Veggie Special Pizza",
    Price = 9.99M
};
context.Add(veggieSpecial);

Product meatLover = new Product()
{
    Name = "Meat Lover Intense",
    Price = 11.50M
};
context.Add(meatLover);


// Ændringer gemmes i database
context.SaveChanges();


// --------------------- Læse/query databasen ---------------------------------

// Hente data fra products hvor product price er større end 10
var products = context.Products.Where(p=> p.Price > 10).OrderBy(p => p.Name);

//
foreach (Product product in products)
{
    Console.WriteLine($"ID:      {product.Id}");
    Console.WriteLine($"Name:    {product.Name}");
    Console.WriteLine($"Price:   {product.Price}");
    Console.WriteLine(new string('-', 20));
}


// --------------------- Opdater en entity i databasen ---------------------------------
/*
// Query databasen for at finde det første produkt hvis nave indehgolder "veggieSpecial"
var veggieSpeciel = context.Products.Where(p => p.Name == "veggieSpecial").FirstOrDefault();

// Hvis veggieSpecial er et Product objekt, sættes prisen til 10.99
if (veggieSpeciel is Product)
{
    veggieSpeciel.Price = 10.99M;
}
context.SaveChanges();
*/
// --------------------- Delete en entity i databasen ---------------------------------

// Query databasen for at finde det første produkt hvis nave indehgolder "veggieSpecial"
var veggieSpeciel = context.Products.Where(p => p.Name == "veggieSpecial").FirstOrDefault();

// Hvis veggieSpecial er et Product objekt, slettes det
if (veggieSpeciel is Product)
{
    context.Remove(veggieSpeciel);
}
context.SaveChanges();