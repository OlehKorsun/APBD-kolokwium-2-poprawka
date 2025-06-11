using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class CharacterService : ICharacterService
{
    
    private readonly ApbdDbContext _context;

    public CharacterService(ApbdDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<CharacterDTO> GetCharacterByIdAsync(int characterId)
    {

        var character = await _context.Characters.FindAsync(characterId);
        if (character == null)
        {
            throw new KeyNotFoundException($"Nie znaleziono bohatera z id: {characterId}");
        }
        
        var characters = await _context.Characters
            .Include(b => b.Backpacks)
            .ThenInclude(i => i.Item)
            .Include(ct => ct.CharacterTitles)
            .ThenInclude(t => t.Title).Where(c => c.CharacterId == characterId).ToListAsync();


        var result = characters.Select(c => new CharacterDTO()
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            CurrentWeight = c.CurrentWeight,
            MaxWeight = c.MaxWeight,
            BackpackItems = c.Backpacks.Select(b => new BackpackItemDTO()
            {
                Amount = b.Amount,
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight
            }).ToList(),
            Titles = c.CharacterTitles.Select(t => new TitleDTO()
            {
                Title = t.Title.Name,
                AquiredAt = t.AcquiredAt
            }).ToList()
        }).FirstOrDefault();

        if (result == null)
        {
            result = new CharacterDTO()
            {
                FirstName = character.FirstName,
                LastName = character.LastName,
                CurrentWeight = character.CurrentWeight,
                MaxWeight = character.MaxWeight,
                BackpackItems = new List<BackpackItemDTO>(),
                Titles = new List<TitleDTO>(),
            };
        }
        return result;
    }



    public async Task PostItemsAsync(ItemsRequest itemsRequest, int characterId)
    {
        var charachter = await _context.Characters.FindAsync(characterId);
        if (charachter == null)
        {
            throw new KeyNotFoundException("Nie znaleziono bohatera z id: " + characterId);
        }

        int weight = charachter.CurrentWeight;
        
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var id in itemsRequest.Ids)
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    throw new KeyNotFoundException("Nie znaleziono przedmiotu z id: " + id);
                }

                weight += item.Weight;
                if (weight > charachter.MaxWeight)
                {
                    throw new WeightLimitException("Przekroczono limit wagi bohatera!");
                }
                var backpack = await _context.Backpacks.FirstOrDefaultAsync(b => b.CharacterId == characterId && b.ItemId == id);
                if (backpack == null)
                {
                    backpack = new Backpack()
                    {
                        CharacterId = characterId,
                        Amount = 1,
                        ItemId = id
                    };
                    await _context.Backpacks.AddAsync(backpack);
                }
                else
                {
                    backpack.Amount++;
                }
            }
            charachter.CurrentWeight = weight;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch 
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
}