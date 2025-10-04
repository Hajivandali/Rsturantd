using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Core.Interfaces
{
    public interface ICustomerInvoiceRepository 
    {
        Task<CustomerInvoice?> GetByIdAsync(long id);
        Task<IEnumerable<CustomerInvoice>> GetAllAsync();
        Task AddAsync(CustomerInvoice customerInvoice);
        Task UpdateAsync(CustomerInvoice customerInvoice);
        Task DeleteAsync(long id); 
        Task SaveChangesAsync();
    }
}