using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class MenuItemRepository 
        : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<MenuItem?> GetByIdAsync(long id)
        {
            return await _dbSet
                .Include(mi => mi.Product)
                .Include(mi => mi.Menu)
                .FirstOrDefaultAsync(mi => mi.Id == id);
        }

        public override async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _dbSet
                .Include(mi => mi.Menu)
                .Include(mi => mi.Product)
                .ToListAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
