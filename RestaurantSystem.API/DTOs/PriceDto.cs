using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class PriceDto
    {
        public long Id { get; set; }
        public long ProductReference { get; set; }
        public long Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEdited { get; set; }
        public ProductDto? Product { get; set; }
    }

    public class CreatePriceDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "ProductReference must be greater than 0")]
        public long ProductReference { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
    }

    public class UpdatePriceDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
    }
}
