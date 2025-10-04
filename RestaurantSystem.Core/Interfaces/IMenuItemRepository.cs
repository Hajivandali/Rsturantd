using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Core.Interfaces
{
    public interface IMenuItemRepository 
    {
        Task<MenuItem?> GetByIdAsync(long id);
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task AddAsync(MenuItem menuItem);
        Task UpdateAsync(MenuItem menuItem);
        Task DeleteAsync(long id); 
        Task SaveChangesAsync();
        
    }
}