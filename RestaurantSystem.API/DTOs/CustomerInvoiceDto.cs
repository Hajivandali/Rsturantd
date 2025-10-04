using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.API.DTOs
{
    public class CustomerInvoiceDto
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CustomerId { get; set; }
        public long TotalAmount { get; set; }
        public CustomerDto? Customer { get; set; }
        public List<CustomerInvoiceItemDto> InvoiceItems { get; set; } = new();
    }

    public class CreateCustomerInvoiceDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "CustomerId must be greater than 0")]
        public long CustomerId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public decimal TotalAmount { get; set; }

        public List<CreateCustomerInvoiceItemDto> InvoiceItems { get; set; } = new();
    }

    public class UpdateCustomerInvoiceDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "CustomerId must be greater than 0")]
        public long CustomerId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public decimal TotalAmount { get; set; }
    }
}
