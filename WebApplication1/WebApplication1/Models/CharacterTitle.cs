using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;
[Table("Character_Title")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class CharacterTitle
{
    [Required]
    public DateTime AcquiredAt { get; set; }
    
    [ForeignKey(nameof(TitleId))]
    public Title Title { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; }
    
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
}