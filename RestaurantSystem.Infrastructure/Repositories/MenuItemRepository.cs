using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems
                .Include(x => x.Menu)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(long id)
        {
            return await _context.MenuItems
                .Include(mi => mi.Product)
                .Include(mi => mi.Menu)
                .FirstOrDefaultAsync(mi => mi.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}