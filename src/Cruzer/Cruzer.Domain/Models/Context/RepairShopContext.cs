using Cruzer.Domain.Models.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cruzer.Domain.Models.Context;

internal class RepairShopContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<RepairOrder> RepairOrders { get; set; }
    
    public DbSet<Repair> Repairs { get; set; }
    
    public DbSet<Part> Parts { get; set; }
    
    public DbSet<Quote> Quotes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Cruze.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Part>().HasData(
            new Part { Id = 1, Name = "Brake Pads", StockNumber = "BP001", Price = 29.99M },
            new Part { Id = 2, Name = "Engine Oil", StockNumber = "OIL4Q", Price = 62.99M },
            new Part { Id = 3, Name = "Oil Filter", StockNumber = "OF001", Price = 15.99M },
            new Part { Id = 4, Name = "Tires (4)", StockNumber = "TR001", Price = 400.00M },
            new Part { Id = 5, Name = "Head Gasket", StockNumber = "HG001", Price = 35.00M },
            new Part { Id = 6, Name = "Radio", StockNumber = "RD001", Price = 350.00M },
            new Part { Id = 7, Name = "Transmission Rebuilt Kit", StockNumber = "TR001", Price = 375.00M },
            new Part { Id = 8, Name = "Muffler", StockNumber = "MFL001", Price = 400M }
            );

        modelBuilder.Entity<Repair>().HasData(
            new Repair { Id = 1, Code = "BS001", Category = "Maint", Name = "Break Service", Labor = 2.0M },
            new Repair { Id = 2, Code = "OC001", Category = "Maint", Name = "Oil Change", Labor = 0.5M },
            new Repair { Id = 3, Code = "TC001", Category = "Maint", Name = "Tire Change", Labor = 1.5M },
            new Repair { Id = 4, Code = "TRB01", Category = "Maint", Name = "Tire Rotation & Balance", Labor = 1.0M },
            new Repair { Id = 5, Code = "NG001", Category = "Engine", Name = "Replace head gasket", Labor = 1.5M },
            new Repair { Id = 6, Code = "RR001", Category = "Infotainment", Name = "Replace radio", Labor = 1.0M },
            new Repair { Id = 7, Code = "TR001", Category = "Trans", Name = "Transmission rebuilt", Labor = 6.0M },
            new Repair { Id = 8, Code = "MR001", Category = "Emissions", Name = "Muffle Replacement", Labor = 1.0M }
        );
        
        modelBuilder.Entity<Repair>()
            .HasMany(x => x.Parts)
            .WithMany(x => x.Repairs)
            .UsingEntity(x => x.HasData(
                new { RepairsId = 1, PartsId = 1 },
                new { RepairsId = 2, PartsId = 2 },
                new { RepairsId = 3, PartsId = 3 },
                new { RepairsId = 4, PartsId = 4 },
                new { RepairsId = 5, PartsId = 5 },
                new { RepairsId = 6, PartsId = 6 },
                new { RepairsId = 7, PartsId = 7 },
                new { RepairsId = 8, PartsId = 8 }
                ));
    }
}