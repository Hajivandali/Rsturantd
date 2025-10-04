using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string Images { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Unit { get; set; }
        public long InventoryItemID { get; set; }
    }

    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Product name can only contain letters, numbers, spaces, hyphens, and underscores")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Images are required")]
        [Url(ErrorMessage = "Images must be a valid URL")]
        public string Images { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Unit must be greater than 0")]
        public long Unit { get; set; }

        [Required(ErrorMessage = "Inventory Item ID is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Inventory Item ID must be greater than 0")]
        public long InventoryItemID { get; set; }
    }

    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Product name can only contain letters, numbers, spaces, hyphens, and underscores")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Images are required")]
        [Url(ErrorMessage = "Images must be a valid URL")]
        public string Images { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Unit must be greater than 0")]
        public long Unit { get; set; }

        [Required(ErrorMessage = "Inventory Item ID is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Inventory Item ID must be greater than 0")]
        public long InventoryItemID { get; set; }
    }
}
