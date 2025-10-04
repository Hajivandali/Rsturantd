using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Core.Interfaces
{
    public interface IMenuRepository
    {
        Task<Menu?> GetByIdAsync(long id);
        Task<IEnumerable<Menu>> GetAllAsync();
        Task AddAsync(Menu menu);
        Task UpdateAsync(Menu menu);
        Task DeleteAsync(long id); 
        Task SaveChangesAsync();
    }
}