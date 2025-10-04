using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;

namespace RestaurantSystem.Application.Services
{
    public class CustomerInvoiceService
    {
        private readonly ICustomerInvoiceRepository _customerInvoiceRepository;

        public CustomerInvoiceService(ICustomerInvoiceRepository customerInvoiceRepository)
        {
            _customerInvoiceRepository = customerInvoiceRepository;
        }

        public async Task<CustomerInvoice?> GetByIdAsync(long id)
        {
            return await _customerInvoiceRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CustomerInvoice>> GetAllAsync()
        {
            return await _customerInvoiceRepository.GetAllAsync();
        }

        public async Task AddAsync(CustomerInvoice customerInvoice)
        {
            await _customerInvoiceRepository.AddAsync(customerInvoice);
            await _customerInvoiceRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerInvoice customerInvoice)
        {
            await _customerInvoiceRepository.UpdateAsync(customerInvoice);
            await _customerInvoiceRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _customerInvoiceRepository.DeleteAsync(id);
            await _customerInvoiceRepository.SaveChangesAsync();
        }
    }
} 