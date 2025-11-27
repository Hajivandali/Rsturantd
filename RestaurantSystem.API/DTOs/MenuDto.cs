using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class MenuDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastEdited { get; set; }
        public List<MenuItemDto> MenuItems { get; set; } = new();
    }

    public class CreateMenuDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateMenuDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
    }
}
