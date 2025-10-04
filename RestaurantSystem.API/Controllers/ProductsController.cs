using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.API.Services;

namespace RestaurantSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing restaurant products
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ValidationService _validationService;

        public ProductsController(ProductService productService, ValidationService validationService)
        {
            _productService = productService;
            _validationService = validationService;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of all products</returns>
        /// <response code="200">Returns the list of products</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<IEnumerable<ProductDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<IEnumerable<ProductDto>>), 500)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                var productDtos = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Images = p.Images,
                    Description = p.Description,
                    Unit = p.Unit,
                    InventoryItemID = p.InventoryItemID
                });

                return Ok(ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<ProductDto>>.ErrorResult("An error occurred while retrieving products", new List<string> { ex.Message }));
            }
        }

        /// <summary>
        /// Gets a specific product by ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>The requested product</returns>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">If the product is not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<ProductDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<ProductDto>), 404)]
        [ProducesResponseType(typeof(ApiResponseDto<ProductDto>), 500)]
        public async Task<ActionResult<ApiResponseDto<ProductDto>>> GetProduct(long id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(ApiResponseDto<ProductDto>.ErrorResult("Product not found"));
                }

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Images = product.Images,
                    Description = product.Description,
                    Unit = product.Unit,
                    InventoryItemID = product.InventoryItemID
                };

                return Ok(ApiResponseDto<ProductDto>.SuccessResult(productDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<ProductDto>.ErrorResult("An error occurred while retrieving the product", new List<string> { ex.Message }));
            }
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="createProductDto">The product data</param>
        /// <returns>The created product</returns>
        /// <response code="201">Returns the created product</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<ProductDto>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<ProductDto>), 400)]
        [ProducesResponseType(typeof(ApiResponseDto<ProductDto>), 500)]
        public async Task<ActionResult<ApiResponseDto<ProductDto>>> CreateProduct(CreateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<ProductDto>.ErrorResult("Invalid input data", errors));
                }

                // Additional business validation
                var validationResult = await _validationService.ValidateProductAsync(createProductDto);
                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors.Select(e => e.Message).ToList();
                    return BadRequest(ApiResponseDto<ProductDto>.ErrorResult(validationResult.Message, validationErrors));
                }

                var product = new Product
                {
                    Name = createProductDto.Name,
                    Images = createProductDto.Images,
                    Description = createProductDto.Description,
                    Unit = createProductDto.Unit,
                    InventoryItemID = createProductDto.InventoryItemID
                };

                await _productService.AddProductAsync(product);

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Images = product.Images,
                    Description = product.Description,
                    Unit = product.Unit,
                    InventoryItemID = product.InventoryItemID
                };

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ApiResponseDto<ProductDto>.SuccessResult(productDto, "Product created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<ProductDto>.ErrorResult("An error occurred while creating the product", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<ProductDto>>> UpdateProduct(long id, UpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<ProductDto>.ErrorResult("Invalid input data", errors));
                }

                // Check if product exists
                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound(ApiResponseDto<ProductDto>.ErrorResult("Product not found"));
                }

                // Additional business validation
                var validationResult = await _validationService.ValidateProductUpdateAsync(id, updateProductDto);
                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors.Select(e => e.Message).ToList();
                    return BadRequest(ApiResponseDto<ProductDto>.ErrorResult(validationResult.Message, validationErrors));
                }

                existingProduct.Name = updateProductDto.Name;
                existingProduct.Images = updateProductDto.Images;
                existingProduct.Description = updateProductDto.Description;
                existingProduct.Unit = updateProductDto.Unit;
                existingProduct.InventoryItemID = updateProductDto.InventoryItemID;

                await _productService.UpdateProductAsync(existingProduct);

                var productDto = new ProductDto
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Images = existingProduct.Images,
                    Description = existingProduct.Description,
                    Unit = existingProduct.Unit,
                    InventoryItemID = existingProduct.InventoryItemID
                };

                return Ok(ApiResponseDto<ProductDto>.SuccessResult(productDto, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<ProductDto>.ErrorResult("An error occurred while updating the product", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteProduct(long id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Product not found"));
                }

                await _productService.DeleteProductAsync(product);

                return Ok(ApiResponseDto<object>.SuccessResult(null, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult("An error occurred while deleting the product", new List<string> { ex.Message }));
            }
        }
    }
}
