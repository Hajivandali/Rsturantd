using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class CustomerInvoiceRepository 
        : GenericRepository<CustomerInvoice>, ICustomerInvoiceRepository
    {
        public CustomerInvoiceRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<CustomerInvoice?> GetByIdAsync(long id)
        {
            return await _dbSet
                .Include(ci => ci.Customer)
                .Include(ci => ci.InvoiceItems)
                .ThenInclude(cii => cii.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id);
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

        public override async Task<IEnumerable<CustomerInvoice>> GetAllAsync()
        {
            return await _dbSet
                .Include(ci => ci.Customer)
                .Include(ci => ci.InvoiceItems)
                .ThenInclude(cii => cii.Product)
                .ToListAsync();
        }
    }
}
