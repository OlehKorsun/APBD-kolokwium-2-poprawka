using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class ItemsRequest
{
    [Required]
    [MinLength(1)]
    public List<int> Ids { get; set; }
}