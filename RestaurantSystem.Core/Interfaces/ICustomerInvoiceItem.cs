using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Core.Interfaces
{
    public interface ICustomerInvoiceItem
    {
        Task<CustomerInvoiceItem?> GetByIdAsync(long id);
        Task<IEnumerable<CustomerInvoiceItem>> GetAllAsync();
        Task AddAsync(CustomerInvoiceItem invoiceItem);
        Task UpdateAsync(CustomerInvoiceItem invoiceItem);
        Task DeleteAsync(CustomerInvoiceItem invoiceItem);
        Task SaveChangesAsync();
    }
}