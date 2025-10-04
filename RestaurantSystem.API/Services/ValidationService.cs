using RestaurantSystem.API.DTOs;
using RestaurantSystem.Core.Interfaces;

namespace RestaurantSystem.API.Services
{
    public class ValidationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly ICustomerRepository _customerRepository;

        public ValidationService(
            IProductRepository productRepository,
            IMenuRepository menuRepository,
            ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _menuRepository = menuRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResultDto> ValidateProductAsync(CreateProductDto productDto)
        {
            var result = new ValidationResultDto { IsValid = true };

            // Check if product name already exists
            var existingProducts = await _productRepository.GetAllAsync();
            if (existingProducts.Any(p => p.Name?.ToLower() == productDto.Name.ToLower()))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationErrorDto
                {
                    Field = nameof(productDto.Name),
                    Message = "A product with this name already exists",
                    AttemptedValue = productDto.Name
                });
            }

            if (!result.IsValid)
            {
                result.Message = "Validation failed";
            }

            return result;
        }

        public async Task<ValidationResultDto> ValidateProductUpdateAsync(long id, UpdateProductDto productDto)
        {
            var result = new ValidationResultDto { IsValid = true };

            // Check if product exists
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationErrorDto
                {
                    Field = "Id",
                    Message = "Product not found",
                    AttemptedValue = id
                });
                return result;
            }

            // Check if product name already exists (excluding current product)
            var existingProducts = await _productRepository.GetAllAsync();
            if (existingProducts.Any(p => p.Name?.ToLower() == productDto.Name.ToLower() && p.Id != id))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationErrorDto
                {
                    Field = nameof(productDto.Name),
                    Message = "A product with this name already exists",
                    AttemptedValue = productDto.Name
                });
            }

            if (!result.IsValid)
            {
                result.Message = "Validation failed";
            }

            return result;
        }

        public async Task<ValidationResultDto> ValidateMenuAsync(CreateMenuDto menuDto)
        {
            var result = new ValidationResultDto { IsValid = true };

            // Check if menu title already exists
            var existingMenus = await _menuRepository.GetAllAsync();
            if (existingMenus.Any(m => m.Title?.ToLower() == menuDto.Title.ToLower()))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationErrorDto
                {
                    Field = nameof(menuDto.Title),
                    Message = "A menu with this title already exists",
                    AttemptedValue = menuDto.Title
                });
            }

            if (!result.IsValid)
            {
                result.Message = "Validation failed";
            }

            return result;
        }

        public async Task<ValidationResultDto> ValidateCustomerAsync(CreateCustomerDto customerDto)
        {
            var result = new ValidationResultDto { IsValid = true };

            // Check if customer phone number already exists
            var existingCustomers = await _customerRepository.GetAllAsync();
            var phoneNumber = long.Parse(customerDto.PhoneNumber.Replace("-", "").Replace(" ", ""));
            
            if (existingCustomers.Any(c => c.PhoneNumber == phoneNumber))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationErrorDto
                {
                    Field = nameof(customerDto.PhoneNumber),
                    Message = "A customer with this phone number already exists",
                    AttemptedValue = customerDto.PhoneNumber
                });
            }

            if (!result.IsValid)
            {
                result.Message = "Validation failed";
            }

            return result;
        }

        public async Task<ValidationResultDto> ValidateEntityExistsAsync<T>(long id, string entityName, Func<long, Task<T?>> getByIdFunc)
        {
            var result = new ValidationResultDto { IsValid = true };

            var entity = await getByIdFunc(id);
            if (entity == null)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationErrorDto
                {
                    Field = "Id",
                    Message = $"{entityName} not found",
                    AttemptedValue = id
                });
                result.Message = $"{entityName} not found";
            }

            return result;
        }
    }
}
