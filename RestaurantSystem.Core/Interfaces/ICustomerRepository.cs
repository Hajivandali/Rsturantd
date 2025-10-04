using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Core.Interfaces
{
    public interface ICustomerRepository 
    {
        Task<Customer?> GetByIdAsync(long id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(long id); 
        Task SaveChangesAsync();
    }
} 