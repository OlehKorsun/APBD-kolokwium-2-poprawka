using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Item")]
public class Item
{
    [Key]
    public int ItemId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public int Weight { get; set; }
    
    public ICollection<Backpack> Backpacks { get; set; }
}