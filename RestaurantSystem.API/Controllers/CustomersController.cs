using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<CustomerDto>>>> GetCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                var customerDtos = customers.Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Family = c.Family,
                    PhoneNumber = c.PhoneNumber
                });

                return Ok(ApiResponseDto<IEnumerable<CustomerDto>>.SuccessResult(customerDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<CustomerDto>>.ErrorResult(
                    "An error occurred while retrieving customers", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerDto>>> GetCustomer(long id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound(ApiResponseDto<CustomerDto>.ErrorResult("Customer not found"));
                }

                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Family = customer.Family,
                    PhoneNumber = customer.PhoneNumber
                };

                return Ok(ApiResponseDto<CustomerDto>.SuccessResult(customerDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerDto>.ErrorResult(
                    "An error occurred while retrieving the customer", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<CustomerDto>>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerDto>.ErrorResult("Invalid input data", errors));
                }

                var customer = new Customer
                {
                    Name = createCustomerDto.Name,
                    Family = createCustomerDto.Family,
                    PhoneNumber = long.Parse(createCustomerDto.PhoneNumber.Replace("-", "").Replace(" ", ""))
                };

                await _customerService.AddAsync(customer);

                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Family = customer.Family,
                    PhoneNumber = customer.PhoneNumber
                };

                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id },
                    ApiResponseDto<CustomerDto>.SuccessResult(customerDto, "Customer created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerDto>.ErrorResult(
                    "An error occurred while creating the customer", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerDto>>> UpdateCustomer(long id, UpdateCustomerDto updateCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerDto>.ErrorResult("Invalid input data", errors));
                }

                var existingCustomer = await _customerService.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound(ApiResponseDto<CustomerDto>.ErrorResult("Customer not found"));
                }

                existingCustomer.Name = updateCustomerDto.Name;
                existingCustomer.Family = updateCustomerDto.Family;
                existingCustomer.PhoneNumber = long.Parse(updateCustomerDto.PhoneNumber.Replace("-", "").Replace(" ", ""));

                await _customerService.UpdateAsync(existingCustomer);

                var customerDto = new CustomerDto
                {
                    Id = existingCustomer.Id,
                    Name = existingCustomer.Name,
                    Family = existingCustomer.Family,
                    PhoneNumber = existingCustomer.PhoneNumber
                };

                return Ok(ApiResponseDto<CustomerDto>.SuccessResult(customerDto, "Customer updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerDto>.ErrorResult(
                    "An error occurred while updating the customer", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteCustomer(long id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Customer not found"));
                }

                await _customerService.DeleteAsync(id);

                return Ok(ApiResponseDto<object>.SuccessResult(null, "Customer deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult(
                    "An error occurred while deleting the customer", new List<string> { ex.Message }));
            }
        }
    }
}
