using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerInvoiceItemsController : ControllerBase
    {
        private readonly CustomerInvoiceItemService _customerInvoiceItemService;

        public CustomerInvoiceItemsController(CustomerInvoiceItemService customerInvoiceItemService)
        {
            _customerInvoiceItemService = customerInvoiceItemService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<CustomerInvoiceItemDto>>>> GetCustomerInvoiceItems()
        {
            try
            {
                var customerInvoiceItems = await _customerInvoiceItemService.GetAllAsync();
                var customerInvoiceItemDtos = customerInvoiceItems.Select(cii => new CustomerInvoiceItemDto
                {
                    Id = cii.Id,
                    CountProduct = cii.CountProduct,
                    Fee = cii.Fee,
                    TotalPrice = cii.TotalPrice,
                    InvoiceReference = cii.InvoiceReference,
                    ProductId = cii.ProductId,
                    Invoice = cii.Invoice != null ? new CustomerInvoiceDto
                    {
                        Id = cii.Invoice.Id,
                        CreatedDate = cii.Invoice.CreatedDate,
                        CustomerId = cii.Invoice.CustomerId,
                        TotalAmount = cii.Invoice.TotalAmount
                    } : null,
                    Product = cii.Product != null ? new ProductDto
                    {
                        Id = cii.Product.Id,
                        Name = cii.Product.Name,
                        Images = cii.Product.Images,
                        Description = cii.Product.Description,
                        Unit = cii.Product.Unit,
                        InventoryItemID = cii.Product.InventoryItemID
                    } : null
                });

                return Ok(ApiResponseDto<IEnumerable<CustomerInvoiceItemDto>>.SuccessResult(customerInvoiceItemDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<CustomerInvoiceItemDto>>.ErrorResult("An error occurred while retrieving customer invoice items", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceItemDto>>> GetCustomerInvoiceItem(long id)
        {
            try
            {
                var customerInvoiceItem = await _customerInvoiceItemService.GetByIdAsync(id);
                if (customerInvoiceItem == null)
                {
                    return NotFound(ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("Customer invoice item not found"));
                }

                var customerInvoiceItemDto = new CustomerInvoiceItemDto
                {
                    Id = customerInvoiceItem.Id,
                    CountProduct = customerInvoiceItem.CountProduct,
                    Fee = customerInvoiceItem.Fee,
                    TotalPrice = customerInvoiceItem.TotalPrice,
                    InvoiceReference = customerInvoiceItem.InvoiceReference,
                    ProductId = customerInvoiceItem.ProductId,
                    Invoice = customerInvoiceItem.Invoice != null ? new CustomerInvoiceDto
                    {
                        Id = customerInvoiceItem.Invoice.Id,
                        CreatedDate = customerInvoiceItem.Invoice.CreatedDate,
                        CustomerId = customerInvoiceItem.Invoice.CustomerId,
                        TotalAmount = customerInvoiceItem.Invoice.TotalAmount
                    } : null,
                    Product = customerInvoiceItem.Product != null ? new ProductDto
                    {
                        Id = customerInvoiceItem.Product.Id,
                        Name = customerInvoiceItem.Product.Name,
                        Images = customerInvoiceItem.Product.Images,
                        Description = customerInvoiceItem.Product.Description,
                        Unit = customerInvoiceItem.Product.Unit,
                        InventoryItemID = customerInvoiceItem.Product.InventoryItemID
                    } : null
                };

                return Ok(ApiResponseDto<CustomerInvoiceItemDto>.SuccessResult(customerInvoiceItemDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("An error occurred while retrieving the customer invoice item", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceItemDto>>> CreateCustomerInvoiceItem(CreateCustomerInvoiceItemDto createCustomerInvoiceItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("Invalid input data", errors));
                }

                var customerInvoiceItem = new CustomerInvoiceItem
                {
                    CountProduct = createCustomerInvoiceItemDto.CountProduct,
                    Fee = createCustomerInvoiceItemDto.Fee,
                    TotalPrice = createCustomerInvoiceItemDto.TotalPrice,
                    InvoiceReference = createCustomerInvoiceItemDto.InvoiceReference,
                    ProductId = createCustomerInvoiceItemDto.ProductId
                };

                await _customerInvoiceItemService.AddAsync(customerInvoiceItem);

                var customerInvoiceItemDto = new CustomerInvoiceItemDto
                {
                    Id = customerInvoiceItem.Id,
                    CountProduct = customerInvoiceItem.CountProduct,
                    Fee = customerInvoiceItem.Fee,
                    TotalPrice = customerInvoiceItem.TotalPrice,
                    InvoiceReference = customerInvoiceItem.InvoiceReference,
                    ProductId = customerInvoiceItem.ProductId
                };

                return CreatedAtAction(nameof(GetCustomerInvoiceItem), new { id = customerInvoiceItem.Id }, ApiResponseDto<CustomerInvoiceItemDto>.SuccessResult(customerInvoiceItemDto, "Customer invoice item created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("An error occurred while creating the customer invoice item", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<CustomerInvoiceItemDto>>> UpdateCustomerInvoiceItem(long id, UpdateCustomerInvoiceItemDto updateCustomerInvoiceItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("Invalid input data", errors));
                }

                var existingCustomerInvoiceItem = await _customerInvoiceItemService.GetByIdAsync(id);
                if (existingCustomerInvoiceItem == null)
                {
                    return NotFound(ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("Customer invoice item not found"));
                }

                existingCustomerInvoiceItem.CountProduct = updateCustomerInvoiceItemDto.CountProduct;
                existingCustomerInvoiceItem.Fee = updateCustomerInvoiceItemDto.Fee;
                existingCustomerInvoiceItem.TotalPrice = updateCustomerInvoiceItemDto.TotalPrice;

                await _customerInvoiceItemService.UpdateAsync(existingCustomerInvoiceItem);

                var customerInvoiceItemDto = new CustomerInvoiceItemDto
                {
                    Id = existingCustomerInvoiceItem.Id,
                    CountProduct = existingCustomerInvoiceItem.CountProduct,
                    Fee = existingCustomerInvoiceItem.Fee,
                    TotalPrice = existingCustomerInvoiceItem.TotalPrice,
                    InvoiceReference = existingCustomerInvoiceItem.InvoiceReference,
                    ProductId = existingCustomerInvoiceItem.ProductId
                };

                return Ok(ApiResponseDto<CustomerInvoiceItemDto>.SuccessResult(customerInvoiceItemDto, "Customer invoice item updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<CustomerInvoiceItemDto>.ErrorResult("An error occurred while updating the customer invoice item", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteCustomerInvoiceItem(long id)
        {
            try
            {
                var customerInvoiceItem = await _customerInvoiceItemService.GetByIdAsync(id);
                if (customerInvoiceItem == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Customer invoice item not found"));
                }

                await _customerInvoiceItemService.DeleteAsync(customerInvoiceItem);

                return Ok(ApiResponseDto<object>.SuccessResult(null, "Customer invoice item deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult("An error occurred while deleting the customer invoice item", new List<string> { ex.Message }));
            }
        }
    }
}
