using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerInvoicesController : ControllerBase
    {
        private readonly CustomerInvoiceService _customerInvoiceService;

        public CustomerInvoicesController(CustomerInvoiceService customerInvoiceService)
        {
            _customerInvoiceService = customerInvoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<CustomerInvoiceDto>>>> GetCustomerInvoices()
        {
            try
            {
                var customerInvoices = await _customerInvoiceService.GetAllAsync();

                var result = customerInvoices.Select(ci => new CustomerInvoiceDto
                {
                    Id = ci.Id,
                    CreatedDate = ci.CreatedDate,
                    CustomerId = ci.CustomerId,
                    TotalAmount = (long)ci.TotalAmount,
                    Customer = ci.Customer != null ? new CustomerDto
                    {
                        Id = ci.Customer.Id,
                        Name = ci.Customer.Name,
                        Family = ci.Customer.Family,
                        PhoneNumber = ci.Customer.PhoneNumber
                    } : null,
                    InvoiceItems = ci.InvoiceItems.Select(ii => new CustomerInvoiceItemDto
                    {
                        Id = ii.Id,
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                });

                return Ok(ApiResponseDto<IEnumerable<CustomerInvoiceDto>>.SuccessResult(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<CustomerInvoiceDto>>.ErrorResult(
                    "An error occurred while retrieving customer invoices",
                    new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceDto>>> GetCustomerInvoice(long id)
        {
            try
            {
                var ci = await _customerInvoiceService.GetByIdAsync(id);
                if (ci == null)
                    return NotFound(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Customer invoice not found"));

                var dto = new CustomerInvoiceDto
                {
                    Id = ci.Id,
                    CreatedDate = ci.CreatedDate,
                    CustomerId = ci.CustomerId,
                    TotalAmount = (long)ci.TotalAmount,
                    Customer = ci.Customer != null ? new CustomerDto
                    {
                        Id = ci.Customer.Id,
                        Name = ci.Customer.Name,
                        Family = ci.Customer.Family,
                        PhoneNumber = ci.Customer.PhoneNumber
                    } : null,
                    InvoiceItems = ci.InvoiceItems.Select(ii => new CustomerInvoiceItemDto
                    {
                        Id = ii.Id,
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                };

                return Ok(ApiResponseDto<CustomerInvoiceDto>.SuccessResult(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceDto>.ErrorResult(
                    "An error occurred while retrieving the customer invoice",
                    new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceDto>>> CreateCustomerInvoice(CreateCustomerInvoiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Invalid input data", errors));
                }

                var entity = new CustomerInvoice
                {
                    CreatedDate = DateTime.UtcNow,
                    CustomerId = dto.CustomerId,
                    TotalAmount = dto.TotalAmount,
                    InvoiceItems = dto.InvoiceItems.Select(ii => new CustomerInvoiceItem
                    {
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                };

                await _customerInvoiceService.AddAsync(entity);

                return CreatedAtAction(nameof(GetCustomerInvoice), new { id = entity.Id },
                    ApiResponseDto<CustomerInvoiceDto>.SuccessResult(new CustomerInvoiceDto
                    {
                        Id = entity.Id,
                        CreatedDate = entity.CreatedDate,
                        CustomerId = entity.CustomerId,
                        TotalAmount = (long)entity.TotalAmount,
                        InvoiceItems = entity.InvoiceItems.Select(ii => new CustomerInvoiceItemDto
                        {
                            Id = ii.Id,
                            CountProduct = ii.CountProduct,
                            Fee = ii.Fee,
                            TotalPrice = ii.TotalPrice,
                            InvoiceReference = ii.InvoiceReference,
                            ProductId = ii.ProductId
                        }).ToList()
                    }, "Customer invoice created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceDto>.ErrorResult(
                    "An error occurred while creating the customer invoice",
                    new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceDto>>> UpdateCustomerInvoice(long id, UpdateCustomerInvoiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Invalid input data", errors));
                }

                var entity = await _customerInvoiceService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Customer invoice not found"));

                entity.CustomerId = dto.CustomerId;
                entity.TotalAmount = dto.TotalAmount;

                await _customerInvoiceService.UpdateAsync(entity);

                return Ok(ApiResponseDto<CustomerInvoiceDto>.SuccessResult(new CustomerInvoiceDto
                {
                    Id = entity.Id,
                    CreatedDate = entity.CreatedDate,
                    CustomerId = entity.CustomerId,
                    TotalAmount = (long)entity.TotalAmount
                }, "Customer invoice updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceDto>.ErrorResult(
                    "An error occurred while updating the customer invoice",
                    new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteCustomerInvoice(long id)
        {
            try
            {
                var entity = await _customerInvoiceService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound(ApiResponseDto<object>.ErrorResult("Customer invoice not found"));

                await _customerInvoiceService.DeleteAsync(id);
                return Ok(ApiResponseDto<object>.SuccessResult( "Customer invoice deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult(
                    "An error occurred while deleting the customer invoice",
                    new List<string> { ex.Message }));
            }
        }
    }
}
