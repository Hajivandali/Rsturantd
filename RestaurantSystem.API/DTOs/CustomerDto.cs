using System.ComponentModel.DataAnnotations;
using RestaurantSystem.API.Attributes;

namespace RestaurantSystem.API.DTOs
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public long? CustomerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
    }

    public class CreateCustomerDto
    {
        public long? CustomerID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Family is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Family must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Family can only contain letters and spaces")]
        public string Family { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [PhoneNumberValidation(ErrorMessage = "Phone number must be a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class UpdateCustomerDto
    {
        public long? CustomerID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Family is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Family must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Family can only contain letters and spaces")]
        public string Family { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [PhoneNumberValidation(ErrorMessage = "Phone number must be a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
