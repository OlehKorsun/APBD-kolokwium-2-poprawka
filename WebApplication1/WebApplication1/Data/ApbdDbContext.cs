using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApbdDbContext : DbContext
{
    
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    
    protected ApbdDbContext()
    {
    }

    public ApbdDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item(){ItemId = 1, Name = "Item 1", Weight = 5},
            new Item(){ItemId = 2, Name = "Item 2", Weight = 1},
            new Item(){ItemId = 3, Name = "Item 3", Weight = 16},
        });


        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new Title(){TitleId = 1, Name = "Title 1"},
            new Title(){TitleId = 2, Name = "Title 2"},
            new Title(){TitleId = 3, Name = "Title 3"},
        });


        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new Character(){CharacterId = 1, FirstName = "John", LastName = "Doe", CurrentWeight = 5, MaxWeight = 100},
            new Character(){CharacterId = 2, FirstName = "Jane", LastName = "Queque", CurrentWeight = 1, MaxWeight = 250},
            new Character(){CharacterId = 3, FirstName = "Jinx", LastName = "Pipjpo", CurrentWeight = 16, MaxWeight = 100},
        });


        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new Backpack(){CharacterId = 1, ItemId = 1, Amount = 1},
            new Backpack(){CharacterId = 2, ItemId = 2, Amount = 2},
            new Backpack(){CharacterId = 3, ItemId = 3, Amount = 3},
        });


        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>()
        {
            new CharacterTitle(){CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2020-02-26")},
            new CharacterTitle(){CharacterId = 2, TitleId = 2, AcquiredAt = DateTime.Parse("2021-03-28")},
            new CharacterTitle(){CharacterId = 3, TitleId = 3, AcquiredAt = DateTime.Parse("2022-04-16")},
        });
    }

}