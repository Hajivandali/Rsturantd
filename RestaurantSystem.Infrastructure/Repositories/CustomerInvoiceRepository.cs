using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class CustomerInvoiceRepository : ICustomerInvoiceRepository
    {
        private readonly AppDbContext _context;

        public CustomerInvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerInvoice?> GetByIdAsync(long id)
        {
            return await _context.CustomerInvoices
                .Include(ci => ci.Customer)
                .Include(ci => ci.InvoiceItems)
                .ThenInclude(cii => cii.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<IEnumerable<CustomerInvoice>> GetAllAsync()
        {
            return await _context.CustomerInvoices
                .Include(ci => ci.Customer)
                .Include(ci => ci.InvoiceItems)
                .ThenInclude(cii => cii.Product)
                .ToListAsync();
        }

        public async Task AddAsync(CustomerInvoice customerInvoice)
        {
            await _context.CustomerInvoices.AddAsync(customerInvoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerInvoice customerInvoice)
        {
            _context.CustomerInvoices.Update(customerInvoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);
            if (customerInvoice != null)
            {
                _context.CustomerInvoices.Remove(customerInvoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 