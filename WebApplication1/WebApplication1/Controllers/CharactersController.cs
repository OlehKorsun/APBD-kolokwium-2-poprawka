using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }


    [HttpGet("{characterId}")]

    public async Task<IActionResult> GetCharacterAsync(int characterId)
    {
        try
        {
            var result = await _characterService.GetCharacterByIdAsync(characterId);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> PostItemsAsync([FromBody]ItemsRequest itemsRequest, int characterId)
    {
        try
        {
            await _characterService.PostItemsAsync(itemsRequest, characterId);
            return Created();
        }
        catch (WeightLimitException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}