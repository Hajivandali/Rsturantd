using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;
using RestaurantSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class CustomerInvoiceItemRepository : ICustomerInvoiceItem
    {
        private readonly AppDbContext _context;
        
        public CustomerInvoiceItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomerInvoiceItem invoiceItem)
        {
            await _context.CustomerInvoiceItems.AddAsync(invoiceItem);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomerInvoiceItem?> GetByIdAsync(long id)
        {
            return await _context.CustomerInvoiceItems
                .Include(ci => ci.Product)
                .Include(ci => ci.Invoice)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<IEnumerable<CustomerInvoiceItem>> GetAllAsync()
        {
            return await _context.CustomerInvoiceItems
                .Include(ci => ci.Product)
                .Include(ci => ci.Invoice)
                .ToListAsync();
        }

        public async Task UpdateAsync(CustomerInvoiceItem invoiceItem)
        {
            _context.CustomerInvoiceItems.Update(invoiceItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CustomerInvoiceItem invoiceItem)
        {
            _context.CustomerInvoiceItems.Remove(invoiceItem);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}