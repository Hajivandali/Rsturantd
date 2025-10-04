using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;

namespace RestaurantSystem.Application.Services
{
    public class PriceService
    {
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<Price?> GetByIdAsync(long id)
        {
            return await _priceRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Price>> GetAllAsync()
        {
            return await _priceRepository.GetAllAsync();
        }

        public async Task AddAsync(Price price)
        {
            await _priceRepository.AddAsync(price);
        }

        public async Task UpdateAsync(Price price)
        {
            await _priceRepository.UpdateAsync(price);
        }

        public async Task DeleteAsync(Price price)
        {
            await _priceRepository.DeleteAsync(price);
        }





    }
}