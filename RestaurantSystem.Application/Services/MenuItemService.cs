using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;

namespace RestaurantSystem.Application.Services
{
    public class MenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<MenuItem?> GetByIdAsync(long id)
        {
            return await _menuItemRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _menuItemRepository.GetAllAsync();
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _menuItemRepository.AddAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            await _menuItemRepository.UpdateAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _menuItemRepository.DeleteAsync(id);
            await _menuItemRepository.SaveChangesAsync();
        }


    }
}