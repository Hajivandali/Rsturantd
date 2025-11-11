using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class CustomerInvoiceItemRepository 
        : GenericRepository<CustomerInvoiceItem>, ICustomerInvoiceItem
    {
        public CustomerInvoiceItemRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<CustomerInvoiceItem?> GetByIdAsync(long id)
        {
            return await _dbSet
                .Include(ci => ci.Product)
                .Include(ci => ci.Invoice)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public override async Task<IEnumerable<CustomerInvoiceItem>> GetAllAsync()
        {
            return await _dbSet
                .Include(ci => ci.Product)
                .Include(ci => ci.Invoice)
                .ToListAsync();
        }
    }
}
