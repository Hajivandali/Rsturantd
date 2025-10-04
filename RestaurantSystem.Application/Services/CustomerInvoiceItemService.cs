using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Application.Services
{
    public class CustomerInvoiceItemService
    {
        private readonly ICustomerInvoiceItem _customerInvoiceItem;

        public CustomerInvoiceItemService(ICustomerInvoiceItem customerInvoiceItem)
        {
            _customerInvoiceItem = customerInvoiceItem;
        }

        public async Task<CustomerInvoiceItem?> GetByIdAsync(long id)
        {
            return await _customerInvoiceItem.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CustomerInvoiceItem>> GetAllAsync()
        {
            return await _customerInvoiceItem.GetAllAsync();
        }

        public async Task AddAsync(CustomerInvoiceItem customerInvoiceItem)
        {
            await _customerInvoiceItem.AddAsync(customerInvoiceItem);
            await _customerInvoiceItem.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerInvoiceItem customerInvoiceItem)
        {
            await _customerInvoiceItem.UpdateAsync(customerInvoiceItem);
            await _customerInvoiceItem.SaveChangesAsync();
        }

        public async Task DeleteAsync(CustomerInvoiceItem customerInvoiceItem)
        {
            await _customerInvoiceItem.DeleteAsync(customerInvoiceItem);
            await _customerInvoiceItem.SaveChangesAsync();
        }
    }
}