using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class CustomerInvoiceItemDto
    {
        public long Id { get; set; }
        public int CountProduct { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalPrice { get; set; }
        public long InvoiceReference { get; set; }
        public long ProductId { get; set; }
        public CustomerInvoiceDto? Invoice { get; set; }
        public ProductDto? Product { get; set; }
    }

    public class CreateCustomerInvoiceItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CountProduct must be greater than 0")]
        public int CountProduct { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fee must be greater than 0")]
        public decimal Fee { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "InvoiceReference must be greater than 0")]
        public long InvoiceReference { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
        public long ProductId { get; set; }
    }

    public class UpdateCustomerInvoiceItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CountProduct must be greater than 0")]
        public int CountProduct { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fee must be greater than 0")]
        public decimal Fee { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal TotalPrice { get; set; }
    }
}
