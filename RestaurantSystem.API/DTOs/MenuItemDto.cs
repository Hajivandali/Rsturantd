using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class MenuItemDto
    {
        public long Id { get; set; }
        public long? MenuReference { get; set; }
        public long? ProductReference { get; set; }
        public bool IsActive { get; set; }
        public MenuDto? Menu { get; set; }
        public ProductDto? Product { get; set; }
    }

    public class CreateMenuItemDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "MenuReference must be greater than 0")]
        public long MenuReference { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "ProductReference must be greater than 0")]
        public long ProductReference { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateMenuItemDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "MenuReference must be greater than 0")]
        public long MenuReference { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "ProductReference must be greater than 0")]
        public long ProductReference { get; set; }

        public bool IsActive { get; set; }
    }
}
