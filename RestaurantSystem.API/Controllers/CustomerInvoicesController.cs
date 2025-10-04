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
                var customerInvoiceDtos = customerInvoices.Select(ci => new CustomerInvoiceDto
                {
                    Id = ci.Id,
                    CreatedDate = ci.CreatedDate,
                    CustomerId = ci.CustomerId,
                    TotalAmount = ci.TotalAmount,
                    Customer = ci.Customer != null ? new CustomerDto
                    {
                        Id = ci.Customer.Id,
                        CustomerID = ci.Customer.CustomerID,
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

                return Ok(ApiResponseDto<IEnumerable<CustomerInvoiceDto>>.SuccessResult(customerInvoiceDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<CustomerInvoiceDto>>.ErrorResult("An error occurred while retrieving customer invoices", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceDto>>> GetCustomerInvoice(long id)
        {
            try
            {
                var customerInvoice = await _customerInvoiceService.GetByIdAsync(id);
                if (customerInvoice == null)
                {
                    return NotFound(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Customer invoice not found"));
                }

                var customerInvoiceDto = new CustomerInvoiceDto
                {
                    Id = customerInvoice.Id,
                    CreatedDate = customerInvoice.CreatedDate,
                    CustomerId = customerInvoice.CustomerId,
                    TotalAmount = customerInvoice.TotalAmount,
                    Customer = customerInvoice.Customer != null ? new CustomerDto
                    {
                        Id = customerInvoice.Customer.Id,
                        CustomerID = customerInvoice.Customer.CustomerID,
                        Name = customerInvoice.Customer.Name,
                        Family = customerInvoice.Customer.Family,
                        PhoneNumber = customerInvoice.Customer.PhoneNumber
                    } : null,
                    InvoiceItems = customerInvoice.InvoiceItems.Select(ii => new CustomerInvoiceItemDto
                    {
                        Id = ii.Id,
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                };

                return Ok(ApiResponseDto<CustomerInvoiceDto>.SuccessResult(customerInvoiceDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceDto>.ErrorResult("An error occurred while retrieving the customer invoice", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceDto>>> CreateCustomerInvoice(CreateCustomerInvoiceDto createCustomerInvoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Invalid input data", errors));
                }

                var customerInvoice = new CustomerInvoice
                {
                    CreatedDate = DateTime.Now,
                    CustomerId = createCustomerInvoiceDto.CustomerId,
                    TotalAmount = (long)createCustomerInvoiceDto.TotalAmount,
                    InvoiceItems = createCustomerInvoiceDto.InvoiceItems.Select(ii => new CustomerInvoiceItem
                    {
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                };

                await _customerInvoiceService.AddAsync(customerInvoice);

                var customerInvoiceDto = new CustomerInvoiceDto
                {
                    Id = customerInvoice.Id,
                    CreatedDate = customerInvoice.CreatedDate,
                    CustomerId = customerInvoice.CustomerId,
                    TotalAmount = customerInvoice.TotalAmount,
                    InvoiceItems = customerInvoice.InvoiceItems.Select(ii => new CustomerInvoiceItemDto
                    {
                        Id = ii.Id,
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                };

                return CreatedAtAction(nameof(GetCustomerInvoice), new { id = customerInvoice.Id }, ApiResponseDto<CustomerInvoiceDto>.SuccessResult(customerInvoiceDto, "Customer invoice created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceDto>.ErrorResult("An error occurred while creating the customer invoice", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceDto>>> UpdateCustomerInvoice(long id, UpdateCustomerInvoiceDto updateCustomerInvoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Invalid input data", errors));
                }

                var existingCustomerInvoice = await _customerInvoiceService.GetByIdAsync(id);
                if (existingCustomerInvoice == null)
                {
                    return NotFound(ApiResponseDto<CustomerInvoiceDto>.ErrorResult("Customer invoice not found"));
                }

                existingCustomerInvoice.CustomerId = updateCustomerInvoiceDto.CustomerId;
                existingCustomerInvoice.TotalAmount = (long)updateCustomerInvoiceDto.TotalAmount;

                await _customerInvoiceService.UpdateAsync(existingCustomerInvoice);

                var customerInvoiceDto = new CustomerInvoiceDto
                {
                    Id = existingCustomerInvoice.Id,
                    CreatedDate = existingCustomerInvoice.CreatedDate,
                    CustomerId = existingCustomerInvoice.CustomerId,
                    TotalAmount = existingCustomerInvoice.TotalAmount,
                    InvoiceItems = existingCustomerInvoice.InvoiceItems.Select(ii => new CustomerInvoiceItemDto
                    {
                        Id = ii.Id,
                        CountProduct = ii.CountProduct,
                        Fee = ii.Fee,
                        TotalPrice = ii.TotalPrice,
                        InvoiceReference = ii.InvoiceReference,
                        ProductId = ii.ProductId
                    }).ToList()
                };

                return Ok(ApiResponseDto<CustomerInvoiceDto>.SuccessResult(customerInvoiceDto, "Customer invoice updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceDto>.ErrorResult("An error occurred while updating the customer invoice", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteCustomerInvoice(long id)
        {
            try
            {
                var customerInvoice = await _customerInvoiceService.GetByIdAsync(id);
                if (customerInvoice == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Customer invoice not found"));
                }

                await _customerInvoiceService.DeleteAsync(id);

                return Ok(ApiResponseDto<object>.SuccessResult(null, "Customer invoice deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult("An error occurred while deleting the customer invoice", new List<string> { ex.Message }));
            }
        }
    }
}
