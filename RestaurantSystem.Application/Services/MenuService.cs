using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;

namespace RestaurantSystem.Application.Services
{
    public class MenuService
    {

        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }



        public async Task<Menu?> GetByIdAsync(long id)
        {
            return await _menuRepository.GetByIdAsync(id);
        }



        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _menuRepository.GetAllAsync();
        }



        public async Task AddMenuAsync(Menu menu)
        {
            await _menuRepository.AddAsync(menu);
            await _menuRepository.SaveChangesAsync();
        }


        public async Task UpdateMenuAsync(Menu menu)
        {
            await _menuRepository.UpdateAsync(menu);
            await _menuRepository.SaveChangesAsync();
        }


        public async Task DeleteMenuAsync(long id)
        {
            await _menuRepository.DeleteAsync(id);
            await _menuRepository.SaveChangesAsync();
        }


}
}