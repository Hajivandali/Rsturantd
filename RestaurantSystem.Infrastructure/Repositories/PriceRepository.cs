using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AppDbContext _context;

        public PriceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Price?> GetByIdAsync(long id)
        {
            return await _context.Prices
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Price>> GetAllAsync()
        {
            return await _context.Prices
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task AddAsync(Price price)
        {
            await _context.Prices.AddAsync(price);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Price price)
        {
            _context.Prices.Update(price);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Price price)
        {
            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
