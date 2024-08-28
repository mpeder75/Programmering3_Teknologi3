using System;
using System.Collections.Generic;
using Change_database_provider.Models;
using Microsoft.EntityFrameworkCore;

namespace Change_database_provider.Data;

public partial class ContosoPizzaContext : DbContext
{
    public ContosoPizzaContext()
    {
    }

    public ContosoPizzaContext(DbContextOptions<ContosoPizzaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data source=ContosoPizza;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
