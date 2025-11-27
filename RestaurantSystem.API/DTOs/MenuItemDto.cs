using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class MenuItemDto
{
    public long Id { get; set; }
    public long MenuId { get; set; }     
    public long ProductId { get; set; }      
    public bool IsActive { get; set; }
    public MenuDto? Menu { get; set; }
    public ProductDto? Product { get; set; }
}

public class CreateMenuItemDto
{
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "MenuId must be greater than 0")]
    public long MenuId { get; set; }

    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
    public long ProductId { get; set; }

    public bool IsActive { get; set; } = true;
}

public class UpdateMenuItemDto
{
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "MenuId must be greater than 0")]
    public long MenuId { get; set; }

    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
    public long ProductId { get; set; }

    public bool IsActive { get; set; }
}

}
