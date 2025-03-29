using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class PriceService(IPriceRepository priceRepository)
    {
        private readonly IPriceRepository _priceRepository = priceRepository;

        public async Task<IEnumerable<Price>> GetAllPricesAsync()
        {
            return await _priceRepository.GetAllPricesAsync();
        }

        public async Task<Price?> GEtSpecificPriceAsync(Brand brand, Model model, Year year)
        {
            return await _priceRepository.GetSpecificPriceAsync(brand, model, year);
        }

        public async Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode)
        {
            await _priceRepository.UpsertPriceAsync(price, brandCode, modelCode, yearCode);
        }
    }
}
