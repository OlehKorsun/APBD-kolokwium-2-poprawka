using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface ICharacterService
{
    Task<CharacterDTO> GetCharacterByIdAsync(int characterId);
    Task PostItemsAsync(ItemsRequest itemsRequest, int characterId);
}