using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class MenuRepository 
        : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<Menu?> GetByIdAsync(long id)
        {
            return await _dbSet
                .Include(m => m.MenuItems)
                .ThenInclude(mi => mi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.MenuItems)
                .ThenInclude(mi => mi.Product)
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
