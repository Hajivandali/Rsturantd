using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class PriceRepository 
        : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<Price?> GetByIdAsync(long id)
        {
            return await _dbSet
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IEnumerable<Price>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Product)
                .ToListAsync();
        }
    }
}
