using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly MenuItemService _menuItemService;

        public MenuItemsController(MenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<MenuItemDto>>>> GetMenuItems()
        {
            try
            {
                var menuItems = await _menuItemService.GetAllAsync();
                var menuItemDtos = menuItems.Select(mi => new MenuItemDto
                {
                    Id = mi.Id,
                    MenuId = mi.MenuId,
                    ProductId = mi.ProductId,
                    IsActive = mi.IsActive,
                    Menu = mi.Menu != null ? new MenuDto
                    {
                        Id = mi.Menu.Id,
                        Title = mi.Menu.Title,
                        CreatedDate = mi.Menu.CreatedDate,
                        LastEdited = mi.Menu.LastEdited.UtcDateTime
                    } : null,
                    Product = mi.Product != null ? new ProductDto
                    {
                        Id = mi.Product.Id,
                        Name = mi.Product.Name,
                        Images = mi.Product.Images,
                        Description = mi.Product.Description,
                        Unit = mi.Product.Unit,
                        InventoryItemID = mi.Product.InventoryItemID
                    } : null
                });

                return Ok(ApiResponseDto<IEnumerable<MenuItemDto>>.SuccessResult(menuItemDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<MenuItemDto>>.ErrorResult("An error occurred while retrieving menu items", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<MenuItemDto>>> GetMenuItem(long id)
        {
            try
            {
                var menuItem = await _menuItemService.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return NotFound(ApiResponseDto<MenuItemDto>.ErrorResult("Menu item not found"));
                }

                var menuItemDto = new MenuItemDto
                {
                    Id = menuItem.Id,
                    MenuId = menuItem.MenuId,
                    ProductId = menuItem.ProductId,
                    IsActive = menuItem.IsActive,
                    Menu = menuItem.Menu != null ? new MenuDto
                    {
                        Id = menuItem.Menu.Id,
                        Title = menuItem.Menu.Title,
                        CreatedDate = menuItem.Menu.CreatedDate,
                        LastEdited = menuItem.Menu.LastEdited.UtcDateTime
                    } : null,
                    Product = menuItem.Product != null ? new ProductDto
                    {
                        Id = menuItem.Product.Id,
                        Name = menuItem.Product.Name,
                        Images = menuItem.Product.Images,
                        Description = menuItem.Product.Description,
                        Unit = menuItem.Product.Unit,
                        InventoryItemID = menuItem.Product.InventoryItemID
                    } : null
                };

                return Ok(ApiResponseDto<MenuItemDto>.SuccessResult(menuItemDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<MenuItemDto>.ErrorResult("An error occurred while retrieving the menu item", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<MenuItemDto>>> CreateMenuItem(CreateMenuItemDto createMenuItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<MenuItemDto>.ErrorResult("Invalid input data", errors));
                }

                var menuItem = new MenuItem
                {
                    MenuId = createMenuItemDto.MenuId,
                    ProductId = createMenuItemDto.ProductId,
                    IsActive = createMenuItemDto.IsActive
                };

                await _menuItemService.AddAsync(menuItem);

                var menuItemDto = new MenuItemDto
                {
                    Id = menuItem.Id,
                    MenuId = menuItem.MenuId,
                    ProductId = menuItem.ProductId,
                    IsActive = menuItem.IsActive
                };

                return CreatedAtAction(nameof(GetMenuItem), new { id = menuItem.Id }, ApiResponseDto<MenuItemDto>.SuccessResult(menuItemDto, "Menu item created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<MenuItemDto>.ErrorResult("An error occurred while creating the menu item", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<MenuItemDto>>> UpdateMenuItem(long id, UpdateMenuItemDto updateMenuItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<MenuItemDto>.ErrorResult("Invalid input data", errors));
                }

                var existingMenuItem = await _menuItemService.GetByIdAsync(id);
                if (existingMenuItem == null)
                {
                    return NotFound(ApiResponseDto<MenuItemDto>.ErrorResult("Menu item not found"));
                }

                existingMenuItem.MenuId = updateMenuItemDto.MenuId;
                existingMenuItem.ProductId = updateMenuItemDto.ProductId;
                existingMenuItem.IsActive = updateMenuItemDto.IsActive;

                await _menuItemService.UpdateAsync(existingMenuItem);

                var menuItemDto = new MenuItemDto
                {
                    Id = existingMenuItem.Id,
                    MenuId = existingMenuItem.MenuId,
                    ProductId = existingMenuItem.ProductId,
                    IsActive = existingMenuItem.IsActive
                };

                return Ok(ApiResponseDto<MenuItemDto>.SuccessResult(menuItemDto, "Menu item updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<MenuItemDto>.ErrorResult("An error occurred while updating the menu item", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteMenuItem(long id)
        {
            try
            {
                var menuItem = await _menuItemService.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Menu item not found"));
                }

                await _menuItemService.DeleteAsync(id);

                return Ok(ApiResponseDto<object>.SuccessResult(new object(), "Menu item deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult("An error occurred while deleting the menu item", new List<string> { ex.Message }));
            }
        }
    }
}
