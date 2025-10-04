using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Core.Interfaces
{
    public interface IPriceRepository
    {
        Task<Price?> GetByIdAsync(long id);
        Task<IEnumerable<Price>> GetAllAsync();
        Task AddAsync(Price price);
        Task UpdateAsync(Price price);
        Task DeleteAsync(Price price);
    }
}