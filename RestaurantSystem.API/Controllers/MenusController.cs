using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.API.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenusController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<MenuDto>>>> GetMenus()
        {
            try
            {
                var menus = await _menuService.GetAllMenusAsync();
                var menuDtos = menus.Select(m => new MenuDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    CreatedDate = m.CreatedDate,
                    LastEdited = m.LastEdited,
                    MenuItems = m.MenuItems.Select(mi => new MenuItemDto
                    {
                        Id = mi.Id,
                        MenuReference = mi.MenuReference,
                        ProductReference = mi.ProductReference,
                        IsActive = mi.IsActive
                    }).ToList()
                });

                return Ok(ApiResponseDto<IEnumerable<MenuDto>>.SuccessResult(menuDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<IEnumerable<MenuDto>>.ErrorResult("An error occurred while retrieving menus", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<MenuDto>>> GetMenu(long id)
        {
            try
            {
                var menu = await _menuService.GetByIdAsync(id);
                if (menu == null)
                {
                    return NotFound(ApiResponseDto<MenuDto>.ErrorResult("Menu not found"));
                }

                var menuDto = new MenuDto
                {
                    Id = menu.Id,
                    Title = menu.Title,
                    CreatedDate = menu.CreatedDate,
                    LastEdited = menu.LastEdited,
                    MenuItems = menu.MenuItems.Select(mi => new MenuItemDto
                    {
                        Id = mi.Id,
                        MenuReference = mi.MenuReference,
                        ProductReference = mi.ProductReference,
                        IsActive = mi.IsActive
                    }).ToList()
                };

                return Ok(ApiResponseDto<MenuDto>.SuccessResult(menuDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<MenuDto>.ErrorResult("An error occurred while retrieving the menu", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<MenuDto>>> CreateMenu(CreateMenuDto createMenuDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<MenuDto>.ErrorResult("Invalid input data", errors));
                }

                var menu = new Menu
                {
                    Title = createMenuDto.Title,
                    CreatedDate = DateTimeOffset.Now,
                    LastEdited = DateTime.Now
                };

                await _menuService.AddMenuAsync(menu);

                var menuDto = new MenuDto
                {
                    Id = menu.Id,
                    Title = menu.Title,
                    CreatedDate = menu.CreatedDate,
                    LastEdited = menu.LastEdited,
                    MenuItems = new List<MenuItemDto>()
                };

                return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, ApiResponseDto<MenuDto>.SuccessResult(menuDto, "Menu created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<MenuDto>.ErrorResult("An error occurred while creating the menu", new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<MenuDto>>> UpdateMenu(long id, UpdateMenuDto updateMenuDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponseDto<MenuDto>.ErrorResult("Invalid input data", errors));
                }

                var existingMenu = await _menuService.GetByIdAsync(id);
                if (existingMenu == null)
                {
                    return NotFound(ApiResponseDto<MenuDto>.ErrorResult("Menu not found"));
                }

                existingMenu.Title = updateMenuDto.Title;
                existingMenu.LastEdited = DateTime.Now;

                await _menuService.UpdateMenuAsync(existingMenu);

                var menuDto = new MenuDto
                {
                    Id = existingMenu.Id,
                    Title = existingMenu.Title,
                    CreatedDate = existingMenu.CreatedDate,
                    LastEdited = existingMenu.LastEdited,
                    MenuItems = existingMenu.MenuItems.Select(mi => new MenuItemDto
                    {
                        Id = mi.Id,
                        MenuReference = mi.MenuReference,
                        ProductReference = mi.ProductReference,
                        IsActive = mi.IsActive
                    }).ToList()
                };

                return Ok(ApiResponseDto<MenuDto>.SuccessResult(menuDto, "Menu updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<MenuDto>.ErrorResult("An error occurred while updating the menu", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteMenu(long id)
        {
            try
            {
                var menu = await _menuService.GetByIdAsync(id);
                if (menu == null)
                {
                    return NotFound(ApiResponseDto<object>.ErrorResult("Menu not found"));
                }

                await _menuService.DeleteMenuAsync(id);

                return Ok(ApiResponseDto<object>.SuccessResult(null, "Menu deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDto<object>.ErrorResult("An error occurred while deleting the menu", new List<string> { ex.Message }));
            }
        }
    }
}
