using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class PriceService(IPriceRepository priceRepository)
    {
        private readonly IPriceRepository _priceRepository = priceRepository;

        public async Task<List<Price>> GetAllPricesAsync()
        {
            return await _priceRepository.GetAllPricesAsync();
        }

        public async Task<Price?> GetSpecificPriceAsync(string brandCode, int modelCode, string yearCode)
        {
            return await _priceRepository.GetSpecificPriceAsync(brandCode, modelCode, yearCode);
        }

        public async Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode)
        {
            await _priceRepository.UpsertPriceAsync(price, brandCode, modelCode, yearCode);
        }
    }
}
