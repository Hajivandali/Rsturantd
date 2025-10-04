using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly PriceService _priceService;

        public PricesController(PriceService priceService)
        {
            _priceService = priceService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<PriceDto>>>> GetPrices()
        {
            try
            {
                var prices = await _priceService.GetAllAsync();
                var priceDtos = prices.Select(p => new PriceDto
                {
                    Id = p.Id,
                    ProductReference = p.ProductReference,
                    Amount = p.Amount,
                    CreatedDate = p.CreatedDate,
                    LastEdited = p.LastEdited,
                    Product = p.Product != null ? new ProductDto
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Images = p.Product.Images,
                        Description = p.Product.Description,
                        Unit = p.Product.Unit,
                        InventoryItemID = p.Product.InventoryItemID
                    } : null
                });

                return Ok(ApiResponseDto<IEnumerable<PriceDto>>.SuccessResult(priceDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<PriceDto>>.ErrorResult("An error occurred while retrieving prices", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<PriceDto>>> GetPrice(long id)
        {
            try
            {
                var price = await _priceService.GetByIdAsync(id);
                if (price == null)
                {
                    return NotFound(ApiResponseDto<PriceDto>.ErrorResult("Price not found"));
                }

                var priceDto = new PriceDto
                {
                    Id = price.Id,
                    ProductReference = price.ProductReference,
                    Amount = price.Amount,
                    CreatedDate = price.CreatedDate,
                    LastEdited = price.LastEdited,
                    Product = price.Product != null ? new ProductDto
                    {
                        Id = price.Product.Id,
                        Name = price.Product.Name,
                        Images = price.Product.Images,
                        Description = price.Product.Description,
                        Unit = price.Product.Unit,
                        InventoryItemID = price.Product.InventoryItemID
                    } : null
                };

                return Ok(ApiResponseDto<PriceDto>.SuccessResult(priceDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<PriceDto>.ErrorResult("An error occurred while retrieving the price", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<PriceDto>>> CreatePrice(CreatePriceDto createPriceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<PriceDto>.ErrorResult("Invalid input data", errors));
                }

                var price = new Price
                {
                    ProductReference = createPriceDto.ProductReference,
                    Amount = (long)createPriceDto.Amount,
                    CreatedDate = DateTime.Now,
                    LastEdited = DateTime.Now
                };

                await _priceService.AddAsync(price);

                var priceDto = new PriceDto
                {
                    Id = price.Id,
                    ProductReference = price.ProductReference,
                    Amount = price.Amount,
                    CreatedDate = price.CreatedDate,
                    LastEdited = price.LastEdited
                };

                return CreatedAtAction(nameof(GetPrice), new { id = price.Id }, ApiResponseDto<PriceDto>.SuccessResult(priceDto, "Price created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<PriceDto>.ErrorResult("An error occurred while creating the price", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<PriceDto>>> UpdatePrice(long id, UpdatePriceDto updatePriceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<PriceDto>.ErrorResult("Invalid input data", errors));
                }

                var existingPrice = await _priceService.GetByIdAsync(id);
                if (existingPrice == null)
                {
                    return NotFound(ApiResponseDto<PriceDto>.ErrorResult("Price not found"));
                }

                existingPrice.Amount = (long)updatePriceDto.Amount;
                existingPrice.LastEdited = DateTime.Now;

                await _priceService.UpdateAsync(existingPrice);

                var priceDto = new PriceDto
                {
                    Id = existingPrice.Id,
                    ProductReference = existingPrice.ProductReference,
                    Amount = existingPrice.Amount,
                    CreatedDate = existingPrice.CreatedDate,
                    LastEdited = existingPrice.LastEdited
                };

                return Ok(ApiResponseDto<PriceDto>.SuccessResult(priceDto, "Price updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<PriceDto>.ErrorResult("An error occurred while updating the price", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeletePrice(long id)
        {
            try
            {
                var price = await _priceService.GetByIdAsync(id);
                if (price == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Price not found"));
                }

                await _priceService.DeleteAsync(price);

                return Ok(ApiResponseDto<object>.SuccessResult(null, "Price deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult("An error occurred while deleting the price", new List<string> { ex.Message }));
            }
        }
    }
}
